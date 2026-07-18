using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Travlr.WebApi.Dtos
{
    /// <summary>
    /// Data Transfer Object for Trip Model 
    /// </summary>
    public class TripDto
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = null!;
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Length { get; set; } = null!;

        public DateTime Start { get; set; }

        public string Resort { get; set; } = null!; 

        // price per person stored as a string
        public string PerPerson { get; set; } = null!;

        // file path to an image resource
        public string Image { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
