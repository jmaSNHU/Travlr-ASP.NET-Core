using Travlr.WebApi.Dtos;

namespace Travlr.WebApi.Services
{
    /// <summary>
    /// Interface contract for ITripsService
    /// Defines CRUD methods
    /// </summary>
    public interface ITripsService
    {
        public Task<List<TripDto>> GetAsync();
        public Task<TripDto?> GetAsync(string id);
        public Task CreateAsync(TripDto trip);
        public Task<TripDto?> UpdateAsync(string id,  TripDto trip);
        public Task RemoveAsync(string id);
    }
}
