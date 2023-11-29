using BookApp.Components.CsvReader.Models;

namespace BookApp.Components.CsvReader
{
    public interface ICsvReader
    {
        List<Books> ProcessBooks(string filePath);
        List<Ratings> ProcessRatings(string filePath);
    }
}
