using System.Linq;
using System.Linq.Expressions;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public abstract class DbRepositoryBaseOld2<TDto, TDb> : IDbRepositoryBaseOld2<TDto, TDb>
    where TDto : class, new()
    where TDb : DbEntity_Old, new()
{
    public TableQuery<TDb> QueryToTable => Db.Connection.Table<TDb>();

    public abstract TDto ToDto(TDb db);

    public abstract TDb ToDb(TDto dto);

    public virtual void AddOrUpdate(TDto item) => SqliteWriter.AddOrUpdate(Db.Connection, item, ToDb);

    public void AddOrUpdate(IEnumerable<TDto> items) => SqliteWriter.AddOrUpdate(Db.Connection, items, ToDb);

    public void Delete() => SqliteWriter.Delete<TDb>(Db.Connection);

    public void Delete(int primaryKey) => SqliteWriter.Delete<TDb>(Db.Connection, primaryKey);

    public void Delete(Expression<Func<TDb, bool>> predicate) => SqliteWriter.Delete<TDb>(Db.Connection, predicate);

    public TDto Find(object key)
    {
        var item = Db.Connection.Find<TDb>(key);

        return item != null
                   ? ToDto(item)
                   : null;
    }
    
    public TDto Find(int key)
    {
        var item = Db.Connection.Find<TDb>(key);

        return item != null
                   ? ToDto(item)
                   : null;
    }

    public TDto Find(Expression<Func<TDb, bool>> predicate)
    {
        var item = Db.Connection.Find(predicate);

        return item != null
                   ? ToDto(item)
                   : null;
    }

    public TDto FirstOrDefault()
    {
        var item = Db.Connection.Table<TDb>().FirstOrDefault();

        return item != null
                   ? ToDto(item)
                   : null;
    }

    public TDto LastOrDefault()
    {
        var item = Db.Connection.Table<TDb>().LastOrDefault();

        return item != null
                   ? ToDto(item)
                   : null;
    }

    public IReadOnlyList<TDto> All()
    {
        return Db.Connection.Table<TDb>().Select(ToDto).ToArray();
    }

    public IReadOnlyList<TDto> Filter(Expression<Func<TDb, bool>> predicate = null)
    {
        var items = predicate == null
                        ? Db.Connection.Table<TDb>()
                        : Db.Connection.Table<TDb>().Where(predicate);

        return items.Select(ToDto).ToArray();
    }

    public bool Any(Func<TDb, bool> predicate = null)
    {
        return predicate == null
                   ? Db.Connection.Table<TDb>().Any()
                   : Db.Connection.Table<TDb>().Any(predicate);
    }

    public int Count(Func<TDb, bool> predicate = null)
    {
        return predicate == null
                   ? Db.Connection.Table<TDb>().Count()
                   : Db.Connection.Table<TDb>().Count(predicate);
    }

    #region Fields

    protected BaseDbContext Db { get; }

    #endregion

    protected DbRepositoryBaseOld2(BaseDbContext db)
    {
        Db = db;
    }
}

public interface IDbRepositoryBaseOld2<TDto, TDb>
    where TDto : class, new()
    where TDb : DbEntity_Old, new()
{
    TableQuery<TDb> QueryToTable { get; }
    
    TDto ToDto(TDb db);

    TDb ToDb(TDto dto);

    void AddOrUpdate(TDto item);

    void AddOrUpdate(IEnumerable<TDto> items);

    void Delete();

    void Delete(int primaryKey);

    void Delete(Expression<Func<TDb, bool>> predicate);

    TDto Find(object key);
    TDto Find(int key);

    TDto Find(Expression<Func<TDb, bool>> predicate);

    TDto FirstOrDefault();

    TDto LastOrDefault();

    IReadOnlyList<TDto> All();

    IReadOnlyList<TDto> Filter(Expression<Func<TDb, bool>> predicate = null);

    bool Any(Func<TDb, bool> predicate = null);

    int Count(Func<TDb, bool> predicate = null);
}