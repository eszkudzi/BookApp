using BookApp.Components.CsvReader;
using BookApp.Components.CsvReader.Models;
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
            var bookDetailsWithRating = _booksRecords.Join(
                 _ratingsRecords,
                 (books => books.Isbn),
                 (ratings => ratings.Isbn),
                 (_booksRecords, _ratingsRecords) =>
                     new
                     {
                         _booksRecords.Isbn,
                         _booksRecords.Title,
                         _booksRecords.Author,
                         _booksRecords.YearOfPublication,
                         _booksRecords.Publisher,
                         _ratingsRecords.Rating
                     });

            var document = new XDocument();
            var booksWithRatings = new XElement("Books", bookDetailsWithRating
                 .Select(x =>
                 new XElement("Book",
                 new XAttribute("ISBN", x.Isbn),
                 new XAttribute("Title", x.Title),
                 new XAttribute("Author", x.Author),
                 new XAttribute("YearOfPublication", x.YearOfPublication),
                 new XAttribute("Publisher", x.Publisher),
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
                .Select(x => x.Attribute("ISBN")?.Value);

            Console.WriteLine("List of books wit rating:");
            foreach (var book in booksWithHighRating)
            {
                Console.WriteLine(book);
            }

        }
    }
}
