using BookApp.Components.CsvReader;
using BookApp.Components.XmlReader;
using BookApp.Data;
using BookApp.Services;
using System.Formats.Asn1;

namespace BookApp
{
    internal class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly IEventHandler _eventHandler;

        private readonly ICsvReader _csvReader;
        private readonly IXmlReader _xmlReader;

        private readonly BookAppDbContext _bookAppDbContext;

        public App(IUserCommunication userCommunication,IEventHandler eventHandler, ICsvReader csvReader, IXmlReader xmlReader, BookAppDbContext bookAppDbContext)
        {
            _userCommunication = userCommunication;
            _eventHandler = eventHandler;
            _csvReader = csvReader;
            _xmlReader = xmlReader;

            _bookAppDbContext = bookAppDbContext;
            _bookAppDbContext.Database.EnsureCreated();
        }
        public void Run()
        {
            //var _books = _csvReader.ProcessBooks("Resources\\Files\\books.csv");
            //var _ratings = _csvReader.ProcessRatings("Resources\\Files\\ratings.csv");

            _eventHandler.Subscribe();
            _userCommunication.Communication();
            //_xmlReader.CreateXml(_books, _ratings);
        }
    }
}
