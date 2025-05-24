using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Shelfish.Api.Models;

public class User : IdentityUser
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required, EmailAddress]
    public override string Email
    {
        get => base.Email;
        set => base.Email = value;
    }

    // Navigation property for libraries where this user is a librarian
    public ICollection<Library> Libraries { get; set; } = new List<Library>();

    // Navigation property for checkouts approved by this user
    public ICollection<Checkout> ApprovedCheckouts { get; set; } = new List<Checkout>();
}
