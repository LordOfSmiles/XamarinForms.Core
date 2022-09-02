using System.Linq;
using System.Linq.Expressions;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public abstract class DbRepositoryBase<T> : IDbRepositoryBase<T>
    where T : DbEntity, new()
{
    public void AddOrUpdate(T item)
    {
        if (item == null)
            return;

        try
        {
            if (item.DbId == 0)
            {
                Db.Connection.Insert(item, typeof(T));
            }
            else
            {
                Db.Connection.Update(item, typeof(T));
            }
        }
        catch (Exception)
        {
            //
        }
    }

    public void AddOrUpdate(IEnumerable<T> items)
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
            Db.Connection.Delete<T>(primaryKey);
            Db.OnDataChanged();
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
            Db.Connection.DeleteAll<T>();
            Db.OnDataChanged();
        }
        catch (Exception)
        {
            //
        }
    }

    public void Delete(Expression<Func<T, bool>> predicate)
    {
        try
        {
            Db.Connection.Table<T>().Delete(predicate);
            Db.OnDataChanged();
        }
        catch (Exception)
        {
            //
        }
    }

    public T Find(object key)
    {
        return Db.Connection.Find<T>(key);
    }

    public T Find(Expression<Func<T, bool>> predicate)
    {
        return Db.Connection.Find(predicate);
    }

    public T[] GetAll(Expression<Func<T, bool>> predicate = null)
    {
        return predicate == null
            ? Db.Connection.Table<T>().ToArray()
            : Db.Connection.Table<T>().Where(predicate).ToArray();
    }

    public bool Any(Func<T, bool> predicate = null)
    {
        return predicate == null
            ? Db.Connection.Table<T>().Any()
            : Db.Connection.Table<T>().Any(predicate);
    }

    public int Count(Func<T, bool> predicate = null)
    {
        return predicate == null
            ? Db.Connection.Table<T>().Count()
            : Db.Connection.Table<T>().Where(predicate).Count();
    }

    #region Fields

    protected BaseDbContext Db { get; }

    #endregion

    protected DbRepositoryBase(BaseDbContext db)
    {
        Db = db;
    }
}

public interface IDbRepositoryBase<T> where T : DbEntity, new()
{
    void AddOrUpdate(T item);

    void AddOrUpdate(IEnumerable<T> items);

    void Delete();

    void Delete(int primaryKey);

    void Delete(Expression<Func<T, bool>> predicate);

    T Find(object key);

    T Find(Expression<Func<T, bool>> predicate);

    T[] GetAll(Expression<Func<T, bool>> predicate = null);

    bool Any(Func<T, bool> predicate = null);

    int Count(Func<T, bool> predicate = null);
}