using Travlr.WebApi.Models;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Mappings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Travlr.WebApi.Services
{
    /// <summary>
    /// Implements CRUD methods defined by ITripsService
    /// </summary>
    /// <param name="travlrDatabaseSettings"></param>
    public class TripsService(IOptions<TravlrDatabaseSettings> travlrDatabaseSettings) : ITripsService
    {
        // use the MongoClient to open a connection using the TravlrDatabaseSettings
        // get the Trips collection from the Travlr database
        private readonly IMongoCollection<Trip> _tripsCollection =
            new MongoClient(travlrDatabaseSettings.Value.ConnectionString)
            .GetDatabase(travlrDatabaseSettings.Value.DatabaseName)
            .GetCollection<Trip>(travlrDatabaseSettings.Value.TripsCollectionName);

        // returns all trips
        public async Task<List<TripDto>> GetAsync()
        {
            var trips = await _tripsCollection.Find(_ => true).ToListAsync();
            return trips.ToDtoList();
        }

        // find and returns a single trip by (unique) code
        public async Task<TripDto?> GetAsync(string code)
        {
            var trip = await _tripsCollection.Find(x => x.Code == code).FirstOrDefaultAsync();
            return trip.ToDto();
        }

        // saves a newly created trip to the database
        public async Task CreateAsync(TripDto trip) =>
            await _tripsCollection.InsertOneAsync(trip.ToEntity());

        // updates an existing trip
        public async Task<TripDto?> UpdateAsync(string code, TripDto trip)
        {
            var result = await _tripsCollection.ReplaceOneAsync(x => x.Code == code, trip.ToEntity());
            if (result.MatchedCount != 0)
                return await GetAsync(trip.Code);
            else
                return null;
        }

        // deletes an existing trip
        public async Task RemoveAsync(string code) 
            => await _tripsCollection.DeleteOneAsync(x => x.Code == code);
    }
}
