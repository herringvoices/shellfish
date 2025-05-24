using Shelfish.Api.Dtos.Auth;
namespace Shelfish.Api.Services.Interfaces;
public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(RegisterDto dto);
    Task<AuthResultDto> LoginAsync(LoginDto dto);
    Task LogoutAsync();
}
