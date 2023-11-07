using BookApp.Components.CsvReader.Models;

namespace BookApp.Components.CsvReader.Extensions
{
    public static class BooksExtensions
    {
        public static IEnumerable<Books> ToBooks(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Books
                {
                    BookId = int.Parse(columns[0]),
                    GoodreadsBookId = int.Parse(columns[1]),
                    BestBookId = int.Parse(columns[2]),
                    WorkId = int.Parse(columns[3]),
                    BooksCount = int.Parse(columns[4]),
                    Isbn = int.Parse(columns[5]),
                    Isbn13 = double.Parse(columns[6]),
                    Authors = columns[7]
                };
            }

        }
    }
}
