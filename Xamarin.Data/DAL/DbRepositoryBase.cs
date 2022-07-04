using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public abstract class DbRepositoryBase<T> : IDbRepositoryBase<T>
    where T : DbEntity_Old, new()
{
    public void AddOrUpdate(T item)
    {
        if (item == null)
            return;

        try
        {
            if (item.Id == 0)
            {
                Db.Connection.Insert(item, typeof(T));
            }
            else
            {
                Db.Connection.Update(item, typeof(T));
            }
        }
        catch
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

    public void Delete(T item)
    {
        if (item == null)
            return;

        try
        {
            Db.Connection.Delete(item, new TableMapping(typeof(T)));
            Db.OnDataChanged();
        }
        catch
        {
            //
        }
    }

    public void Delete(int dbId)
    {
        if (dbId < 0)
            return;

        try
        {
            Db.Connection.Delete<T>(dbId);
            Db.OnDataChanged();
        }
        catch
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
        catch
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
        catch
        {
            //
        }
    }

    public T FindById(int id)
    {
        return Db.Connection.Find<T>(id);
    }
    
    public T Find(Expression<Func<T,bool>> predicate)
    {
        return Db.Connection.Find(predicate);
    }

    public T[] GetAll()
    {
        return Db.Connection.Table<T>().ToArray();
    }
    
    public T[] GetAll(Expression<Func<T,bool>> predicate)
    {
        return Db.Connection.Table<T>().Where(predicate).ToArray();
    }

    public bool Any()
    {
        return Db.Connection.Table<T>().Any();
    }

    public bool Any(Func<T, bool> predicate)
    {
        return Db.Connection.Table<T>().Any(predicate);
    }

    public int Count()
    {
        return Db.Connection.Table<T>().Count();
    }

    #region Fields

    protected BaseDbContext Db { get; }

    #endregion

    protected DbRepositoryBase(BaseDbContext db)
    {
        Db = db;
    }
}

public interface IDbRepositoryBase<T> where T : DbEntity_Old, new()
{
    void AddOrUpdate(T item);

    void AddOrUpdate(IEnumerable<T> items);

    void Delete();

    void Delete(T item);

    void Delete(int dbId);

    void Delete(Expression<Func<T, bool>> predicate);

    T FindById(int id);

    T Find(Expression<Func<T, bool>> predicate);

    T[] GetAll();

    T[] GetAll(Expression<Func<T, bool>> predicate);

    bool Any();

    bool Any(Func<T, bool> predicate);

    int Count();
}