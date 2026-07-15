using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Travlr.WebApi.Models
{
    /// <summary>
    /// Trip Model mapped to Document fields in the MongoDB Trips collection
    /// </summary>
    public class Trip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // These properties are required to create a new document
        [BsonRequired]
        [BsonElement("code")]
        public string Code { get; set; }

        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonRequired]
        [BsonElement("length")]
        public string Length { get; set; }

        [BsonRequired]
        [BsonElement("start")]
        public DateTime Start { get; set; }

        [BsonRequired]
        [BsonElement("resort")]
        public string Resort { get; set; }

        // price per person stored as a string
        [BsonRequired]
        [BsonElement("perPerson")]
        public string PerPerson { get; set; }

        // file path to an image resource
        [BsonRequired]
        [BsonElement("image")]
        public string Image { get; set; }

        [BsonRequired]
        [BsonElement("description")]
        public string Description { get; set; }

        // __v field is  MongoDB's document version key
        [BsonElement("__v")]
        public int Version { get; set; }
    }
}
