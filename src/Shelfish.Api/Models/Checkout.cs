namespace Shelfish.Api.Models;

public class Checkout
{
    public int Id { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; } = null!;

    public int? PatronAccountId { get; set; }
    public PatronAccount? PatronAccount { get; set; }

    public DateTime CheckedOutAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedAt { get; set; }

    public string ApprovedById { get; set; } = null!;
    public User ApprovedBy { get; set; } = null!;
}
