namespace XamarinForms.Core.Helpers;

public static class CollectionHelper
{
    public static bool IsEquals<T>(IReadOnlyCollection<T> list1, IReadOnlyCollection<T> list2)
    {
        var firstNotSecond = list1.Except(list2).ToArray();
        var secondNotFirst = list2.Except(list1).ToArray();

        return !firstNotSecond.Any() && !secondNotFirst.Any();
    }
}