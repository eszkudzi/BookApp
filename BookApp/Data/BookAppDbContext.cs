using Microsoft.EntityFrameworkCore;
using BookApp.Entities;

namespace BookApp.Data
{
    public class BookAppDbContext : DbContext
    {
        public DbSet<Book> Book => Set<Book>();
        public DbSet<BookOwner> BookOwner => Set<BookOwner>();

        public BookAppDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("BookAppDb");
        }
    }
}
