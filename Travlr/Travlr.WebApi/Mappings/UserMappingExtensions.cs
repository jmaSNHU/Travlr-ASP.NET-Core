using System.Runtime.CompilerServices;
using Travlr.WebApi.Dtos;
using Travlr.WebApi.Models;

namespace Travlr.WebApi.Mappings
{
    /// <summary>
    /// User Mapping Extensions class defines
    /// extension methods for mapping:
    /// User -> UserDto
    /// UserDto -> User
    /// </summary>
    public static class UserMappingExtensions
    {
        /// <summary>
        /// Maps a User object to a UserDto object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static ApplicationUserDto ToDto (this ApplicationUser user)
        {
            if (user == null)
            {
                return null!;
            }

            return new ApplicationUserDto
            {
                Email = user.Email,
                Name = user.Name,
                Salt = user.Salt,
                Hash = user.Hash
            };
        }

        /// <summary>
        /// Maps a UserDto object to a User object
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public static ApplicationUser ToEntity(this ApplicationUserDto userDto)
        {
            if (userDto == null)
            {
                return null!;
            }

            return new ApplicationUser
            {
                Email = userDto.Email,
                Name = userDto.Name,
                Salt = userDto.Salt,
                Hash = userDto.Hash
            };
        }
    }
}
