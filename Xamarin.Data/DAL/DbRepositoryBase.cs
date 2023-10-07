using System.Linq;
using System.Linq.Expressions;
using Xamarin.Data.Dto;
using Xamarin.Data.Interfaces;
using Xamarin.Data.Models;

namespace Xamarin.Data.DAL;

// public abstract class DbRepositoryBase<TDto, TDb> : IDbRepositoryBase<TDto, TDb>
//     where TDto : DtoBase, IConvertableToDb<TDb>
//     where TDb : DbEntity, IConvertableToDto<TDto>, new()
// {
//     public abstract TDto ToDto(TDb db);
//
//     public abstract TDb ToDb(TDto dto);
//
//     public virtual TDto AddOrUpdate(TDto item) => SqliteWriter.AddOrUpdate<TDb, TDto>(DbContext.Connection, item);
//
//     public void AddOrUpdate(IEnumerable<TDto> items) => SqliteWriter.AddOrUpdate<TDb, TDto>(DbContext.Connection, items);
//
//     public void Delete() => SqliteWriter.Delete<TDb>(DbContext.Connection);
//
//     public void Delete(int primaryKey) => SqliteWriter.Delete<TDb>(DbContext.Connection, primaryKey);
//
//     public void Delete(Expression<Func<TDb, bool>> predicate) => SqliteWriter.Delete<TDb>(DbContext.Connection, predicate);
//
//     public TDto Find(object key) => SqliteReaderBase.Find<TDb, TDto>(DbContext.Connection, key);
//
//     public TDto Find(Expression<Func<TDb, bool>> predicate) => SqliteReaderBase.Find<TDb, TDto>(DbContext.Connection, predicate);
//
//     public TDto FirstOrDefault() => SqliteReaderBase.FirstOrDefault<TDb, TDto>(DbContext.Connection);
//
//     public TDto LastOrDefault() => SqliteReaderBase.LastOrDefault<TDb, TDto>(DbContext.Connection);
//
//     public IReadOnlyList<TDto> All(Expression<Func<TDb, bool>> predicate = null)
//     {
//         return predicate == null
//                    ? SqliteReaderBase.All<TDb, TDto>()
//                    : SqliteReaderBase.All<TDb, TDto>(DbContext.Connection, predicate);
//     }
//
//     public bool Any(Func<TDb, bool> predicate = null)
//     {
//         return predicate == null
//                    ? SqliteReaderBase.Any<TDb>(DbContext.Connection)
//                    : SqliteReaderBase.Any(DbContext.Connection, predicate);
//     }
//
//     public int Count(Func<TDb, bool> predicate = null)
//     {
//         return predicate == null
//                    ? SqliteReaderBase.Count<TDb>(DbContext.Connection)
//                    : SqliteReaderBase.Count(DbContext.Connection, predicate);
//     }
//
//     #region Fields
//
//     protected BaseDbContext DbContext { get; }
//
//     #endregion
//
//     protected DbRepositoryBase(BaseDbContext dbContext)
//     {
//         DbContext = dbContext;
//     }
// }
//
// public interface IDbRepositoryBase<TDto, TDb>
//     where TDto : DtoBase, IConvertableToDb<TDb>
//     where TDb : DbEntity, IConvertableToDto<TDto>
// {
//     TDto ToDto(TDb db);
//
//     TDb ToDb(TDto dto);
//
//     TDto AddOrUpdate(TDto item);
//
//     void AddOrUpdate(IEnumerable<TDto> items);
//
//     void Delete();
//
//     void Delete(int primaryKey);
//
//     void Delete(Expression<Func<TDb, bool>> predicate);
//
//     TDto Find(object key);
//
//     TDto Find(Expression<Func<TDb, bool>> predicate);
//
//     IReadOnlyList<TDto> All(Expression<Func<TDb, bool>> predicate = null);
//
//     bool Any(Func<TDb, bool> predicate = null);
//
//     int Count(Func<TDb, bool> predicate = null);
// }