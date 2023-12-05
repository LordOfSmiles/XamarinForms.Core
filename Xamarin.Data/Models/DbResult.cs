using Xamarin.Data.Dto;

namespace Xamarin.Data.Models;

public sealed class DbResult<T>
{
    public DbResult(IReadOnlyList<T> items, int total)
    {
        Items = items;
        Total = total;
    }

    public IReadOnlyList<T> Items { get; }
    public int Total { get; }
}