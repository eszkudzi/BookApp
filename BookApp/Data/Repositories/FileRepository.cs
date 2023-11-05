using BookApp.Entities;
using System.Text.Json;

namespace BookApp.Repositories
{
    public class FileRepository<T> : IRepository<T>
    where T : class, IEntity
    {
        private readonly List<T> _items = new();
        private readonly string _folder = $"FileRepository";
        private readonly string _path = $".\\FileRepository\\{typeof(T).Name}.json";
        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
        public event EventHandler? ItemSaved;
        public FileRepository()
        {
        }
        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
            ItemAdded?.Invoke(this, item);
        }
        public void Remove(T item)
        {
            _items.Remove(item);
            ItemRemoved?.Invoke(this, item);
        }
        public T GetById(int id)
        {
            IEnumerable<T> items = GetAll();
            return items.Single(x => x.Id == id);
        }
        public IEnumerable<T> GetAll()
        {
            _items.Clear();
            CreateFile();

            List<string> jsonAll = File.ReadAllLines(_path).ToList();
            foreach (var json in jsonAll)
            {
                if (json is not null)
                {
                    _items.Add(JsonSerializer.Deserialize<T>(json));
                }
            }
            return _items.ToList();
        }
        public void Save()
        {
            CreateFile();

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

                    }
                    foreach (var itemSerialize in _items)
                    {
                        itemSerialize.Id += id;
                        sWriter.WriteLine(JsonSerializer.Serialize<T>(itemSerialize));
                    }
                }
            }
            File.Delete(_path);
            File.Move(tempFile, _path);
            ItemSaved?.Invoke(this, new EventArgs());
        }
        private void CreateFile()
        {
            if (!Directory.Exists(".\\" + _folder))
            {
                Directory.CreateDirectory(".\\" + _folder);
            }
            if (!File.Exists(_path))
            {
                using (File.Create(_path)) { };
            }
        }
    }
}