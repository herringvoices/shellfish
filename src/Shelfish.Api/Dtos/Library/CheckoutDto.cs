namespace Shelfish.Api.Dtos.Library;

public record CheckoutDto(
    int Id,
    int BookId,
    int? PatronAccountId,
    DateTime CheckedOutAt,
    DateTime DueDate,
    DateTime? ReturnedAt,
    string ApprovedById
);
