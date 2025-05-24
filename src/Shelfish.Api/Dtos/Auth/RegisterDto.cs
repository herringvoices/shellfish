namespace Shelfish.Api.Dtos.Auth;
public record RegisterDto(string UserName,string Email,string Password,
                          string FirstName,string LastName);
