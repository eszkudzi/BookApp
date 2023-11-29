using BookApp.Components.CsvReader.Models;

namespace BookApp.Components.XmlReader
{
    public interface IXmlReader
    {
        public void CreateXml(List<Books> books, List<Ratings> ratings);
        public void QueryXml(string fileName);
    }
}
