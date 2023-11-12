using BookApp.Components.CsvReader.Models;

namespace BookApp.Components.CsvReader.Extensions
{
    public static class RatingsExtensions
    {
        public static IEnumerable<Ratings> ToRatings(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                yield return new Ratings
                {
                    UserId = int.Parse(columns[0]),
                    Isbn = columns[1],
                    Rating = int.Parse(columns[2])
                };
            }
        }
    }
}
