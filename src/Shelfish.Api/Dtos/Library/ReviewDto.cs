namespace Shelfish.Api.Dtos.Library;

public record ReviewDto(
    int Id,
    int BookId,
    int? PatronAccountId,
    int LibraryId,
    string? Name,
    int Rating,
    string? Body,
    DateTime CreatedAt
);
