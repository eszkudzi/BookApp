namespace BookApp.Entities
{
    public class Book : EntityBase
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public Book()
        { }
        public override string ToString() => $"Id: {Id}, Title: {Title}, Author: {Author}";
    }
}
