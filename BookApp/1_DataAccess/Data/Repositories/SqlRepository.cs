using BookApp.Data;
using BookApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookApp.Repositories
{
    public class SqlRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        private readonly DbSet<T> _dbSet;
        private readonly BookAppDbContext _BookAppDbContext;
        public event EventHandler<T>? ItemAdded;
        public event EventHandler<T>? ItemRemoved;
        public event EventHandler? ItemSaved;

        public SqlRepository(BookAppDbContext bookAppDbContext)
        {
            _BookAppDbContext = bookAppDbContext;
            _dbSet = _BookAppDbContext.Set<T>();
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
            Save();
            ItemAdded?.Invoke(this, item);
        }
        public void Remove(T item)
        {
            _dbSet.Remove(item);
            Save();
            ItemRemoved?.Invoke(this, item);
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public void Save()
        {
            _BookAppDbContext.SaveChanges();
            ItemSaved?.Invoke(this, new EventArgs());
        }

        public void EnsureCreated()
        {
            _BookAppDbContext.Database.EnsureCreated();
        }
        public IEnumerable<T> Read()
        {
            return _dbSet.ToList();
        }
    }
}
