using MongoDB.Bson.Serialization.Attributes;

namespace Travlr.WebApi.Dtos
{
    /// <summary>
    /// Data Transfer Object for Trip Model 
    /// </summary>
    public class TripDto
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Length { get; set; }

        public DateTime Start { get; set; }

        public string Resort { get; set; }

        // price per person stored as a string
        public string PerPerson { get; set; }

        // file path to an image resource
        public string Image { get; set; }

        public string Description { get; set; }
    }
}
