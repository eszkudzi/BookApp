namespace BookApp.Entities
{
    public class BookOwner : EntityBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public override string ToString() => $"Id: {Id}, FirstName: {FirstName}, LastName: {LastName}";
    }
}
