using Microsoft.EntityFrameworkCore;
using BookApp.Entities;

namespace BookApp.Data
{
    public class BookAppDbContext : DbContext
    {
        public DbSet<Book> Book => Set<Book>();
        public DbSet<BookSpecial> BookSpecial => Set<BookSpecial>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("StorageAppDb");
        }
    }
}
