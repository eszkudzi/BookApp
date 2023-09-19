namespace BookApp.Repositories
{
    public interface IRepository : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {

    }
}
