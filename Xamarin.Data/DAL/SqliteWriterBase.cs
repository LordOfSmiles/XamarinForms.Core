using System.Linq.Expressions;
using Xamarin.Data.Dto;
using Xamarin.Data.Interfaces;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

public abstract class SqliteWriterBase
{
    #region Fields

    protected abstract SQLiteConnection Connection { get; }

    #endregion

    public TDto AddOrUpdate<TDb, TDto>(TDto dto)
        where TDb : ISqliteEntity, IConvertableToDto<TDto>
        where TDto : DtoBase, IConvertableToDb<TDb>
    {
        if (dto == null)
            return null;

        var dbItem = dto.ConvertToDb();

        try
        {
            if (dbItem.DbId == 0)
            {
                Connection.Insert(dbItem, typeof(TDb));
            }
            else
            {
                Connection.Update(dbItem, typeof(TDb));
            }
        }
        catch (Exception)
        {
            //
        }

        return dbItem.ConvertToDto();
    }

    public void AddOrUpdate<TDb, TDto>(IEnumerable<TDto> items)
        where TDb : ISqliteEntity, IConvertableToDto<TDto>
        where TDto : DtoBase, IConvertableToDb<TDb>
    {
        foreach (var dto in items)
        {
            AddOrUpdate<TDb, TDto>(dto);
        }
    }

    public void Delete<TDb>()
        where TDb : ISqliteEntity
    {
        try
        {
            Connection.DeleteAll<TDb>();
        }
        catch (Exception)
        {
            //
        }
    }

    public void Delete<TDb>(object primaryKey)
        where TDb : ISqliteEntity
    {
        try
        {
            Connection.Delete<TDb>(primaryKey);
        }
        catch (Exception)
        {
            //
        }
    }

    public void Delete<TDb>(Expression<Func<TDb, bool>> predicate)
        where TDb : ISqliteEntity, new()
    {
        try
        {
            Connection.Table<TDb>().Delete(predicate);
        }
        catch (Exception)
        {
            //
        }
    }
}