using Travlr.WebApi.Dtos;

namespace Travlr.WebApi.Services
{
    /// <summary>
    /// Interface contract for ITripsService
    /// Defines CRUD methods
    /// </summary>
    public interface ITripsService
    {
        public Task<IEnumerable<TripDto>> GetTripsAsync();
        public Task<TripDto?> GetTripAsync(string id);
        public Task CreateTripAsync(TripDto trip);
        public Task<TripDto?> UpdateTripAsync(string id,  TripDto trip);
        public Task RemoveTripAsync(string id);
    }
}
