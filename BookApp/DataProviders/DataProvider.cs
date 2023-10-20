using BookApp.Entities;
using BookApp.Repositories;

namespace BookApp.DataProviders
{
    public class DataProvider : IDataProvider
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BookOwner> _bookOwnerRepository;

        public DataProvider(IRepository<Book> bookRepository, IRepository<BookOwner> bookOwnerRepository)
        {
            _bookRepository = bookRepository;
            _bookOwnerRepository = bookOwnerRepository;
        }

        public List<string> GetUniqueBookOwnerLastName()
        {
            var bookOwner = _bookOwnerRepository.GetAll();
            var lastName = bookOwner.Select(x => x.LastName).Distinct().ToList();
            return lastName;
        }

        public List<string> GetUniqueBooksAuthor()
        {
            var book = _bookRepository.GetAll();
            var author = book.Select(x => x.Author).Distinct().ToList();
            return author;
        }

        public List<BookOwner> OrderBookOwnerByFirstNameAndLastName()
        {
            var bookOwner = _bookOwnerRepository.GetAll();
            return bookOwner.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
        }

        public List<BookOwner> OrderBookOwnerByLastName()
        {
            var bookOwner = _bookOwnerRepository.GetAll();
            return bookOwner.OrderBy(x => x.LastName).ToList();
        }

        public List<Book> OrderBooksByTitle()
        {
            var book = _bookRepository.GetAll();
            return book.OrderBy(x => x.Title).ToList();
        }

        public List<Book> OrderBooksByTitleAndAuthor()
        {
            var book = _bookRepository.GetAll();
            return book.OrderBy(x => x.Title).ThenBy(x => x.Author).ToList();
        }

        public List<BookOwner> SkipBookOwner(int howMany)
        {
            var bookOwner = _bookOwnerRepository.GetAll();
            return bookOwner.OrderBy(x => x.Id).Skip(howMany).ToList();
        }

        public List<BookOwner> SkipBookOwnerWhileLastNameStartsWith(string prefix)
        {
            var bookOwner = _bookOwnerRepository.GetAll();
            return bookOwner.OrderBy(x => x.Id).SkipWhile(x => x.LastName.StartsWith(prefix)).ToList();
        }

        public List<Book> SkipBooks(int howMany)
        {
            var book = _bookRepository.GetAll();
            return book.OrderBy(x => x.Id).Skip(howMany).ToList();
        }

        public List<Book> SkipBooksWhileTitleStartsWith(string prefix)
        {
            var book = _bookRepository.GetAll();
            return book.OrderBy(x => x.Id).SkipWhile(x => x.Title.StartsWith(prefix)).ToList();
        }

        public List<BookOwner> TakeBookOwners(int howMany)
        {
            var bookOwner = _bookOwnerRepository.GetAll();
            return bookOwner.OrderBy(x => x.Id).Take(howMany).ToList();
        }

        public List<BookOwner> TakeBookOwnerWhileLastNameStartsWith(string prefix)
        {
            var bookOwner = _bookOwnerRepository.GetAll();
            return bookOwner.OrderBy(x => x.Id).TakeWhile(x => x.LastName.StartsWith(prefix)).ToList();
        }

        public List<Book> TakeBooks(int howMany)
        {
            var book = _bookRepository.GetAll();
            return book.OrderBy(x => x.Id).Take(howMany).ToList();
        }

        public List<Book> TakeBooksWhileTitleStartsWith(string prefix)
        {
            var book = _bookRepository.GetAll();
            return book.OrderBy(x => x.Id).TakeWhile(x => x.Title.StartsWith(prefix)).ToList();
        }

        public List<BookOwner> WhereBookOwnerLastNameStartsWith(string prefix)
        {
            var bookOwner = _bookOwnerRepository.GetAll();
            return bookOwner.Where(x => x.LastName.StartsWith(prefix)).ToList();
        }

        public List<Book> WhereBooksTitleStartsWith(string prefix)
        {
            var book = _bookRepository.GetAll();
            return book.Where(x => x.Title.StartsWith(prefix)).ToList();

        }
    }
}
