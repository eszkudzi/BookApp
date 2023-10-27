using BookApp.Entities;

namespace BookApp.DataProviders
{
    public interface IDataProvider
    {
        //select
        List<string> GetUniqueBooksAuthor();
        List<string> GetUniqueBookOwnerLastName();

        //order by
        List<Book> OrderBooksByTitle();
        List<BookOwner> OrderBookOwnerByLastName();

        //where
        List<Book> WhereBooksTitleStartsWith(string prefix);
        List<BookOwner> WhereBookOwnerLastNameStartsWith(string prefix);

        //take
        List<Book> TakeBooks(int howMany);
        List<BookOwner> TakeBookOwners(int howMany);

        //skip
        List<Book> SkipBooks(int howMany);
        List<BookOwner> SkipBookOwner(int howMany);
    }
}
