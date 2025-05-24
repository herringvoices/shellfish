namespace Shelfish.Api.Dtos.Library;

public record NoteDto(
    int Id,
    int BookId,
    int? PatronAccountId,
    int? UserId,
    string Body,
    DateTime CreatedAt
);
