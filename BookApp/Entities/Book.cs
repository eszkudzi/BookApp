namespace BookApp.Entities
{
    public class Book : EntityBase
    {
        public Book()
        { }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? PublicationDate { get; set; }
    public override string ToString() => $"Id: {Id}, Title: {Title}, Author: {Author}, Publication date: {PublicationDate}";
    }
}
