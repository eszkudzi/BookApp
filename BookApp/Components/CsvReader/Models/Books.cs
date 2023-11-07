namespace BookApp.Components.CsvReader.Models
{
    public class Books
    {
        public int BookId { get; set; }
        public int GoodreadsBookId { get; set; }
        public int BestBookId { get; set; }
        public int WorkId { get; set; }
        public int BooksCount { get; set; }
        public int Isbn { get; set; }
        public double Isbn13 { get; set; }
        public string Authors { get; set; }

    }
}
