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
        public override string? Email { get; set; }
        //{
        //    get => base.Email;
        //    set
        //    {
        //        base.Email = value;
        //        // Sync with normalized fields used by Identity
        //        base.NormalizedEmail = value?.ToUpperInvariant();
        //    }
        //}

        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; } = null!;

        // password hash
        // Note: Identity defaults to 100,000 iterations
        // and automatically computes the salt value, 
        // and stores it in the hash field.
        [BsonRequired]
        [BsonElement("hash")]
        public override string? PasswordHash { get; set; } = null!;
    }
}
