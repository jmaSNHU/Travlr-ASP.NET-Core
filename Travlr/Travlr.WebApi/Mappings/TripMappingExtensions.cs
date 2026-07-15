using MongoDB.Driver;
using System.Net.NetworkInformation;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Models;

namespace Travlr.WebApi.Mappings
{
    /// <summary>
    /// Trip Mapping Extensions class defines
    /// extension methods for mapping:
    /// Trip -> TripDto,
    /// TripDto -> Trip
    /// </summary>
    public static class TripMappingExtensions
    {
        /// <summary>
        /// Maps a Trip object to a TripDto object
        /// </summary>
        /// <param name="trip"></param>
        /// <returns></returns>
        public static TripDto ToDto(this Trip trip)
        {
            if (trip == null) return null!;

            return new TripDto
            {
                // map Trip properties to TripDto properties
                Code = trip.Code,
                Name = trip.Name,
                Length = trip.Length,
                Start = trip.Start,
                Resort = trip.Resort,
                PerPerson = trip.PerPerson,
                Image = trip.Image,
                Description = trip.Description
            };
        }

        // convert any IEnumberable of trips to a list of trip dtos
        public static List<TripDto> ToDtoList(this IEnumerable<Trip> trips)
        {
            return trips.Select(x => ToDto(x)).ToList();
        }

        /// <summary>
        /// Maps a TripDto object to Trip object
        /// </summary>
        /// <param name="tripDto"></param>
        /// <returns></returns>
        public static Trip ToEntity(this TripDto tripDto)
        {
            if (tripDto == null) return null!;

            return new Trip
            {
                // map dto properties to model
                Code = tripDto.Code,
                Name = tripDto.Name,
                Length = tripDto.Length,
                Start = tripDto.Start,
                Resort = tripDto.Resort,
                PerPerson = tripDto.PerPerson,
                Image = tripDto.Image,
                Description = tripDto.Description
            };
        }

        // convert any IEnumerable of Trip DTOs to a list of Trips
        public static List<Trip> ToEntityList(this IEnumerable<TripDto> tripDtos)
        {
            return tripDtos.Select(x => ToEntity(x)).ToList();
        }
    }
}
