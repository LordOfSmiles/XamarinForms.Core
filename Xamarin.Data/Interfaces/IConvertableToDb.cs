using Xamarin.Data.Models;

namespace Xamarin.Data.Interfaces;

public interface IConvertableToDb<TDb>
    where TDb : ISqliteEntity
{
    public TDb ConvertToDb();
}
