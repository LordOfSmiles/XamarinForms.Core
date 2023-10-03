using System.Linq;
using System.Linq.Expressions;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public abstract class DbRepositoryBase<TDto, TDb> : IDbRepositoryBase<TDto, TDb>
    where TDb : DbEntity, new()
    where TDto : class
{
    public abstract TDto ToDto(TDb db);

    public abstract TDb ToDb(TDto dto);

    public virtual TDto AddOrUpdate(TDto item) => SqliteWriter.AddOrUpdate(DbContext.Connection, item, ToDb, ToDto);

    public void AddOrUpdate(IEnumerable<TDto> items) => SqliteWriter.AddOrUpdate(DbContext.Connection, items, ToDb, ToDto);

    public void Delete() => SqliteWriter.Delete<TDb>(DbContext.Connection);

    public void Delete(int primaryKey) => SqliteWriter.Delete<TDb>(DbContext.Connection, primaryKey);

    public void Delete(Expression<Func<TDb, bool>> predicate) => SqliteWriter.Delete<TDb>(DbContext.Connection, predicate);

    public TDto Find(object key) => SqliteReader.Find<TDb, TDto>(DbContext.Connection, key, ToDto);

    public TDto Find(Expression<Func<TDb, bool>> predicate) => SqliteReader.Find(DbContext.Connection, predicate, ToDto);

    public TDto FirstOrDefault() => SqliteReader.FirstOrDefault<TDb, TDto>(DbContext.Connection, ToDto);

    public TDto LastOrDefault() => SqliteReader.LastOrDefault<TDb, TDto>(DbContext.Connection, ToDto);

    public IReadOnlyList<TDto> All(Expression<Func<TDb, bool>> predicate = null)
    {
        return predicate == null
                   ? SqliteReader.All<TDb, TDto>(DbContext.Connection, ToDto)
                   : SqliteReader.All(DbContext.Connection, predicate, ToDto);
    }

    public bool Any(Func<TDb, bool> predicate = null)
    {
        return predicate == null
                   ? SqliteReader.Any<TDb>(DbContext.Connection)
                   : SqliteReader.Any(DbContext.Connection, predicate);
    }

    public int Count(Func<TDb, bool> predicate = null)
    {
        return predicate == null
                   ? SqliteReader.Count<TDb>(DbContext.Connection)
                   : SqliteReader.Count(DbContext.Connection, predicate);
    }

    #region Fields

    protected BaseDbContext DbContext { get; }

    #endregion

    protected DbRepositoryBase(BaseDbContext dbContext)
    {
        DbContext = dbContext;
    }
}

public interface IDbRepositoryBase<TDto, TDb>
    where TDb : DbEntity, new()
    where TDto : class
{
    TDto ToDto(TDb db);

    TDb ToDb(TDto dto);

    TDto AddOrUpdate(TDto item);

    void AddOrUpdate(IEnumerable<TDto> items);

    void Delete();

    void Delete(int primaryKey);

    void Delete(Expression<Func<TDb, bool>> predicate);

    TDto Find(object key);

    TDto Find(Expression<Func<TDb, bool>> predicate);

    IReadOnlyList<TDto> All(Expression<Func<TDb, bool>> predicate = null);

    bool Any(Func<TDb, bool> predicate = null);

    int Count(Func<TDb, bool> predicate = null);
}