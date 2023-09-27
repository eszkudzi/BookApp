using BookApp.Entities;
using System.Text.Json;

namespace BookApp.Repositories
{
    public class FileRepository<T> : IRepository<T> 
        where T : class, IEntity
    {
        private readonly List<T> _items = new();
        private readonly List<T> _itemsToAdd = new();
        private readonly List<int> _itemsIdToRemove = new();
        private readonly string _directoryname = $"Repository";
        private readonly string _path = $".\\Repository\\{typeof(T).Name}.json";

        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
        public event EventHandler? ItemSaved;

        public FileRepository()
        {
        }

        public IEnumerable<T> GetAll()
        {
            _items.Clear();
            CreateFileIfNotExist();

            List<string> jsonItemsAll = File.ReadAllLines(_path).ToList();

            foreach (var jsonItem in jsonItemsAll)
            {
                if (jsonItem is not null)
                {
                    _items.Add(JsonSerializer.Deserialize<T>(jsonItem));
                }
            }

            return _items.ToList();
        }

        public T GetById(int id)
        {
            IEnumerable<T> items = GetAll();
            return _items.Single(item => item.Id == id);
        }

        public void Add(T item)
        {
            item.Id = _itemsToAdd.Count + 1;
            _itemsToAdd.Add(item);
            /* item.Id = _items.Count + 1;
             _items.Add(item);
            */
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            //_items.Remove(item);
            _itemsIdToRemove.Add(item.Id);
            ItemRemoved?.Invoke(this, item);
        }

        public void Save()
        {
            CreateFileIfNotExist();

            string tempFile = Path.GetTempFileName();

            using (var sReader = new StreamReader(_path))
            {
                using (var sWriter = new StreamWriter(tempFile))
                {
                    string readJson;
                    int id = 0;

                    while ((readJson = sReader.ReadLine()) != null)
                    {
                        var item = JsonSerializer.Deserialize<T>(readJson);
                        if (item is not null)
                        {
                            id = item.Id;
                            if (!_itemsIdToRemove.Contains(id))
                            {
                                sWriter.WriteLine(readJson);
                            }
                        }
                    }

                    foreach (var itemToAdd in _itemsToAdd)
                    {
                        itemToAdd.Id += id;
                        sWriter.WriteLine(JsonSerializer.Serialize<T>(itemToAdd));
                    }
                }
            }

            File.Delete(_path);
            File.Move(tempFile, _path);
            ItemSaved?.Invoke(this, new EventArgs());
        }

        private void CreateFileIfNotExist()
        {
            if (!Directory.Exists(".\\" + _directoryname))
            {
                Directory.CreateDirectory(".\\" + _directoryname);
            }

            if (!File.Exists(_path))
            {
                using (File.Create(_path)) { };
            }
        }
    }
}
