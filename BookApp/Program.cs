using BookApp.Data;
using BookApp.Entities;
using BookApp.Repositories;

var bookRepository = new SqlRepository<Book>(new BookAppDbContext());

Console.WriteLine("Hello! This is a digital book archive.");
Console.WriteLine("Choose what you want to do.");
Console.WriteLine("---------------------------------------");
Console.WriteLine("1 - Add book.\n" + "2 - Remove book.\n" + "3 - Show book by Id.\n" + "X - Close app");
Console.WriteLine("---------------------------------------");
Console.WriteLine("What you want to do? \nPress key 1, 2 or X: ");

var userSelectionOfOptions = Console.ReadLine();
bool close = false;

while (!close)
{
    switch (userSelectionOfOptions)
    {
        case "1":
            Console.WriteLine("Choose what you want to do.");
            close = true;
            break;
        case "2":
            close = true;
            break;
        case "3":
            WriteAllToConsole(bookRepository);
            close = true;
            break;
        case "X":
        case "x":
            close = true;
            break;
        default:
            Console.WriteLine("Invalid operation.");
            close = true;
            break;
    }
}

static void SaveLogFile(string repository, string action, string comment)
{
    using (var writer = File.AppendText($"BookAppLog.txt"))
    {
        writer.WriteLine($"[{DateTime.Now}]-{repository}-{action}-[{comment}]");
    }
}

/*static void AddBooks(IRepository<Book> bookRepository)
{
    bookRepository.Add(new Book { Title = "Ordinary Book 1", Author = "John Smith I", PublicationDate = 2021 });
    bookRepository.Add(new Book { Title = "Ordinary Book 2", Author = "John Smith II", PublicationDate = 2022 });
    bookRepository.Add(new Book { Title = "Ordinary Book 3", Author = "John Smith III", PublicationDate = 2023 });
    bookRepository.Save();
}

static void AddBooksSpecial(IWriteRepository<BookSpecial> bookSpecialRepository)
{
    bookSpecialRepository.Add(new BookSpecial { Title = "Extra Book 1", Author = "Anna Smith I", PublicationDate = 2021 });
    bookSpecialRepository.Add(new BookSpecial { Title = "Extra Book 1", Author = "Anna Smith II", PublicationDate = 2022 });
    bookSpecialRepository.Add(new BookSpecial { Title = "Extra Book 1", Author = "Anna Smith III", PublicationDate = 2023 });
    bookSpecialRepository.Save();
}*/

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}