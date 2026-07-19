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
using Travlr.WebApi.Repository;
using Travlr.WebApi.Mappings;

namespace Travlr.WebApi.UnitTests
{
    public class TripServiceTests
    {
        // for debug printing
        private readonly ITestOutputHelper _output;

        private readonly Mock<IRepository<Trip>> _mockRepository;
        private readonly TripsService _tripsService;
        

        /// <summary>
        /// Constructor mocks the GenericRepository and injects it into
        /// the Trip Service for testing
        /// </summary>
        /// <param name="testOutputHelper"></param>
        public TripServiceTests(ITestOutputHelper testOutputHelper)
        {
            _output = testOutputHelper;
            _mockRepository = new Mock<IRepository<Trip>>();
            _tripsService = new TripsService(_mockRepository.Object);
        }

        #region Test TripService.GetTripsAsync()

        [Fact]
        public async Task TestGetTripsAsync()
        {

            // Arrange
            var trips = new List<Trip>
            {
                new Trip { Id = "test1", Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime()},
                new Trip { Id = "test2", Code = "B", Name = "B Trip", Description = "B Trip Desc.", Length = "2 days", Resort = "B resort", Image = "b_img.jpg", PerPerson = "2300.00", Start = new DateTime()}
            };

            // setup the mock repo
            _mockRepository
                .Setup(r => r.GetAsync())
                .ReturnsAsync(trips);

            // Act
            var tripDtoList = await _tripsService.GetTripsAsync();

            // Assert
            Assert.IsType<List<TripDto>>(tripDtoList);
            Assert.Equal(2, tripDtoList.Count());
        }

        #endregion



        #region Test TripService.GetTripAsync(id)
        [Fact]
        public async Task TestGetTripAsync()
        {

            // Arrange
            var trip = new Trip { Id = "test1", Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };

            // setup the mock repo
            _mockRepository
                .Setup(r => r.GetAsync(trip.Id))
                .ReturnsAsync(trip);

            // Act
            var tripDto = await _tripsService.GetTripAsync(trip.Id);

            // Assert
            Assert.IsType<TripDto>(tripDto);
            Assert.Equal("test1", tripDto.Id);
        }
        #endregion

        #region Test TripService.GetTripAsync(id) Does Not Exist
        [Fact]
        public async Task TestGetTripAsyncDoesNotExist()
        {

            // Arrange
            // setup the mock repo
            _mockRepository
                .Setup(r => r.GetAsync("test"));

            // Act
            var tripDto = await _tripsService.GetTripAsync("test");

            // Assert
            Assert.Null(tripDto);
        }
        #endregion

        #region Test UpdateTripAsync
        [Fact]
        public async Task TestUpdateTripAsync()
        {

            // Arrange
            Trip trip = new Trip { Id = "test1", Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };
            TripDto tripDto = new TripDto { Id = "test1", Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };

            // setup the mock repo
            _mockRepository
                .Setup(r => r.UpdateAsync("test1", It.IsAny<Trip>()))
                .ReturnsAsync(trip);

            // Act
            var dto = await _tripsService.UpdateTripAsync("test1", tripDto);

            // Assert
            Assert.IsType<TripDto>(dto);
            Assert.Equal("test1", dto.Id);
        }
        #endregion


        #region Test UpdateTripAsyncDoesNotExist
        [Fact]
        public async Task TestUpdateTripAsyncDoesNotExist()
        {

            // Arrange
            var trip = new Trip { Id = "test1", Code = "A", Name = "A Trip", Description = "A Trip Desc.", Length = "1 day", Resort = "A resort", Image = "a_img.jpg", PerPerson = "1200.00", Start = new DateTime() };

            // setup the mock repo
            _mockRepository
                .Setup(r => r.UpdateAsync("test2", trip))
                .ReturnsAsync(null as Trip);

            // Act
            // test updating a non-existant trip id
            var tripDto = await _tripsService.UpdateTripAsync("test2", trip.ToDto());

            // Assert
            Assert.Null(tripDto);
        }
        #endregion

       
    }
}
