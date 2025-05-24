namespace Shelfish.Api.Dtos.Library;

public record LibraryDto(
    int Id,
    string Name,
    bool IsClassroom,
    int LibrarianId,
    string LibraryCode
);
