using Xamarin.Data.Models;

namespace Xamarin.Data.Interfaces;

public interface IDtoWithConverter<out TDb>
    where TDb : ISqliteEntity
{
    public TDb ConvertToDb();
}