using Shelfish.Api.Dtos.Auth;
using Shelfish.Api.Services.Interfaces;
namespace Shelfish.Api.Services;
public class AuthService : IAuthService
{
    public Task<AuthResultDto> RegisterAsync(RegisterDto dto) =>
        Task.FromResult(new AuthResultDto(false, "Stub"));
    public Task<AuthResultDto> LoginAsync(LoginDto dto) =>
        Task.FromResult(new AuthResultDto(false, "Stub"));
    public Task LogoutAsync() => Task.CompletedTask;
}
