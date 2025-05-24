namespace Shelfish.Api.Dtos.Library;

public record PatronAccountDto(
    int Id,
    string FirstName,
    string LastName,
    string UserName,
    int LibraryId
);
