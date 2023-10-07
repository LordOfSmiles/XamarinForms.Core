using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Xamarin.Data.Dto;
using Xamarin.Data.Interfaces;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public abstract class SqliteReaderBase
{
    #region Fields

    protected abstract SQLiteConnection Connection { get; }

    #endregion

    public TableQuery<TDb> Table<TDb>()
        where TDb : new()
    {
        return Connection.Table<TDb>();
    }

    public IReadOnlyList<TDto> All<TDb, TDto>()
        where TDb : ISqliteEntity, IConvertableToDto<TDto>, new()
        where TDto : DtoBase, IConvertableToDb<TDb>
    {
        return Connection.Table<TDb>()
                         .Select(x => x.ConvertToDto())
                         .ToArray();
    }

    public IReadOnlyList<TDto> All<TDb, TDto>(Expression<Func<TDb, bool>> predicate)
        where TDb : ISqliteEntity, IConvertableToDto<TDto>, new()
        where TDto : DtoBase, IConvertableToDb<TDb>
    {
        return Connection.Table<TDb>()
                         .Where(predicate)
                         .Select(x => x.ConvertToDto())
                         .ToArray();
    }

    public TDto Find<TDb, TDto>(object key)
        where TDb : ISqliteEntity, IConvertableToDto<TDto>, new()
        where TDto : DtoBase, IConvertableToDb<TDb>
    {
        var dbItem = Connection.Find<TDb>(key);

        return dbItem != null
                   ? dbItem.ConvertToDto()
                   : default;
    }

    public TDto Find<TDb, TDto>(Expression<Func<TDb, bool>> predicate)
        where TDb : ISqliteEntity, IConvertableToDto<TDto>, new()
        where TDto : DtoBase, IConvertableToDb<TDb>
    {
        var dbItem = Connection.Find(predicate);

        return dbItem != null
                   ? dbItem.ConvertToDto()
                   : default;
    }

    public TDto FirstOrDefault<TDb, TDto>()
        where TDb : ISqliteEntity, IConvertableToDto<TDto>, new()
        where TDto : DtoBase, IConvertableToDb<TDb>
    {
        var dbItem = Connection.Table<TDb>().FirstOrDefault();

        return dbItem != null
                   ? dbItem.ConvertToDto()
                   : default;
    }

    public TDto LastOrDefault<TDb, TDto>()
        where TDb : ISqliteEntity, IConvertableToDto<TDto>, new()
        where TDto : DtoBase, IConvertableToDb<TDb>
    {
        var dbItem = Connection.Table<TDb>().LastOrDefault();

        return dbItem != null
                   ? dbItem.ConvertToDto()
                   : default;
    }

    public bool Any<TDb>()
        where TDb : new()
    {
        return Connection.Table<TDb>().Any();
    }

    public bool Any<TDb>(Func<TDb, bool> predicate)
        where TDb : new()
    {
        return Connection.Table<TDb>().Any(predicate);
    }

    public int Count<TDb>()
        where TDb : new()
    {
        return Connection.Table<TDb>().Count();
    }

    public int Count<TDb>(Func<TDb, bool> predicate)
        where TDb : new()
    {
        return Connection.Table<TDb>().Count(predicate);
    }
}