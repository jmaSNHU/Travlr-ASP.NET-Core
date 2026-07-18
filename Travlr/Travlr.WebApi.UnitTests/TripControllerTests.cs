using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Travlr.WebApi.Controllers;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Services;
using Travlr.WebApi.Models;
using Xunit;
using Xunit.Abstractions;

namespace Travlr.WebApi.UnitTests
{
    public class TripControllerTests
    {
        private readonly ITestOutputHelper _output;

        private readonly Mock<ITripsService> _mockTripsService;
        private readonly TripsController _controller;

        /// <summary>
        /// mock trips service and inject into trips controller
        /// </summary>
        public TripControllerTests(ITestOutputHelper output)
        {
            _output = output;
            _mockTripsService = new Mock<ITripsService>();
            _controller = new TripsController(_mockTripsService.Object);
        }

        #region Test GET ALL

        [Fact]
        public async Task TestGetAllExpectOkAndTripsList()
        {
            // Arrange
            var trips = new List<TripDto>
            {
                new TripDto {Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime()},
                new TripDto {Code = "B", Name = "B Trip", Description = "B Trip Desc.", Length = "2 days", Resort = "B resort", Image = "b_img.jpg", PerPerson = "2300.00", Start = new DateTime()}
            };

            // mocks the trip service dependency to returns the expected result
            // note: the trip service is not what's being tested here
            _mockTripsService
                .Setup(s => s.GetAsync())
                .ReturnsAsync(trips);

            // Act
            var actionResult = await _controller.GetAll();

            // Assert
            var resultOK = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedTrips = Assert.IsAssignableFrom<IEnumerable<TripDto>>(resultOK.Value);
            Assert.Equal(2, returnedTrips.Count()); // expect 2 trips in the list
        }
        #endregion

        #region Test GET
        [Fact]
        public async Task TestGetExpectOkAndSingleTrip()
        {
            // Arrange
            var trip = new TripDto { Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };

            // TripService.GetAsync("A") 
            // returns the trip object declared above
            _mockTripsService
                .Setup(s => s.GetAsync("A"))
                .ReturnsAsync(trip);

            // Act
            var actionResult = await _controller.Get("A");

            // Assert
            var resultOk = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedTrip = Assert.IsAssignableFrom<TripDto>(resultOk.Value);
            Assert.Equal("A", returnedTrip.Code);
        }

        [Fact]
        public async Task TestGetExpectNotFound()
        {
            // Arrange
            var trip = new TripDto { Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };

            // mock TripService.GetAsync()
            _mockTripsService
                .Setup(s => s.GetAsync("B"))
                .ReturnsAsync(trip);

            // Act
            var actionResult = await _controller.Get("A");

            // Assert
            var resultOk = Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        #endregion 

        #region Test CREATE
        [Fact]
        public async Task TestCreateTrip()
        {
            // Arrange
            var newTrip = new TripDto { Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };

            // mock TripService.CreateAsync()
            _mockTripsService
                .Setup(s => s.CreateAsync(newTrip))
                .Returns(Task.CompletedTask);

            // Act
            var actionResult = await _controller.Create(newTrip);

            // Assert
            var resultCreated = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            Assert.Equal(nameof(_controller.Get), resultCreated.ActionName);
            Assert.Equal("A", resultCreated.RouteValues?["code"]);

            var createdTrip = Assert.IsType<TripDto>(resultCreated.Value);
            Assert.Equal("A", createdTrip.Code);
            Assert.Equal("A Trip", createdTrip.Name);
            Assert.Equal("A Trip Desc.", createdTrip.Description);
            Assert.Equal("1 day", createdTrip.Length);
            Assert.Equal("A resort", createdTrip.Resort);
            Assert.Equal("a_img.jpg", createdTrip.Image);
            Assert.Equal("1200.00", createdTrip.PerPerson);
        }
        #endregion

        #region Test UPDATE

        [Fact]
        public async Task TestUpdateTrip()
        {
            // Arrange
            var tripToUpdate = new TripDto { Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };
            
            // mock TripService.UpdateAsync()
            _mockTripsService
                .Setup(s => s.UpdateAsync("A", tripToUpdate))
                .ReturnsAsync(tripToUpdate);

            // Act
            tripToUpdate.Name = "AAA Trip";
            var actionResult = await _controller.Update("A", tripToUpdate);

            // Assert
            var resultOk = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnedTrip = Assert.IsAssignableFrom<TripDto>(resultOk.Value);
        }

        [Fact]
        public async Task TestUpdateTripNotFound()
        {
            // Arrange
            var tripToUpdate = new TripDto { Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };

            // mock not necessary
            _mockTripsService
                .Setup(s => s.UpdateAsync("A", tripToUpdate))
                .ReturnsAsync(tripToUpdate);

            // Act
            tripToUpdate.Name = "AAA Trip";
            // try to update a non-existant trip
            var actionResult = await _controller.Update("B", tripToUpdate);
            var test = actionResult.Value;
            if (test != null)
            {
                _output.WriteLine(test.Resort);
            }

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        #endregion

        #region Test DELETE

        [Fact]
        public async Task TestDeleteTrip()
        {
            // Arrange
            var tripToRemove = new TripDto { Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };

            // mock TripService.GetAsync() - used by controller Delete method
            _mockTripsService
                .Setup(s => s.GetAsync(tripToRemove.Code))
                .ReturnsAsync(tripToRemove);
            // mock TripService.RemoveAsync() 
            _mockTripsService
                .Setup(s => s.RemoveAsync(tripToRemove.Code))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(tripToRemove.Code);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task TestDeleteTripNotFound()
        {
            // Arrange
            _mockTripsService
                .Setup(s => s.GetAsync("A"))
                .ReturnsAsync((TripDto)null!);
            _mockTripsService
                .Setup(s => s.RemoveAsync("A"))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete("A");

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion
    }
}
