using Microsoft.EntityFrameworkCore;
using BookApp.Entities;

namespace BookApp.Data
{
    public class BookAppDbContext : DbContext
    {
        public BookAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<BookOwner> BookOwner { get; set; }

    }
}
