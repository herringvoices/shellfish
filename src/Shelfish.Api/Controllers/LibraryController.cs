using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelfish.Api.Dtos.Library;
using Shelfish.Api.Services.Interfaces;

namespace Shelfish.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LibraryController : ControllerBase
{
    private readonly ILibraryService _libs;
    public LibraryController(ILibraryService libs) => _libs = libs;

    [HttpGet]
    public async Task<IActionResult> GetMyLibraries()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
          ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;

if (userId is null) return Unauthorized();
        return Ok(await _libs.GetUserLibrariesAsync(userId));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
          ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;

if (userId is null) return Unauthorized();
        var lib = await _libs.GetByIdAsync(id, userId);
        return lib is null ? NotFound() : Ok(lib);
    }

    [HttpPost]
    public async Task<IActionResult> Create(LibraryDto dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
          ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;

if (userId is null) return Unauthorized();
        var created = await _libs.CreateAsync(dto, userId);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
          ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;

if (userId is null) return Unauthorized();
        var ok = await _libs.DeleteAsync(id, userId);
        return ok ? NoContent() : NotFound();
    }
}
