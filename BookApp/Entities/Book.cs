namespace BookApp.Entities
{
    public class Book : EntityBase
    {
        public Book()
        { }

    public override string ToString() => $"Id: {Id}, Title: {Title}, Author: {Author}, Publication date: {PublicationDate}";
    }
}
