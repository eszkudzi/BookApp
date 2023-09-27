using BookApp.Data;
using BookApp.Entities;
using BookApp.Repositories;

//var bookRepository = new FileRepository<Book>();
//var bookSpecialRepository = new FileRepository<BookSpecial>();

IRepository<Book> bookRepository;
IRepository<BookSpecial> bookSpecialRepository;

Console.WriteLine("Hello! This is a digital book archive.");
Console.WriteLine("---------------------------------------");
Console.WriteLine("The data will be saved to a json file.");
Console.WriteLine("Currently, MS SQL Server cannot be used.");
Console.WriteLine("---------------------------------------");
Console.WriteLine("1 - Add book.\n" + "2 - Remove book.\n" + "3 - Show book using Id.\n" + "4 - Show list of books.\n" + "X - Close app");
Console.WriteLine("---------------------------------------");
Console.WriteLine("What you want to do? \nPress key 1, 2 or X: ");

var userSelectionOfOptions = Console.ReadLine();
bool closeApp = false;

while (!closeApp)
{
    switch (userSelectionOfOptions)
    {
        case "1":
            AddBooks(bookRepository, bookSpecialRepository);
            closeApp = true;
            break;
        case "2":
            //RemoveBook();
            closeApp = true;
            break;
        case "3":
            //ShowBook();
            closeApp = true;
            break;
        case "4":
            //ShowAll();
            closeApp = true;
            break;
        case "X":
        case "x":
            closeApp = true;
            break;
        default:
            Console.WriteLine("Invalid operation.");
            closeApp = true;
            break;
    }
}

static void AddBooks(IRepository<Book> bookRepository, IRepository<BookSpecial> bookSpecialRepository)
{
    Console.WriteLine($"Do you want to add a ordinary (choose 'o') or special book (choose 's')?\n" + "To finish entering data, select the letter \"Q\".");
    var userOptions = Console.ReadLine();
    string title;
    string author;
    //string publicationDate = null;
    //int publicationDateConvert = int.Parse(publicationDate);

        if (userOptions == "o" || userOptions == "O")
        {
            Console.WriteLine($"You choose add a ordinary book. Enter the title of the book and press 'enter'.");
            title = Console.ReadLine();
            Console.WriteLine($"Enter the author of the book and press 'enter'.");
            author = Console.ReadLine();
            //Console.WriteLine($"Enter the author of the book and press 'enter'.");
            //var publicationDate = Console.ReadLine();

            bookRepository.Add(new Book { Title = title, Author = author, PublicationDate = 2021 });

            bookRepository.Save();
        }
        else if (userOptions == "s" || userOptions == "S")
        {
            //bookSpecialRepository.Add();
            //bookSpecialRepository.Save();
        }

}



/*static void SaveLogFile(string repository, string action, string comment)
{
    using (var writer = File.AppendText($"BookAppLog.txt"))
    {
        writer.WriteLine($"[{DateTime.Now}]-{repository}-{action}-[{comment}]");
    }
}
*/