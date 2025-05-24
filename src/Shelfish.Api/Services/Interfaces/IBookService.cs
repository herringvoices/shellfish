using Shelfish.Api.Dtos.Library;

namespace Shelfish.Api.Services.Interfaces;
public interface IBookService
{
    Task<IEnumerable<BookDto>> GetByLibraryAsync(int libraryId);
    Task<BookDto?> GetAsync(int id);
    Task<BookDto> AddAsync(BookDto dto, int libraryId);
    Task<bool> DeleteAsync(int id, int libraryId);
}
