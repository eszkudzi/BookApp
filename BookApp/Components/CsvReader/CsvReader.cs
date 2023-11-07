using BookApp.Components.CsvReader.Extensions;
using BookApp.Components.CsvReader.Models;

namespace BookApp.Components.CsvReader
{
    public class CsvReader : ICsvReader
    {
        List<Books> ICsvReader.ProcessBooks(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Books>();
            }

            var items = File.ReadAllLines(filePath)
                .Skip(1)
                .Where(x => x.Length > 1)
                .ToBooks();

            return items.ToList();
        }

        List<Ratings> ICsvReader.ProcessRatings(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<Ratings>();
            }

            var items = File.ReadAllLines(filePath)
                .Skip(1)
                .Where(x => x.Length > 1)
                .ToRatings();

            return items.ToList();
        }
    }
}
