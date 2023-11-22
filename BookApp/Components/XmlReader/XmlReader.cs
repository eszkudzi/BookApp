using BookApp.Components.CsvReader;
using BookApp.Components.CsvReader.Models;
using System.Xml.Linq;


namespace BookApp.Components.XmlReader
{
    public class XmlReader : IXmlReader
    {
        public void CreateXml(List<Books> recordsBooks, List<Ratings> recordsRatings)
        {
            var document = new XDocument();
            var books = new XElement("Ratings", recordsRatings
                .Select(r =>
                new XElement("Rating",
                    new XAttribute("UserId", r.UserId),
                    new XAttribute("Isbn", r.Isbn),
                    recordsBooks
                    .Where(c => c.Isbn == r.Isbn)
                    .GroupBy(c => c.Isbn)
                    .Select(x =>
                    new XElement("Books",
                        new XAttribute("ISBN", r.Isbn),
                        new XAttribute("CombinedSum", x.Sum(c => c.YearOfPublication)),
                        x
                        .Select(c =>
                        new XElement("Book",
                            new XAttribute("Title", c.Title),
                            new XAttribute("Combined", c.YearOfPublication)
                       )))))));

            document.Add(books);
            document.Save("booksWithRatings.xml");
        }

        public void QueryXml(string fileName)
        {
            var document = XDocument.Load(fileName);
            var booksWithHighRating = document
                .Element("Books")?
                .Elements("Book")
                .Where(x => x.Attribute("Rating")?.Value == "5")
                .Select(x => x.Attribute("ISBN")?.Value);

            Console.WriteLine("List of 5 ratings with books:");
            foreach (var book in booksWithHighRating)
            {
                Console.WriteLine(book);
            }

        }
    }
}
