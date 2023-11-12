using BookApp.Components.CsvReader;
using BookApp.Components.XmlReader;
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

        public App(IUserCommunication userCommunication,IEventHandler eventHandler, ICsvReader csvReader, IXmlReader xmlReader)
        {
            _userCommunication = userCommunication;
            _eventHandler = eventHandler;
            _csvReader = csvReader;
            _xmlReader = xmlReader;
        }
        public void Run()
        {
            var _books = _csvReader.ProcessBooks("Resources\\Files\\books.csv");
            var _ratings = _csvReader.ProcessRatings("Resources\\Files\\ratings.csv");

            _xmlReader.CreateXml(_books, _ratings);

            _eventHandler.Subscribe();
            _userCommunication.Communication();
        }
    }
}
