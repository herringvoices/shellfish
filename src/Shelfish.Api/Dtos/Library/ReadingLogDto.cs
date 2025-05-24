namespace Shelfish.Api.Dtos.Library;

public record ReadingLogDto(int Id, int BookId, int PatronAccountId, DateTime ReadAt);
