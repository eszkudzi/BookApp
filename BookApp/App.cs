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
            _xmlReader.QueryXml();
            _eventHandler.Subscribe();
            _userCommunication.Communication();
        }
    }
}
