using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using AspNetCore.Identity.MongoDbCore;
using MongoDbGenericRepository.Attributes;
using AspNetCore.Identity.MongoDbCore.Models;

namespace Travlr.WebApi.Models
{
    /// <summary>
    /// User Model mapped to Document fields in the MongoDB user collection
    /// </summary>
    [CollectionName("users")]
    public class ApplicationUser : MongoIdentityUser<ObjectId>
    {
        // this is the username field
        [BsonRequired]
        [BsonElement("email")]
        public override string? Email
        {
            get => base.Email;
            set
            {
                base.Email = value;
                // Sync with normalized fields used by Identity
                base.NormalizedEmail = value?.ToUpperInvariant();
            }
        }

        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        // random salt value to prevent duplicate passwords
        [BsonRequired]
        [BsonElement("salt")]
        public string Salt { get; set; } = null!;

        // password hash
        [BsonRequired]
        [BsonElement("hash")]
        public string Hash { get; set; } = null!;
    }
}
