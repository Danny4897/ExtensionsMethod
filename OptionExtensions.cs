namespace ExtensionMethods;

/// <summary>
/// Provides a set of useful extension methods for working with Option{T} instances.
/// </summary>
public static class OptionExtensions
{
    /// <summary>
    /// Returns the first element of a sequence that satisfies a condition as an Option, or None if no such element exists.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <param name="source">The sequence to search.</param>
    /// <param name="predicate">The condition to satisfy.</param>
    /// <returns>An Option containing the first matching element, or None if no match was found.</returns>
    public static Option<T> FirstOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(predicate);

        foreach (var item in source)
        {
            if (predicate(item))
                return Option<T>.Some(item);
        }
        return Option<T>.None();
    }

    /// <summary>
    /// Projects the value of an Option into a new form if the Option has a value.
    /// </summary>
    /// <typeparam name="T">The type of the source Option value.</typeparam>
    /// <typeparam name="R">The type of the projected value.</typeparam>
    /// <param name="option">The source Option.</param>
    /// <param name="selector">The transform function to apply to the value.</param>
    /// <returns>A new Option containing the transformed value, or None if the source was None.</returns>
    public static Option<R> Select<T, R>(this Option<T> option, Func<T, R> selector)
    {
        ArgumentNullException.ThrowIfNull(selector);
        return option.Map(selector);
    }

    /// <summary>
    /// Filters an Option value based on a predicate.
    /// </summary>
    /// <typeparam name="T">The type of the Option value.</typeparam>
    /// <param name="option">The source Option.</param>
    /// <param name="predicate">The condition to test.</param>
    /// <returns>The Option if it has a value and satisfies the condition, otherwise None.</returns>
    public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return option.Bind(value => predicate(value) ? Option<T>.Some(value) : Option<T>.None());
    }

    /// <summary>
    /// Projects and flattens an Option value into a new Option based on a selector function.
    /// </summary>
    public static Option<TResult> SelectMany<T, R, TResult>(
        this Option<T> option,
        Func<T, Option<R>> selector,
        Func<T, R, TResult> resultSelector)
    {
        ArgumentNullException.ThrowIfNull(selector);
        ArgumentNullException.ThrowIfNull(resultSelector);
        
        return option.Bind(x => selector(x).Map(y => resultSelector(x, y)));
    }

    /// <summary>
    /// Returns the option if it has a value, otherwise returns the alternative option.
    /// </summary>
    public static Option<T> OrElse<T>(this Option<T> option, Option<T> alternative) =>
        option.IsSome ? option : alternative;

    /// <summary>
    /// Returns the option if it has a value, otherwise returns the result of calling the alternative function.
    /// </summary>
    public static Option<T> OrElse<T>(this Option<T> option, Func<Option<T>> alternative)
    {
        ArgumentNullException.ThrowIfNull(alternative);
        return option.IsSome ? option : alternative();
    }

    /// <summary>
    /// Returns the value if present, otherwise returns the provided default value.
    /// </summary>
    public static T GetValueOr<T>(this Option<T> option, T defaultValue) =>
        option.GetValueOrDefault(defaultValue);

    /// <summary>
    /// Returns the value if present, otherwise returns the result of calling the default value provider.
    /// </summary>
    public static T GetValueOr<T>(this Option<T> option, Func<T> defaultValueProvider)
    {
        ArgumentNullException.ThrowIfNull(defaultValueProvider);
        return option.Match(
            value => value,
            () => defaultValueProvider()
        );
    }

    /// <summary>
    /// Executes an action if the Option has a value.
    /// </summary>
    public static void IfSome<T>(this Option<T> option, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(action);
        option.Match(
            some: action,
            none: () => { }
        );
    }

    /// <summary>
    /// Executes an action if the Option has no value.
    /// </summary>
    public static void IfNone<T>(this Option<T> option, Action action)
    {
        ArgumentNullException.ThrowIfNull(action);
        option.Match(
            some: _ => { },
            none: action
        );
    }

    /// <summary>
    /// Converts the Option to an enumerable with zero or one elements.
    /// </summary>
    public static IEnumerable<T> AsEnumerable<T>(this Option<T> option) =>
        option.Match(
            some: value => new[] { value },
            none: Array.Empty<T>
        );

    /// <summary>
    /// Flattens a nested Option (Option{Option{T}}) into a single Option{T}.
    /// </summary>
    public static Option<T> Flatten<T>(this Option<Option<T>> option) =>
        option.Bind(x => x);

    /// <summary>
    /// Combines two Options into a tuple if both have values.
    /// </summary>
    public static Option<(T1, T2)> Zip<T1, T2>(this Option<T1> first, Option<T2> second) =>
        first.Bind(x => second.Map(y => (x, y)));

    /// <summary>
    /// Attempts to cast the value to type R if the Option has a value.
    /// </summary>
    public static Option<R> Cast<T, R>(this Option<T> option) where T : class where R : class =>
        option.Bind(value => value as R is R cast ? Option<R>.Some(cast) : Option<R>.None());

    /// <summary>
    /// Returns true if the Option has a value and the value satisfies the predicate.
    /// </summary>
    public static bool Exists<T>(this Option<T> option, Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        return option.Match(
            some: predicate,
            none: () => false
        );
    }

    /// <summary>
    /// Converts a nullable value to an Option.
    /// </summary>
    public static Option<T> ToOption<T>(this T? value) where T : class =>
        value is not null ? Option<T>.Some(value) : Option<T>.None();

    /// <summary>
    /// Converts a nullable struct to an Option.
    /// </summary>
    public static Option<T> ToOption<T>(this T? value) where T : struct =>
        value.HasValue ? Option<T>.Some(value.Value) : Option<T>.None();
}