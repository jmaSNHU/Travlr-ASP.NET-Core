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
        public Task<TripDto?> GetAsync(string code);
        public Task CreateAsync(TripDto trip);
        public Task<TripDto> UpdateAsync(string code,  TripDto trip);
        public Task RemoveAsync(string code);
    }
}
