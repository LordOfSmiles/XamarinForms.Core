using System.Linq;
using System.Linq.Expressions;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public abstract class DbRepositoryBase2<TDto, TDb> : IDbRepositoryBase2<TDto, TDb>
    where TDb : DbEntity, new()
    where TDto : class
{
    public abstract TDto ToDto(TDb db);

    public abstract TDb ToDb(TDto dto);

    public void AddOrUpdate(TDto item)
    {
        if (item == null)
            return;

        var dbItem = ToDb(item);

        try
        {
            if (dbItem.DbId == 0)
            {
                Db.Connection.Insert(item, typeof(TDb));
            }
            else
            {
                Db.Connection.Update(item, typeof(TDb));
            }

            Db.OnDataChanged(this);
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

    public void Delete(int primaryKey)
    {
        if (primaryKey < 0)
            return;

        try
        {
            Db.Connection.Delete<TDb>(primaryKey);

            Db.OnDataChanged(this);
        }
        catch (Exception)
        {
            //
        }
    }

    public void Delete()
    {
        try
        {
            Db.Connection.DeleteAll<TDb>();

            Db.OnDataChanged(this);
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
            Db.Connection.Table<TDb>().Delete(predicate);

            Db.OnDataChanged(this);
        }
        catch (Exception)
        {
            //
        }
    }

    public TDto Find(object key)
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

    public IReadOnlyCollection<TDto> GetAll(int currentPage, int itemsPerPage)
    {
        return Db.Connection.Table<TDb>()
                 .Skip(currentPage * itemsPerPage)
                 .Take(itemsPerPage)
                 .Select(ToDto)
                 .ToArray();
    }

    public TDto[] GetAll(Expression<Func<TDb, bool>> predicate = null)
    {
        return predicate == null
                   ? Db.Connection.Table<TDb>().Select(ToDto).ToArray()
                   : Db.Connection.Table<TDb>().Where(predicate).Select(ToDto).ToArray();
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

    protected DbRepositoryBase2(BaseDbContext db)
    {
        Db = db;
    }
}

public interface IDbRepositoryBase2<TDto, TDb>
    where TDb : DbEntity, new()
    where TDto : class
{
    TDto ToDto(TDb db);

    TDb ToDb(TDto dto);

    void AddOrUpdate(TDto item);

    void AddOrUpdate(IEnumerable<TDto> items);

    void Delete();

    void Delete(int primaryKey);

    void Delete(Expression<Func<TDb, bool>> predicate);

    TDto Find(object key);

    TDto Find(Expression<Func<TDb, bool>> predicate);

    IReadOnlyCollection<TDto> GetAll(int currentPage, int itemsPerPage);

    TDto[] GetAll(Expression<Func<TDb, bool>> predicate = null);

    bool Any(Func<TDb, bool> predicate = null);

    int Count(Func<TDb, bool> predicate = null);
}