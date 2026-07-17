using MongoDB.Bson.Serialization.Attributes;

namespace Travlr.WebApi.Dtos
{
    /// <summary>
    /// Data Transfer Object for User Model
    /// </summary>
    public class ApplicationUserDto
    {
        // this is the username field
        public string Email { get; set; } = null!;

        public string Name { get; set; } = null!;

        // random salt value to prevent duplicate passwords
        public string Salt { get; set; } = null!;

        // password hash
        public string Hash { get; set; } = null!;
    }
}
