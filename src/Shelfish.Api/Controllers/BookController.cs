using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shelfish.Api.Dtos.Library;
using Shelfish.Api.Services.Interfaces;

namespace Shelfish.Api.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IBookService _books;
    private readonly ILibraryService _libs;
    public BookController(IBookService books, ILibraryService libs)
    {
        _books = books;
        _libs  = libs;
    }

    // GET api/libraries/5/books
    [HttpGet("libraries/{libraryId:int}/books")]
    public async Task<IActionResult> GetByLibrary(int libraryId)
    {
        return Ok(await _books.GetByLibraryAsync(libraryId));
    }

    // GET api/books/10
    [HttpGet("books/{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var book = await _books.GetAsync(id);
        return book is null ? NotFound() : Ok(book);
    }

    // POST api/libraries/5/books
    [HttpPost("libraries/{libraryId:int}/books")]
    public async Task<IActionResult> Create(int libraryId, BookDto dto)
    {
        // simple guard: library must exist & belong to current user
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
          ?? User.FindFirst(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub)?.Value;

if (userId is null) return Unauthorized();
        var lib = await _libs.GetByIdAsync(libraryId, userId);
        if (lib is null) return NotFound("Library not found or not yours.");

        var created = await _books.AddAsync(dto, libraryId);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    // DELETE api/books/10
    [HttpDelete("books/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _books.DeleteAsync(id, 0); // service checks library internally
        return ok ? NoContent() : NotFound();
    }
}
