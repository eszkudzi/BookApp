namespace BookApp.Entities
{
    public class BookSpecial : Book
    {
        public override string ToString() => base.ToString() + " (This is special book!)";
    }
}
