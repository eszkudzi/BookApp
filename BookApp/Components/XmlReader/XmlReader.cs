using BookApp.Components.CsvReader;
using BookApp.Components.CsvReader.Models;
using System.Linq;
using System.Xml.Linq;

namespace BookApp.Components.XmlReader
{
    public class XmlReader : IXmlReader
    {
        private readonly ICsvReader _csvReader;
        private List<Books> _booksRecords;
        private List<Ratings> _ratingsRecords;

        public XmlReader(ICsvReader csvReader)
        {
            _csvReader = csvReader;
            _booksRecords = _csvReader.ProcessBooks("Resources\\Files\\books.csv");
            _ratingsRecords = _csvReader.ProcessRatings("Resources\\Files\\ratings.csv");
        }


        public void CreateXml()
        {
            var ratingsWithBooksDetails = _ratingsRecords.Join(
                _booksRecords,
                x => x.BookId,
                x => x.BookId,
                (_ratingsRecords, _booksRecords) =>
                    new 
                    {
                        _ratingsRecords.BookId,
                        _booksRecords.Isbn,
                        _booksRecords.Authors,
                        _booksRecords.Title,
                        _ratingsRecords.Rating
                    })
                .OrderByDescending(x=>x.Rating);


            var document = new XDocument();
            var booksWithRatings = new XElement("Books", ratingsWithBooksDetails
                .Select(x =>
                new XElement("Book",
                new XAttribute("IdBook", x.BookId),
                new XAttribute("ISBN", x.Isbn),
                new XAttribute("Authors", x.Authors),
                new XAttribute("Title", x.Title),
                new XAttribute("Rating", x.Rating)
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
                .Select(x => x.Attribute("IdBook")?.Value);

            Console.WriteLine("List of books with only high rating (5 stars):");
            foreach (var book in booksWithHighRating)
            {
                Console.WriteLine(book);
            }

        }
    }
}
