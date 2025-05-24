namespace Shelfish.Api.Dtos.Library;

public record BookDto(
    int Id,
    string Title,
    string Author,
    bool IsSeries,
    string? SeriesName,
    int? Volume,
    int? GenreId,
    string? GoogleBooksId,
    int? BookshelfId
);
