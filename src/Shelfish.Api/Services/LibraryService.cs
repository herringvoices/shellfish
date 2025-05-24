using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shelfish.Api.Data;
using Shelfish.Api.Dtos.Library;
using Shelfish.Api.Models;
using Shelfish.Api.Services.Interfaces;

namespace Shelfish.Api.Services;

public class LibraryService : ILibraryService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    public LibraryService(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LibraryDto>> GetUserLibrariesAsync(string userId)
    {
        var libs = await _db.Libraries.Where(l => l.UserId == userId)
                                      .ToListAsync();
        return _mapper.Map<IEnumerable<LibraryDto>>(libs);
    }

    public async Task<LibraryDto?> GetByIdAsync(int id, string userId)
    {
        var lib = await _db.Libraries.FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);
        return lib is null ? null : _mapper.Map<LibraryDto>(lib);
    }

    public async Task<LibraryDto> CreateAsync(LibraryDto dto, string userId)
    {
        var entity = _mapper.Map<Library>(dto);
        entity.UserId = userId;
        entity.LibraryCode = Guid.NewGuid().ToString("N").Substring(0, 8);

        _db.Libraries.Add(entity);
        await _db.SaveChangesAsync();

        return _mapper.Map<LibraryDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id, string userId)
    {
        var lib = await _db.Libraries.FirstOrDefaultAsync(l => l.Id == id && l.UserId == userId);
        if (lib is null) return false;

        _db.Libraries.Remove(lib);
        await _db.SaveChangesAsync();
        return true;
    }
}
