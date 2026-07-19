using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Services;

namespace Travlr.WebApi.Controllers
{
    /// <summary>
    /// Defines GetAll, Get, Create, Update and Delete actions
    /// for the /trips endpoints
    /// 
    /// [Authorized]
    /// Create/Update/Delete require a bearer token
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _tripsService;

        // dependency injection provides the implementation of ITripsService
        public TripsController(ITripsService tripsService) => _tripsService = tripsService;

        /// <summary>
        /// GET /trips endpoint
        /// Returns HTTP OK status 200 and a collection of all trips
        /// </summary>
        /// <returns>HTTP status 200 OK and IEnumberable<TripDto></Trips></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripDto>>> GetAll()
        {
            var trips = await _tripsService.GetTripsAsync();
            return Ok(trips);
        }

        /// <summary>
        /// GET /trips/{id}
        /// Takes a unique trip id as a parameter and returns
        /// the matching trip objects, if found
        /// Otherwise, returns status code 404: Not Found
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HTTP status 200 OK and a TripDto object with matching Id</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TripDto?>> Get(string id)
        {
            var trip = await _tripsService.GetTripAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return Ok(trip);
        }

        /// <summary>
        /// Creates a new Trip using the TripDto parameter.
        /// Produces HTTP status code 201 upon successful creation
        /// </summary>
        /// <param name="trip"></param>
        /// <returns>HTTP status 201 Created and the newly created TripDto</returns>
        [Authorize] // requires authentication
        [HttpPost]
        public async Task<ActionResult<TripDto>> Create([FromBody] TripDto trip)
        {
            await _tripsService.CreateTripAsync(trip);
            // produces a 201 created response
            // uses the [GET] method to fetch the newly created Trip
            return CreatedAtAction(nameof(Get), new { id = trip.Id }, trip);
        }

        /// <summary>
        /// Updates an existing Trip by Id
        /// Returns status 200 after successful update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="trip"></param>
        /// <returns>HTTP status 200 OK and the update TripDto</returns>
        [Authorize] // requires authentication
        [HttpPut("{id}")]
        public async Task<ActionResult<TripDto>> Update(string id,[FromBody] TripDto trip)
        {
            var updatedTrip = await _tripsService.UpdateTripAsync(id, trip);

            if (updatedTrip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        /// <summary>
        /// Deletes an existing Trip by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>HTTP status 204 No Content</returns>
        [Authorize] // requires authentication
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var trip = await _tripsService.GetTripAsync(id);

            // return NotFound result if trip doesn't exist
            if (trip == null)
            {
                return NotFound();
            }

            await _tripsService.RemoveTripAsync(id);
            return NoContent(); // returns NoContent result after deletion
        }
    }
}
