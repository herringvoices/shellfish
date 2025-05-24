using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shelfish.Api.Data;
using Shelfish.Api.Dtos.Library;
using Shelfish.Api.Models;
using Shelfish.Api.Services.Interfaces;

namespace Shelfish.Api.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    private readonly IGoogleBooksService _google;

    public BookService(AppDbContext db, IMapper mapper, IGoogleBooksService google)
    {
        _db     = db;
        _mapper = mapper;
        _google = google;
    }

    public async Task<IEnumerable<BookDto>> GetByLibraryAsync(int libraryId)
    {
        var books = await _db.Books.Where(b => b.LibraryId == libraryId)
                                   .OrderBy(b => new { b.CategoryId, b.Author, b.SeriesId, b.VolumeNumber, b.Title })
                                   .ToListAsync();
        return _mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto?> GetAsync(int id)
    {
        var book = await _db.Books.FindAsync(id);
        return book is null ? null : _mapper.Map<BookDto>(book);
    }

    public async Task<BookDto> AddAsync(BookDto dto, int libraryId)
    {
        var entity = _mapper.Map<Book>(dto);
        entity.LibraryId = libraryId;

        /* ---------- Google Books enrichment ---------- */
        var g = await _google.FetchAsync(dto.Isbn);
        if (g is not null)
        {
            entity.Title    = g.VolumeInfo.Title    ?? entity.Title;
            entity.Subtitle = g.VolumeInfo.Subtitle ?? entity.Subtitle;
            entity.Details  = g.VolumeInfo.Description;

            // take first author & flip to "Last, First"
            var author = g.VolumeInfo.Authors?.FirstOrDefault() ?? "Unknown Author";
            var parts  = author.Split(' ');
            entity.Author = parts.Length > 1
                            ? $"{parts[^1]}, {string.Join(' ', parts[..^1])}"
                            : author;

            entity.SmallThumbnail = g.VolumeInfo.ImageLinks?.SmallThumbnail;
            entity.LargeThumbnail = g.VolumeInfo.ImageLinks?.Thumbnail;
        }
        /* --------------------------------------------- */

        _db.Books.Add(entity);
        await _db.SaveChangesAsync();
        return _mapper.Map<BookDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id, int libraryId)
    {
        var book = await _db.Books.FirstOrDefaultAsync(b => b.Id == id && b.LibraryId == libraryId);
        if (book is null) return false;
        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        return true;
    }
}
