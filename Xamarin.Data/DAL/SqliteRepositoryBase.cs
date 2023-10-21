using System.Linq;
using System.Linq.Expressions;
using Xamarin.Data.Dto;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public abstract class SqliteRepositoryBase<TDb, TDto> : ISqliteRepositoryBase<TDb, TDto>
    where TDto : DtoBase
    where TDb : DbEntity, new()
{
    public TableQuery<TDb> Table()
    {
        return DbContext.Connection.Table<TDb>();
    }

    public abstract TDto ToDto(TDb db);

    public abstract TDb ToDb(TDto dto);

    public virtual TDto AddOrUpdate(TDto item)
    {
        if (item == null)
            return null;

        var dbItem = ToDb(item);

        try
        {
            // if (dbItem.DbId == 0)
            // {
            //     DbContext.Connection.Insert(dbItem, typeof(TDb));
            // }
            // else
            // {
            //     DbContext.Connection.Update(dbItem, typeof(TDb));
            // }
            var count = DbContext.Connection.InsertOrReplace(dbItem, typeof(TDb));
        }
        catch (Exception)
        {
            //
        }

        return ToDto(dbItem);
    }

    public void AddOrUpdate(IEnumerable<TDto> items)
    {
        if (items == null)
            return;

        foreach (var item in items)
        {
            AddOrUpdate(item);
        }
    }

    public void Delete()
    {
        try
        {
            DbContext.Connection.DeleteAll<TDb>();
        }
        catch (Exception)
        {
            //
        }
    }

    public void Delete(object primaryKey)
    {
        try
        {
            DbContext.Connection.Delete<TDb>(primaryKey);
        }
        catch (Exception)
        {
            //
        }
    }

    public void Delete(Expression<Func<TDb, bool>> predicate)
    {
        try
        {
            DbContext.Connection.Table<TDb>().Delete(predicate);
        }
        catch (Exception)
        {
            //
        }
    }

    public TDto Find(object key)
    {
        var dbItem = DbContext.Connection.Find<TDb>(key);

        return dbItem != null
                   ? ToDto(dbItem)
                   : null;
    }

    public TDto Find(Expression<Func<TDb, bool>> predicate)
    {
        var dbItem = DbContext.Connection.Find(predicate);

        return dbItem != null
                   ? ToDto(dbItem)
                   : null;
    }

    public TDto FirstOrDefault()
    {
        var dbItem = DbContext.Connection.Table<TDb>().FirstOrDefault();

        return dbItem != null
                   ? ToDto(dbItem)
                   : null;
    }

    public TDto LastOrDefault()
    {
        var dbItem = DbContext.Connection.Table<TDb>().LastOrDefault();

        return dbItem != null
                   ? ToDto(dbItem)
                   : null;
    }

    public IReadOnlyList<TDto> All(Expression<Func<TDb, bool>> predicate = null)
    {
        return DbContext.Connection.Table<TDb>()
                        .Select(ToDto)
                        .ToArray();
    }

    public bool Any(Func<TDb, bool> predicate = null)
    {
        return predicate == null
                   ? DbContext.Connection.Table<TDb>().Any()
                   : DbContext.Connection.Table<TDb>().Any(predicate);
    }

    public int Count(Func<TDb, bool> predicate = null)
    {
        return predicate == null
                   ? DbContext.Connection.Table<TDb>().Count()
                   : DbContext.Connection.Table<TDb>().Count(predicate);
    }

    #region Fields

    protected BaseDbContext DbContext { get; }

    #endregion

    protected SqliteRepositoryBase(BaseDbContext dbContext)
    {
        DbContext = dbContext;
    }
}

public interface ISqliteRepositoryBase<TDb, TDto>
    where TDto : DtoBase
    where TDb : DbEntity
{
    TableQuery<TDb> Table();
    
    TDto ToDto(TDb db);

    TDb ToDb(TDto dto);

    TDto AddOrUpdate(TDto item);

    void AddOrUpdate(IEnumerable<TDto> items);

    void Delete();

    void Delete(object primaryKey);

    void Delete(Expression<Func<TDb, bool>> predicate);

    TDto Find(object key);

    TDto Find(Expression<Func<TDb, bool>> predicate);

    IReadOnlyList<TDto> All(Expression<Func<TDb, bool>> predicate = null);

    bool Any(Func<TDb, bool> predicate = null);

    int Count(Func<TDb, bool> predicate = null);
}