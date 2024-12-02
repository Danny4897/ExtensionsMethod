namespace ExtensionMethods;

public static class FunctionalExtensions
{
    public static void DoOptional(this object obj, Action<object> action)
    {
        if (obj is not null) action(obj);
    }

    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) where T : class
    {
        foreach (var item in collection) action(item);
    }

    public static R? BindOptional<T, R>(this T[]? obj, Func<T[], R> func)
    {
        return obj is null ? default : func(obj);
    }
}