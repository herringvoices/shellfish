namespace Shelfish.Api.Dtos.Auth;
public record AuthResultDto(bool Success, string Message, object? User = null);
