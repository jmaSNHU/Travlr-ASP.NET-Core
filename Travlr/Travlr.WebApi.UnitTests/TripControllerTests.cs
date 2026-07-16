using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Travlr.WebApi.Controllers;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Services;
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

        [Fact]
        public async Task TestGetAllExpectOkAndTripsList()
        {
            // Arrange
            var trips = new List<TripDto>
            {
                new TripDto {Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime()},
                new TripDto {Code = "B", Name = "B Trip", Description = "B Trip Desc.", Length = "2 days", Resort = "B resort", Image = "b_img.jpg", PerPerson = "2300.00", Start = new DateTime()}
            };

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

        // TODO: more testing!!!
    }
}
