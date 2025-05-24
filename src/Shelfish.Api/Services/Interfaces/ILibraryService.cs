using Shelfish.Api.Dtos.Library;

namespace Shelfish.Api.Services.Interfaces;
public interface ILibraryService
{
    Task<IEnumerable<LibraryDto>> GetUserLibrariesAsync(string userId);
    Task<LibraryDto?> GetByIdAsync(int id, string userId);
    Task<LibraryDto> CreateAsync(LibraryDto dto, string userId);
    Task<bool> DeleteAsync(int id, string userId);
}
