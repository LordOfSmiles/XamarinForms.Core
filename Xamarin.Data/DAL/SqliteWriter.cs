using System.Linq.Expressions;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public static class SqliteWriter
{
    public static void AddOrUpdate<TDb>(SQLiteConnection db, TDb dbItem)
        where TDb : ISqliteEntity
    {
        try
        {
            if (dbItem.DbId == 0)
            {
                db.Insert(dbItem, typeof(TDb));
            }
            else
            {
                db.Update(dbItem, typeof(TDb));
            }
        }
        catch (Exception)
        {
            //
        }
    }

    public static TDto AddOrUpdate<TDb, TDto>(SQLiteConnection db, TDto dto, Func<TDto, TDb> toDb, Func<TDb, TDto> toDto)
        where TDb : ISqliteEntity
        where TDto : class
    {
        if (dto == null)
            return null;

        var dbItem = toDb(dto);

        try
        {
            if (dbItem.DbId == 0)
            {
                db.Insert(dbItem, typeof(TDb));
            }
            else
            {
                db.Update(dbItem, typeof(TDb));
            }
        }
        catch (Exception)
        {
            //
        }

        return toDto(dbItem);
    }

    public static void AddOrUpdate<TDb>(SQLiteConnection db, IEnumerable<TDb> items)
        where TDb : ISqliteEntity
    {
        foreach (var item in items)
        {
            AddOrUpdate(db, item);
        }
    }

    public static void AddOrUpdate<TDb, TDto>(SQLiteConnection db, IEnumerable<TDto> items, Func<TDto, TDb> toDb, Func<TDb, TDto> toDto)
        where TDb : ISqliteEntity
        where TDto : class
    {
        foreach (var dto in items)
        {
            AddOrUpdate(db, dto, toDb, toDto);
        }
    }

    public static void Delete<TDb>(SQLiteConnection db)
        where TDb : ISqliteEntity
    {
        try
        {
            db.DeleteAll<TDb>();
        }
        catch (Exception)
        {
            //
        }
    }

    public static void Delete<TDb>(SQLiteConnection db, object primaryKey)
        where TDb : ISqliteEntity
    {
        try
        {
            db.Delete<TDb>(primaryKey);
        }
        catch (Exception)
        {
            //
        }
    }

    public static void Delete<TDb>(SQLiteConnection db, Expression<Func<TDb, bool>> predicate)
        where TDb : ISqliteEntity, new()
    {
        try
        {
            db.Table<TDb>().Delete(predicate);
        }
        catch (Exception)
        {
            //
        }
    }
}