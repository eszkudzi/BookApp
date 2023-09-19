using BookApp.Data;
using BookApp.Entities;
using BookApp.Repositories;

var bookRepository = new SqlRepository<Book>(new BookAppDbContext());

AddBooks(bookRepository);
AddBooksSpecial(bookRepository);
WriteAllToConsole(bookRepository);

static void AddBooks(IRepository<Book> bookRepository)
{
    bookRepository.Add(new Book { Name = "Ordinary Book 1" });
    bookRepository.Add(new Book { Name = "Ordinary Book 2" });
    bookRepository.Add(new Book { Name = "Ordinary Book 3" });
    bookRepository.Save();
}

static void AddBooksSpecial(IWriteRepository<BookSpecial> bookSpecialRepository)
{
    bookSpecialRepository.Add(new BookSpecial { Name = "Extra Book 1" });
    bookSpecialRepository.Add(new BookSpecial { Name = "Extra Book 2" });
    bookSpecialRepository.Add(new BookSpecial { Name = "Extra Book 3" });
    bookSpecialRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}