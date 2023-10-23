using BookApp.Data;
using BookApp.DataProviders;
using BookApp.Entities;
using BookApp.Repositories;

namespace BookApp.Services
{
    internal class UserCommunication : IUserCommunication
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BookOwner> _bookOwnerRepository;
        private readonly IDataProvider _dataProvider;
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

        public UserCommunication(IRepository<Book> bookRepository, IRepository<BookOwner> bookOwnerRepository, IDataProvider dataProvider)
        {
            _bookRepository = bookRepository;
            _bookOwnerRepository = bookOwnerRepository;
            _dataProvider = dataProvider;
        }

        public void Intro()
        {
            Console.WriteLine("--------------------------------------------------------", Console.BackgroundColor = ConsoleColor.Cyan, Console.ForegroundColor = ConsoleColor.Black);
            Console.WriteLine("---  HELLO! THIS IS BOOK APP - DIGITAL BOOK ARCHIVE  ---");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Data stored in memory. Data will not be readable after closing the application.");
            Console.WriteLine("--------------------------------------------------------");
            Console.ResetColor();
        }

        public void Communication()
        {
            Intro();

            bool closeApp = true;
            string? action = GetInputFromUser();

            while (closeApp)
            {
                Console.WriteLine("What do you want to do?\n"
                    + "1 - Add Books or Owners\n"
                    + "2 - Remove Books or Owners\n"
                    + "3 - Show Books or Owners by ID\n"
                    + "4 - View all Books or Owners data\n"
                    + "5 - Get unique authors of Books or last name of Owners\n"
                    + "6 - Order Books or Owners data\n"
                    + "7 - Show where Books title or Books Owners last name starts with\n"
                    + "8 - Take Books or Owners data\n"
                    + "9 - Skip Books or Owners data\n"
                    + "X - Close app", Console.ForegroundColor = ConsoleColor.Cyan);
                Console.ResetColor();


                if ((action != "1") && (action != "2") && (action != "3") && (action != "4") && (action != "5") && (action != "6") && (action != "7") && (action != "8") && (action != "9") && (action.ToUpper() != "X"))
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
                        AddBookOrOwner(_bookRepository, _bookOwnerRepository, bookOrOwnerType);
                        break;

                    case "2":
                        RemoveBookOrOwner(_bookRepository, _bookOwnerRepository, bookOrOwnerType);
                        break;

                    case "3":
                        ShowByIdBookOrOwner(_bookRepository, _bookOwnerRepository, bookOrOwnerType);
                        break;

                    case "4":
                        ShowAllDataBookOrOwner(_bookRepository, _bookOwnerRepository, bookOrOwnerType);
                        break;

                    case "5":
                        GetUniqueBookAuthorsOrOwnerNames(_dataProvider, bookOrOwnerType);
                        break;
                    case "6":
                        OrderDataBookOrOwner(_dataProvider, bookOrOwnerType);
                        break;
                    case "7":
                        ShowWhereBookTitleOrBookOwnerLastNameStartsWith(_dataProvider, bookOrOwnerType);
                        break;
                    case "8":
                        TakeDataBookOwner(_dataProvider, bookOrOwnerType);
                        break;
                    case "9":
                        SkipDataBookOwner(_dataProvider, bookOrOwnerType);
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

        }

        private void SkipDataBookOwner(IDataProvider dataProvider, entitiesType bookOrOwnerType)
        {
            throw new NotImplementedException();
        }

        private void TakeDataBookOwner(IDataProvider dataProvider, entitiesType bookOrOwnerType)
        {
            throw new NotImplementedException();
        }

        private void ShowWhereBookTitleOrBookOwnerLastNameStartsWith(IDataProvider dataProvider, entitiesType bookOrOwnerType)
        {
            throw new NotImplementedException();
        }

        private void OrderDataBookOrOwner(IDataProvider dataProvider, entitiesType bookOrOwnerType)
        {
            throw new NotImplementedException();
        }

        private void GetUniqueBookAuthorsOrOwnerNames(IDataProvider dataProvider, entitiesType bookOrOwnerType)
        {
            throw new NotImplementedException();
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


        enum entitiesType
        {
            BOOK,
            OWNER
        }


    }
}
