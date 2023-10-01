﻿using BookApp.Entities;
using System.Text.Json;

namespace BookApp.Repositories
{
    public class FileRepository<T> : IRepository<T>
    where T : class, IEntity
    {
        private readonly List<T> _itemsSet = new();
        private readonly List<T> _itemsToAdd = new();
        private readonly List<int> _itemsIdToRemove = new();
        private readonly string _directory = $"Repository";
        private readonly string _path = $".\\Repository\\{typeof(T).Name}.txt";
        public FileRepository()
        {
        }
        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
        public event EventHandler? ItemsSaved;
        public void Add(T item)
        {
            item.Id = _itemsToAdd.Count + 1;
            _itemsToAdd.Add(item);
            ItemAdded?.Invoke(this, item);
        }

        public IEnumerable<T> GetAll()
        {
            _itemsSet.Clear();
            CreateFileIfNotExist();

            List<string> jsonItemsAll = File.ReadAllLines(_path).ToList();
            foreach (var jsonItem in jsonItemsAll)
            {
                if (jsonItem is not null)
                {
                    _itemsSet.Add(JsonSerializer.Deserialize<T>(jsonItem));
                }
            }
            return _itemsSet.ToList();
        }
        public T GetById(int id)
        {
            IEnumerable<T> items = GetAll();
            return items.Single(x => x.Id == id);
        }
        public void Remove(T item)
        {
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
            ItemsSaved?.Invoke(this, new EventArgs());
        }
        private void CreateFileIfNotExist()
        {
            if (!Directory.Exists(".\\" + _directory))
            {
                Directory.CreateDirectory(".\\" + _directory);
            }
            if (!File.Exists(_path))
            {
                using (File.Create(_path)) { };
            }
        }
    }
}