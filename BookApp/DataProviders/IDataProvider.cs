using BookApp.Entities;

namespace BookApp.DataProviders
{
    public interface IDataProvider
    {
        //select
        List<string> GetUniqueBooksAuthor();
        List<string> GetUniqueBookOwnerLastName();

        //other by
        List<Book> OrderBooksByTitle();
        List<Book> OrderBooksByTitleAndAuthor();
        List<BookOwner> OrderBookOwnerByLastName();
        List<BookOwner> OrderBookOwnerByFirstNameAndLastName();

        //where
        List<Book> WhereBooksTitleStartsWith(string prefix);
        List<BookOwner> WhereBookOwnerLastNameStartsWith(string prefix);

        //take
        List<Book> TakeBooks(int howMany);
        List<Book> TakeBooksWhileTitleStartsWith(string prefix);
        List<BookOwner> TakeBookOwners(int howMany);
        List<BookOwner> TakeBookOwnerWhileLastNameStartsWith(string prefix);

        //skip
        List<Book> SkipBooks(int howMany);
        List<Book> SkipBooksWhileTitleStartsWith(string prefix);
        List<BookOwner> SkipBookOwner(int howMany);
        List<BookOwner> SkipBookOwnerWhileLastNameStartsWith(string prefix);
    }
}
