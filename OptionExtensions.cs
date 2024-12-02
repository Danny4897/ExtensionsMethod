using System.Collections;

namespace ExtensionMethods;

public static class OptionExtensions
{
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> list, Func<T, bool> predicate) =>
        list.Where(predicate)
            .Select(Option<T>.Some)
            .DefaultIfEmpty(Option<T>.None())
            .First();
    
    public static Option<R> Select<T, R>(this Option<T> option, Func<T, R> selector) =>
    option.Map(selector);
    
    public static Option<T> Where<T>(this Option<T> list, Func<T, bool> predicate) =>
    list.Bind(content => predicate(content) ? list : Option<T>.None());
    
    public static Option<TResult> SelectMany<T, R, TResult>(this Option<T> list, Func<T, Option<R>> selector, Func<T, R, TResult> map) =>
    list.Bind(original => selector(original).Map(result => map(original, result)));

    public static Option<T> In<T>(this IEnumerable<T> list, Func<T, bool> predicate) =>
        list.FirstOrNone(predicate);
}