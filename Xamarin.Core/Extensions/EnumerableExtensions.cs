using System.Collections;
using System.Globalization;

namespace Xamarin.Core.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    /// Distinct method that accepts a perdicate
    /// </summary>
    /// <typeparam name="TSource">The type of the t source.</typeparam>
    /// <typeparam name="TKey">The type of the t key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="predicate">The predicate.</param>
    /// <returns>IEnumerable&lt;TSource&gt;.</returns>
    /// <exception cref="System.ArgumentNullException">source</exception>
    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> predicate)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        return source.GroupBy(predicate)
                     .Select(x => x.First());
    }

    public static int IndexOf(this IEnumerable self, object obj)
    {
        int index = -1;

        var enumerator = self.GetEnumerator();
        enumerator.Reset();
        int i = 0;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current == obj)
            {
                index = i;
                break;
            }

            i++;
        }

        return index;
    }

    public static void AddRange<T>(this ICollection<T> oc, IEnumerable<T> collection)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        foreach (var item in collection)
        {
            oc.Add(item);
        }
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> act)
    {
        foreach (var i in array)
        {
            act(i);
        }

        return array;
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable arr, Action<T> act)
    {
        return arr.OfType<T>().ForEach(act);
    }

    public static IEnumerable<TResult> ForEach<T, TResult>(this IEnumerable<T> array, Func<T, TResult> func)
    {
        var list = new List<TResult>();

        foreach (var i in array)
        {
            var obj = func(i);

            if (obj != null)
                list.Add(obj);
        }

        return list;
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source) => source is null || !source.Any();

    /// <summary>
    /// Partitioning is a common operation on collections. We can partition a collection into two collections. One collection contains all elements that match a predicate and the other collection contains all elements that do not match the predicate.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static (IEnumerable<T> True, IEnumerable<T> False) Partition<T>(this IEnumerable<T> source,
                                                                           Func<T, bool> predicate)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));

        var trueItems = new List<T>();
        var falseItems = new List<T>();

        foreach (var item in source)
        {
            if (predicate(item))
            {
                trueItems.Add(item);
            }
            else
            {
                falseItems.Add(item);
            }
        }

        return (trueItems, falseItems);
    }
    
    /// <summary>
    /// he median is the middle number in a sorted, ascending or descending, list of numbers and can be more descriptive of that data set than the average.
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public static double Median<T>(this IEnumerable<T> source) where T : IConvertible
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var sortedList = source.Select(x => x.ToDouble(CultureInfo.InvariantCulture)).OrderBy(x => x).ToList();
        var count = sortedList.Count;

        if (count == 0)
        {
            throw new InvalidOperationException("The source sequence is empty.");
        }

        if (count % 2 == 0)
        {
            return (sortedList[count / 2 - 1] + sortedList[count / 2]) / 2;
        }

        return sortedList[count / 2];
    }
    
    /// <summary>
    /// The mode is the number that is repeated most often in a set of numbers
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<T> Mode<T>(this IEnumerable<T> source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var groups = source.GroupBy(x => x);
        var maxCount = groups.Max(g => g.Count());
        return groups.Where(g => g.Count() == maxCount).Select(g => g.Key);
    }
    
    /// <summary>
    /// This method shuffles the elements in a sequence using Fisher-Yates shuffle algorithm
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var elements = source.ToArray();
        var random = new Random();
        for (var i = elements.Length - 1; i > 0; i--)
        {
            var swapIndex = random.Next(i + 1);
            (elements[i], elements[swapIndex]) = (elements[swapIndex], elements[i]);
        }

        return elements;
    }
}