namespace Travlr.WebApi.Dtos
{
    public record RegisterDto(string Email, string Password, string Name);
    public record LoginDto(string Email, string Password);
    public record AuthResponseDto(bool IsSuccess, string Token, string Message);
}
