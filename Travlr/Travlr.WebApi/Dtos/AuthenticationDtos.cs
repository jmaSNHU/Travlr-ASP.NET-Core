namespace Travlr.WebApi.Dtos
{
    /// <summary>
    /// DTO passed to the Identity /register endpoint
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    /// <param name="Name"></param>
    public record RegisterDto(string Email, string Password, string Name);
    /// <summary>
    /// DTO passed to the Login Endpoint
    /// </summary>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    public record LoginDto(string Email, string Password);
    /// <summary>
    /// DTO returned by Login and Register endpoints
    /// </summary>
    /// <param name="IsSuccess"></param>
    /// <param name="Token"></param>
    /// <param name="Message"></param>
    public record AuthResponseDto(bool IsSuccess, string Token, string Message);
}
