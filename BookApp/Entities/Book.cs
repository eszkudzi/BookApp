namespace BookApp.Entities
{
    public class Book : EntityBase
    {
        public Book()
        { }
        public string? Name { get; set; }
        public override string ToString() => $"Id: {Id}, FirstName: {Name}";
    }
}
