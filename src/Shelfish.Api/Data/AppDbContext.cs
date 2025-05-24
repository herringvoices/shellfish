using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shelfish.Api.Models;

namespace Shelfish.Api.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Library> Libraries => Set<Library>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Bookshelf> Bookshelves => Set<Bookshelf>();
    public DbSet<PatronAccount> PatronAccounts => Set<PatronAccount>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Note> Notes => Set<Note>();
    public DbSet<ReadingLog> ReadingLogs => Set<ReadingLog>();
    public DbSet<Checkout> Checkouts => Set<Checkout>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // Unique index: LibraryId + UserName on PatronAccount
        b.Entity<PatronAccount>()
            .HasIndex(p => new { p.LibraryId, p.UserName })
            .IsUnique()
            .HasDatabaseName("UQ_PatronAccounts_LibraryId_UserName");

        // Unique ReadingLog index
        b.Entity<ReadingLog>()
            .HasIndex(rl => new { rl.PatronAccountId, rl.BookId })
            .IsUnique()
            .HasDatabaseName("UQ_ReadingLogs_PatronAccountId_BookId");

        // Unique LibraryCode
        b.Entity<Library>().HasIndex(l => l.LibraryCode).IsUnique();
    }
}
