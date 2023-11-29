using BookApp.Repositories;
using BookApp.Entities;
using BookApp.DataProviders;

namespace BookApp.Services
{
    internal class EventHandler : IEventHandler
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<BookOwner> _bookOwnerRepository;

        public EventHandler(IRepository<Book> bookRepository, IRepository<BookOwner> bookOwnerRepository)
        {
            _bookRepository = bookRepository;
            _bookOwnerRepository = bookOwnerRepository;
        }

        public void Subscribe()
        {
            _bookRepository.ItemAdded += OnItemAddedSubscribeEvent;
            _bookRepository.ItemRemoved += OnItemRemovedSubscribeEvent;
            _bookOwnerRepository.ItemAdded += OnItemAddedSubscribeEvent;
            _bookOwnerRepository.ItemRemoved += OnItemRemovedSubscribeEvent;
        }

        static void OnItemRemovedSubscribeEvent(object? sender, IEntity e)
        {
            if (sender is not null)
            {
                var senderName = sender.GetType().Name;
                SaveToLogFile($"{senderName.Substring(0, senderName.Length - 2)}", $"{e.GetType().Name}Removed", e.ToString() ?? "");
            }
        }

        static void OnItemAddedSubscribeEvent(object? sender, IEntity e)
        {
            if (sender is not null)
            {
                var senderName = sender.GetType().Name;
                SaveToLogFile($"{senderName.Substring(0, senderName.Length - 2)}", $"{e.GetType().Name}Added", e.ToString() ?? "");
            }
        }

        static void SaveToLogFile(string repositoryType, string action, string comment)
        {
            using (var writer = File.AppendText($"BookAppLog.txt"))
            {
                writer.WriteLine($"[{DateTime.Now}]-{repositoryType}-{action}-[{comment}]");
            }
        }

    }
}
