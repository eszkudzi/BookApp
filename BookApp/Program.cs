using BookApp.Data;
using BookApp.Entities;
using BookApp.Repositories;

IRepository<Book> bookRepository;
IRepository<BookOwner> bookOwnerRepository;

bool closeApp = true;

Console.WriteLine("--------------------------------------------------------", Console.BackgroundColor = ConsoleColor.Cyan, Console.ForegroundColor = ConsoleColor.Black);
Console.WriteLine("---  HELLO! THIS IS BOOK APP - DIGITAL BOOK ARCHIVE  ---");
Console.WriteLine("--------------------------------------------------------");
Console.ResetColor();
Console.WriteLine("Currently you can use only data of books or book owners.\n", Console.ForegroundColor = ConsoleColor.Cyan);
Console.WriteLine("How do you want to store the data?");
Console.WriteLine("1 - Data stored in .jsons files.\n" + "2 - Data stored in memory.");
Console.WriteLine("Currently you cannot store data in MS SQL Server.");
Console.ResetColor();

switch (GetInputFromUser().ToUpper())
{
    case "1":
        bookRepository = GetRepository<Book>(repositoryType.JSON_FILE);
        bookOwnerRepository = GetRepository<BookOwner>(repositoryType.JSON_FILE);
        Console.WriteLine("You choose work with .json files.\n", Console.ForegroundColor = ConsoleColor.Yellow);
        Console.ResetColor();
        break;
    case "2":
        bookRepository = GetRepository<Book>(repositoryType.IN_MEMORY_SQL);
        bookOwnerRepository = GetRepository<BookOwner>(repositoryType.IN_MEMORY_SQL);
        Console.WriteLine("You choose work with data in memory.\n", Console.ForegroundColor = ConsoleColor.Yellow);
        Console.ResetColor();
        break;
    default:
        Console.WriteLine($"You choose invalid option.\nThe application has been closed.\n", Console.ForegroundColor = ConsoleColor.Red);
        Console.ResetColor();
        return;
}


while (closeApp)
{
    Console.WriteLine("What do you want to do?\n" + "1 - Add Book or Owner\n" + "2 - Remove Book or Owner\n" + "3 - Show Book or Owner by ID\n" + "4 - View all Book or Owner data\n" + "X - Close app", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.ResetColor();

    string? action = GetInputFromUser();
    if ((action != "1") && (action != "2") && (action != "3") && (action != "4") && (action.ToUpper() != "X"))
    {
        Console.WriteLine($"You choose invalid option. Choose again!\n", Console.ForegroundColor = ConsoleColor.Red);
        Console.ResetColor();
        continue;
    }
    else if (action.ToUpper() == "X")
    {
        break;
    }

    Console.WriteLine("Which type of data do you want to work with?", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.ResetColor();
    entitiesType bookOrOwnerType = GetBookOrOwnerType();

    switch (action.ToUpper())
    {
        case "1":
            AddBookOrOwner(bookRepository, bookOwnerRepository, bookOrOwnerType);
            break;

        case "2":
            RemoveBookOrOwner(bookRepository, bookOwnerRepository, bookOrOwnerType);
            break;

        case "3":
            ShowByIdBookOrOwner(bookRepository, bookOwnerRepository, bookOrOwnerType);
            break;

        case "4":
            ShowAllDataBookOrOwner(bookRepository, bookOwnerRepository, bookOrOwnerType);
            break;
        case "X":
            closeApp = false;
            return;

        default:
            Console.WriteLine($"You choose invalid option.", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
            continue;
    }
}

Console.WriteLine("The application has been closed.", Console.ForegroundColor = ConsoleColor.Red);
Console.ResetColor();
static string GetInputFromUser() => Console.ReadLine() ?? "";
static T GetItemByID<T>(IRepository<T> repository, int id) where T : class, IEntity, new()
{
    try
    {
        T item = repository.GetById(id);
        return item;
    }
    catch (Exception e)
    {
        throw new Exception("You choose invalid ID.");
    }
}

IRepository<T> GetRepository<T>(repositoryType repositoryType) where T : class, IEntity
{
    switch (repositoryType)
    {
        case repositoryType.IN_MEMORY_SQL:
            SqlRepository<T> repositorySqlInMemory = new SqlRepository<T>(new BookAppDbContext());
            repositorySqlInMemory.ItemAdded += OnItemAddedSubscribeEvent;
            repositorySqlInMemory.ItemRemoved += OnItemRemovedSubscribeEvent;
            return repositorySqlInMemory;

        case repositoryType.JSON_FILE:
            FileRepository<T> repositoryFile = new FileRepository<T>();
            repositoryFile.ItemAdded += OnItemAddedSubscribeEvent;
            repositoryFile.ItemRemoved += OnItemRemovedSubscribeEvent;
            return repositoryFile;

        default:
            return null;
    }
}

static void OnItemRemovedSubscribeEvent(object? sender, IEntity e)
{
    if (sender is not null)
    {
        var senderName = sender.GetType().Name;
        SaveToLogFile($"{senderName.Substring(0, senderName.Length - 2)}", $"{e.GetType().Name}Removed", e.ToString() ?? "");
    }
}

static void OnItemAddedSubscribeEvent(object? sender, IEntity e)
{
    if (sender is not null)
    {
        var senderName = sender.GetType().Name;
        SaveToLogFile($"{senderName.Substring(0, senderName.Length - 2)}", $"{e.GetType().Name}Added", e.ToString() ?? "");
    }
}

static void SaveToLogFile(string repositoryType, string action, string comment)
{
    using (var writer = File.AppendText($"BookAppLog.txt"))
    {
        writer.WriteLine($"[{DateTime.Now}]-{repositoryType}-{action}-[{comment}]");
    }
}

static Book GetDataBook()
{
    Book book = new();
    Console.WriteLine("You can enter data of book.", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.Write("Title: ", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.ResetColor();
    book.Title = GetInputFromUser();

    Console.Write("Author: ", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.ResetColor();
    book.Author = GetInputFromUser();

    return book;
}

static BookOwner GetDataOwner()
{
    BookOwner bookOwner = new();
    Console.WriteLine("You can enter data of owner.", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.Write("First name: ", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.ResetColor();
    bookOwner.FirstName = GetInputFromUser();

    Console.Write("Last name: ", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.ResetColor();
    bookOwner.LastName = GetInputFromUser();

    return bookOwner;
}

static entitiesType GetBookOrOwnerType()
{
    Console.WriteLine("B - Book\n" + "O - Owner", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.ResetColor();

    switch (GetInputFromUser().ToUpper())
    {
        case "B":
            Console.WriteLine("You choose data type: BOOK\n", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ResetColor();
            return entitiesType.BOOK;

        case "O":
            Console.WriteLine("You choose data type: OWNER\n", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.ResetColor();
            return entitiesType.OWNER;

        default:
            Console.WriteLine($"You choose invalid option.", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("Select one of the options:", Console.ForegroundColor = ConsoleColor.Cyan);
            Console.ResetColor();
            return GetBookOrOwnerType();
    }
}

static void AddBookOrOwner(IRepository<Book> bookRepository, IRepository<BookOwner> bookOwnerRepository, entitiesType bookOrOwnerType)
{
    try
    {
        switch (bookOrOwnerType)
        {
            case entitiesType.BOOK:
                Book bookToAdd = GetDataBook();
                bookRepository.Add(bookToAdd);
                bookRepository.Save();
                Console.WriteLine($"Book was added.\n", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ResetColor();
                break;
            case entitiesType.OWNER:
                BookOwner bookOwnerToAdd = GetDataOwner();
                bookOwnerRepository.Add(bookOwnerToAdd);
                bookOwnerRepository.Save();
                Console.WriteLine($"Owner was added.\n", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ResetColor();
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"{e.Message}", Console.ForegroundColor = ConsoleColor.Red);
        Console.ResetColor();
        Console.WriteLine();
    }
}

static void RemoveBookOrOwner(IRepository<Book> bookRepository, IRepository<BookOwner> bookOwnerRepository, entitiesType bookOrOwnerType)
{
    Console.Write($"Select ID of {bookOrOwnerType} to remove:", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.ResetColor();
    try
    {
        int idToRemove = Int32.Parse(GetInputFromUser());
        switch (bookOrOwnerType)
        {
            case entitiesType.BOOK:
                Book bookToRemove = GetItemByID(bookRepository, idToRemove);
                bookRepository.Remove(bookToRemove);
                bookRepository.Save();
                Console.WriteLine($"Book was removed.\n", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ResetColor();
                break;
            case entitiesType.OWNER:
                BookOwner ownerToRemove = GetItemByID(bookOwnerRepository, idToRemove);
                bookOwnerRepository.Add(ownerToRemove);
                bookOwnerRepository.Save();
                Console.WriteLine($"Owner was removed.\n", Console.ForegroundColor = ConsoleColor.Yellow);
                Console.ResetColor();
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"{e.Message}", Console.ForegroundColor = ConsoleColor.Red);
        Console.ResetColor();
        Console.WriteLine();
    }
}

static void ShowByIdBookOrOwner(IRepository<Book> bookRepository, IRepository<BookOwner> bookOwnerRepository, entitiesType bookOrOwnerType)
{
    Console.Write($"Select ID of {bookOrOwnerType} to show:", Console.ForegroundColor = ConsoleColor.Cyan);
    Console.ResetColor();
    try
    {
        int idItem = Int32.Parse(GetInputFromUser());
        switch (bookOrOwnerType)
        {
            case entitiesType.BOOK:
                Book bookToShow = GetItemByID(bookRepository, idItem);
                Console.WriteLine(bookToShow);
                Console.WriteLine();
                break;
            case entitiesType.OWNER:
                BookOwner ownerToShow = GetItemByID(bookOwnerRepository, idItem);
                Console.WriteLine(ownerToShow);
                Console.WriteLine();
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"{e.Message}", Console.ForegroundColor = ConsoleColor.Red);
        Console.ResetColor();
        Console.WriteLine();
    }
}

static void ShowAllDataBookOrOwner(IRepository<Book> bookRepository, IRepository<BookOwner> bookOwnerRepository, entitiesType bookOrOwnerType)
{
    switch (bookOrOwnerType)
    {
        case entitiesType.BOOK:
            var itemsBook = bookRepository.GetAll();
            foreach (var item in itemsBook)
            {
                Console.WriteLine(item);
            }
            break;
        case entitiesType.OWNER:
            var itemsOwner = bookOwnerRepository.GetAll();
            foreach (var item in itemsOwner)
            {
                Console.WriteLine(item);
            }
            break;
    }
    Console.WriteLine("The available data was displayed.\n", Console.ForegroundColor = ConsoleColor.Yellow);
    Console.ResetColor();
    Console.WriteLine();
}


enum repositoryType
{
    IN_MEMORY_SQL,
    JSON_FILE
}

enum entitiesType
{
    BOOK,
    OWNER
}