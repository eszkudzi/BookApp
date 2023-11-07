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
                    Authors = columns[7],
                    OriginalTitle = columns[9],
                    Title = columns[10],
                    AverageRating = double.Parse(columns[12]),
                    RatingsCount = int.Parse(columns[13]),
                    WorkRatingsCount = int.Parse(columns[14]),
                    WorkTextReviewsCount = int.Parse(columns[15]),
                    Ratings1 = int.Parse(columns[16]),
                    Ratings2 = int.Parse(columns[17]),
                    Ratings3 = int.Parse(columns[18]),
                    Ratings4 = int.Parse(columns[19]),
                    Ratings5 = int.Parse(columns[20]),
                    ImageUrl = columns[21],
                    SmallImageUrl = columns[22]
                };
            }

        }
    }
}
