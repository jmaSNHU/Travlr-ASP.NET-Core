using Travlr.WebApi.Models;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Mappings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Travlr.WebApi.Repository;

namespace Travlr.WebApi.Services
{
    /// <summary>
    /// Implements CRUD methods defined by ITripsService
    /// </summary>
    /// <param name="travlrDatabaseSettings"></param>
    public class TripsService : ITripsService
    {
        private readonly IRepository<Trip> _repository;

        public TripsService(IRepository<Trip> repository)
        {
            _repository = repository;
        }



        // returns a list of all trip DTOs
        public async Task<IEnumerable<TripDto>> GetTripsAsync()
        {
            var trips = await _repository.GetAsync();
            return trips.ToDtoList();
        }

        // find and returns a single trip by Id
        public async Task<TripDto?> GetTripAsync(string id)
        {
            Trip trip = await _repository.GetAsync(id);
            return trip.ToDto();
        }

        // saves a newly created trip to the database
        public async Task CreateTripAsync(TripDto trip)
        {
            var tripEntity = trip.ToEntity();
            await _repository.CreateAsync(tripEntity);
            // have to save the Id back to DTO for controller to return Get(Id) 
            trip.Id = tripEntity.Id;
        }

        // updates an existing trip
        public async Task<TripDto?> UpdateTripAsync(string id, TripDto trip)
        {
            var tripEntity = await _repository.UpdateAsync(id, trip.ToEntity());
            if (tripEntity != null)
            {
                return tripEntity.ToDto();
            }  
            return null;
        }

        // deletes an existing trip
        public async Task RemoveTripAsync(string id) 
            => await _repository.RemoveAsync(id);
    }
}
