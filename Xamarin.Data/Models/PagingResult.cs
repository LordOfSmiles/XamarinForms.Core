namespace Xamarin.Data.Models;

public sealed class PagingResult<T>
{
    public PagingResult(T[] items, int total)
    {
        Items = items;
        Total = total;
    }

    public T[] Items { get; }
        
    public int Total { get; }
}