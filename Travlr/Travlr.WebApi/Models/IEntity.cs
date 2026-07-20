using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Travlr.WebApi.Models
{
    public interface IEntity
    {
        // Id may be null on create
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
    }
}
