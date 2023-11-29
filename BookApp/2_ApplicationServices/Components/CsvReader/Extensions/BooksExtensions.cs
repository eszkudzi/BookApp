using BookApp.Components.CsvReader.Models;

namespace BookApp.Components.CsvReader.Extensions
{
    public static class BooksExtensions
    {
        public static IEnumerable<Books> ToBooks(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                yield return new Books
                {
                    Isbn = columns[0],
                    Title = columns[1],
                    Author = columns[2],
                    YearOfPublication = int.Parse(columns[3]),
                    Publisher = columns[4],
                    ImageUrlS = columns[5],
                    ImageUrlM = columns[6],
                    ImageUrlL = columns[7]
                };
            }

        }
    }
}
