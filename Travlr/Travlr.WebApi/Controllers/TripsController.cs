using Microsoft.AspNetCore.Mvc;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Services;

namespace Travlr.WebApi.Controllers
{
    /// <summary>
    /// Defines GetAll, Get, Create, Update and Delete actions
    /// for the /trips endpoints
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
            var trips = await _tripsService.GetAsync();
            return Ok(trips);
        }

        /// <summary>
        /// GET /trips/{code}
        /// Takes a unique trip code a parameter and returns
        /// the matching trip objects, if found
        /// Otherwise, returns status code 404: Not Found
        /// </summary>
        /// <param name="code"></param>
        /// <returns>HTTP status 200 OK and a TripDto object with matching Code</returns>
        [HttpGet("{code}")]
        public async Task<ActionResult<TripDto?>> Get(string code)
        {
            var trip = await _tripsService.GetAsync(code);
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
        [HttpPost]
        public async Task<ActionResult<TripDto>> Create([FromForm] TripDto trip)
        {
            await _tripsService.CreateAsync(trip);
            // produces a 201 created response
            // uses the [GET] method to fetch the newly created Trip
            return CreatedAtAction(nameof(Get), new { code = trip.Code }, trip);
        }
        
        /// <summary>
        /// Updates an existing Trip by Code
        /// Returns status 200 after successful update
        /// </summary>
        /// <param name="code"></param>
        /// <param name="trip"></param>
        /// <returns>HTTP status 200 OK and the update TripDto</returns>
        [HttpPut("{code}")]
        public async Task<ActionResult<TripDto>> Update(string code,[FromForm] TripDto trip)
        {
            await _tripsService.UpdateAsync(code, trip);
            return Ok(trip);
        }

        /// <summary>
        /// Deletes an existing Trip by Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>HTTP status 204 No Content</returns>
        [HttpDelete("{code}")]
        public async Task<ActionResult> Delete(string code)
        {
            await _tripsService.RemoveAsync(code);
            return NoContent();
        }
    }
}
