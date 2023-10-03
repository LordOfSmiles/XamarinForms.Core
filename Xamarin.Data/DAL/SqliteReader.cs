using System.Linq;
using System.Linq.Expressions;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public static class SqliteReader
{
    public static IReadOnlyList<TDto> All<TDb, TDto>(SQLiteConnection db, Func<TDb, TDto> toDto)
        where TDb : new()
    {
        return db.Table<TDb>()
                 .Select(toDto)
                 .ToArray();
    }

    public static IReadOnlyList<TDto> All<TDb, TDto>(SQLiteConnection db, Expression<Func<TDb, bool>> predicate, Func<TDb, TDto> toDto)
        where TDb : new()
    {
        return db.Table<TDb>()
                 .Where(predicate)
                 .Select(toDto)
                 .ToArray();
    }

    public static TDto Find<TDb, TDto>(SQLiteConnection db, object key, Func<TDb, TDto> toDto)
        where TDb : ISqliteEntity, new()
    {
        var dbItem = db.Find<TDb>(key);

        return dbItem != null
                   ? toDto(dbItem)
                   : default;
    }

    public static TDto Find<TDb, TDto>(SQLiteConnection db, Expression<Func<TDb, bool>> predicate, Func<TDb, TDto> toDto)
        where TDb : ISqliteEntity, new()
    {
        var dbItem = db.Find(predicate);

        return dbItem != null
                   ? toDto(dbItem)
                   : default;
    }

    public static TDto FirstOrDefault<TDb, TDto>(SQLiteConnection db, Func<TDb, TDto> toDto)
        where TDb : ISqliteEntity, new()
    {
        var dbItem = db.Table<TDb>().FirstOrDefault();

        return dbItem != null
                   ? toDto(dbItem)
                   : default;
    }

    public static TDto LastOrDefault<TDb, TDto>(SQLiteConnection db, Func<TDb, TDto> toDto)
        where TDb : ISqliteEntity, new()
    {
        var dbItem = db.Table<TDb>().LastOrDefault();

        return dbItem != null
                   ? toDto(dbItem)
                   : default;
    }

    public static bool Any<TDb>(SQLiteConnection db)
        where TDb : new()
    {
        return db.Table<TDb>().Any();
    }

    public static bool Any<TDb>(SQLiteConnection db, Func<TDb, bool> predicate)
        where TDb : new()
    {
        return db.Table<TDb>().Any(predicate);
    }

    public static int Count<TDb>(SQLiteConnection db)
        where TDb : new()
    {
        return db.Table<TDb>().Count();
    }

    public static int Count<TDb>(SQLiteConnection db, Func<TDb, bool> predicate)
        where TDb : new()
    {
        return db.Table<TDb>().Count(predicate);
    }
}