using Microsoft.AspNetCore.Mvc;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Services;

namespace Travlr.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private ITripsService _tripsService;

        // dependency injection provides the implementation of ITripsService
        public TripsController(ITripsService tripsService) => _tripsService = tripsService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TripDto>>> GetAll()
        {
            var trips = await _tripsService.GetAsync();
            return Ok(trips);
        }

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

        [HttpPost]
        public async Task<ActionResult<TripDto>> Create(TripDto trip)
        {
            await _tripsService.CreateAsync(trip);
            return CreatedAtAction(nameof(Get), trip.Code);
        }

        [HttpPut("{code}")]
        public async Task<ActionResult<TripDto>> Update(string code, TripDto trip)
        {
            await _tripsService.UpdateAsync(code, trip);
            return Ok(trip);
        }

        [HttpDelete("{code}")]
        public async Task<ActionResult> Delete(string code)
        {
            await _tripsService.RemoveAsync(code);
            return Ok();
        }
    }
}
