using BookApp.Components.CsvReader;
using BookApp.Components.CsvReader.Models;
using System.Linq;
using System.Xml.Linq;

namespace BookApp.Components.XmlReader
{
    internal class XmlReader : IXmlReader
    {
        private readonly ICsvReader _csvReader;
        private List<Books> _booksRecords;
        private List<Ratings> _ratingsRecords;

        public XmlService(ICsvReader csvReader)
        {
            _csvReader = csvReader;
            _booksRecords = _csvReader.ProcessBooks("Resources\\Files\\books.csv");
            _ratingsRecords = _csvReader.ProcessRatings("Resources\\Files\\ratings.csv");
        }


        public void CreateXml()
        {
            // DO POPRAWIENIA

            var document = new XDocument();
            var booksWithRatings = new XElement("Books", books
                .Select(x =>
                new XElement("Book",
                new XAttribute("ID", x.Manufacturer),
                new XAttribute("ISBN", x.Combined),
                new XAttribute("Authors", x.Manufacturer),
                new XAttribute("Rating", x.Manufacturer)
                )
                ));

            document.Add(booksWithRatings);
            document.Save("booksWithRatings.xml");
        }

        public void QueryXml()
        {
            var document = XDocument.Load("booksWithRatings.xml");
            var booksWithHighRating = document
                .Element("Books")?
                .Elements("Book")
                .Where(x => x.Attribute("Rating")?.Value == "5")
                .Select(x => x.Attribute("BookId")?.Value);

            Console.WriteLine("List of books with only high rating (5 stars):");
            foreach (var book in booksWithHighRating)
            {
                Console.WriteLine(book);
            }

        }
    }
}
