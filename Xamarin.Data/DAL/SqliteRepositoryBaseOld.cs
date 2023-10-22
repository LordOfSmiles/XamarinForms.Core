using System.Linq;
using System.Linq.Expressions;
using Xamarin.Data.Dto;
using Xamarin.Data.Interfaces;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public abstract class SqliteRepositoryBaseOld<TDb, TDto> : ISqliteRepositoryBaseOld<TDb, TDto>
    where TDto : DtoBase
    where TDb : DbEntity_Old, new()
{
    public TableQuery<TDb> QueryToTable => DbContext.Connection.Table<TDb>();

    public abstract TDto ToDto(TDb db);

    public abstract TDb ToDb(TDto dto);

    public virtual void AddOrUpdate(TDto dto)
    {
        if (dto == null)
            return;

        var dbItem = ToDb(dto);

        try
        {
            if (dbItem.DbId == 0)
            {
                DbContext.Connection.Insert(dbItem, typeof(TDb));
                dto.DbId = dbItem.DbId;
            }
            else
            {
                DbContext.Connection.Update(dbItem, typeof(TDb));
            }
        }
        catch (Exception)
        {
            //
        }
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
        var query = DbContext.Connection.Table<TDb>();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }
        
        return query.Select(ToDto).ToArray();
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

    protected SqliteRepositoryBaseOld(BaseDbContext dbContext)
    {
        DbContext = dbContext;
    }
}

public interface ISqliteRepositoryBaseOld<TDb, TDto>
    where TDto : DtoBase
    where TDb : DbEntity_Old
{
    TableQuery<TDb> QueryToTable { get; }

    TDto ToDto(TDb db);

    TDb ToDb(TDto dto);

    void AddOrUpdate(TDto dto);

    void AddOrUpdate(IEnumerable<TDto> items);

    void Delete();

    void Delete(object primaryKey);

    void Delete(Expression<Func<TDb, bool>> predicate);

    TDto Find(object key);

    TDto Find(Expression<Func<TDb, bool>> predicate);

    TDto FirstOrDefault();

    TDto LastOrDefault();

    IReadOnlyList<TDto> All(Expression<Func<TDb, bool>> predicate = null);

    bool Any(Func<TDb, bool> predicate = null);

    int Count(Func<TDb, bool> predicate = null);
}