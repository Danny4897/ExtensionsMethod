namespace ExtensionMethods;

/// <summary>
/// Provides extension methods for functional programming patterns in C#.
/// </summary>
public static class FunctionalExtensions
{
    /// <summary>
    /// Executes an action on a value if it's not null.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to check and potentially act on.</param>
    /// <param name="action">The action to execute if the value is not null.</param>
    /// <exception cref="ArgumentNullException">Thrown when action is null.</exception>
    public static void IfNotNull<T>(this T? value, Action<T> action) where T : class
    {
        ArgumentNullException.ThrowIfNull(action);
        if (value is not null) action(value);
    }

    /// <summary>
    /// Executes an action on a value if it's null.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="value">The value to check and potentially act on.</param>
    /// <param name="action">The action to execute if the value is null.</param>
    /// <exception cref="ArgumentNullException">Thrown when action is null.</exception>
    public static void IfNull<T>(this T? value, Action action) where T : class
    {
        ArgumentNullException.ThrowIfNull(action);
        if (value is null) action();
    }

    public static void Else<T>(this T? value, Action<T> action) where T : class =>action(value);


    /// <summary>
    /// Executes an action for each element in a collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to iterate over.</param>
    /// <param name="action">The action to execute for each element.</param>
    /// <exception cref="ArgumentNullException">Thrown when source or action is null.</exception>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        foreach (var item in source)
        {
            action(item);
        }
    }

    /// <summary>
    /// Executes an action for each element in a collection with its index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="source">The collection to iterate over.</param>
    /// <param name="action">The action to execute for each element and its index.</param>
    /// <exception cref="ArgumentNullException">Thrown when source or action is null.</exception>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        var index = 0;
        foreach (var item in source)
        {
            action(item, index++);
        }
    }

    /// <summary>
    /// Applies a function to a value if it's not null, otherwise returns default value.
    /// </summary>
    /// <typeparam name="T">The type of the input value.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="value">The value to transform.</param>
    /// <param name="func">The transformation function.</param>
    /// <returns>The transformed value or default if the input was null.</returns>
    /// <exception cref="ArgumentNullException">Thrown when func is null.</exception>
    public static TResult? Map<T, TResult>(this T? value, Func<T, TResult> func)
        where T : class
        where TResult : class
    {
        ArgumentNullException.ThrowIfNull(func);
        return value is not null ? func(value) : null;
    }

    /// <summary>
    /// Chains two functions together, passing the output of the first into the second.
    /// </summary>
    /// <typeparam name="T">The input type of the first function.</typeparam>
    /// <typeparam name="TIntermediate">The output type of the first function and input type of the second function.</typeparam>
    /// <typeparam name="TResult">The final result type.</typeparam>
    /// <param name="func1">The first function to execute.</param>
    /// <param name="func2">The second function to execute with the result of the first.</param>
    /// <returns>A new function that represents the composition of the two input functions.</returns>
    /// <exception cref="ArgumentNullException">Thrown when either function is null.</exception>
    public static Func<T, TResult> Compose<T, TIntermediate, TResult>(
        this Func<T, TIntermediate> func1,
        Func<TIntermediate, TResult> func2)
    {
        ArgumentNullException.ThrowIfNull(func1);
        ArgumentNullException.ThrowIfNull(func2);

        return x => func2(func1(x));
    }

    /// <summary>
    /// Attempts to execute a function and returns an Option containing the result.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="func">The function to execute.</param>
    /// <returns>An Option containing the result if successful, or None if an exception occurred.</returns>
    /// <exception cref="ArgumentNullException">Thrown when func is null.</exception>
    public static Option<T> TryExecute<T>(this Func<T> func)
    {
        ArgumentNullException.ThrowIfNull(func);
        
        try
        {
            return Option<T>.Some(func());
        }
        catch
        {
            return Option<T>.None();
        }
    }

    /// <summary>
    /// Memoizes a function, caching its results for subsequent calls with the same input.
    /// </summary>
    /// <typeparam name="T">The type of the input.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The function to memoize.</param>
    /// <returns>A memoized version of the input function.</returns>
    /// <exception cref="ArgumentNullException">Thrown when func is null.</exception>
    public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> func) where T : notnull
    {
        ArgumentNullException.ThrowIfNull(func);
        
        var cache = new Dictionary<T, TResult>();
        return input =>
        {
            if (!cache.TryGetValue(input, out var result))
            {
                result = func(input);
                cache[input] = result;
            }
            return result;
        };
    }

    /// <summary>
    /// Creates a function that returns the same value regardless of input.
    /// </summary>
    /// <typeparam name="T">The type of the input (ignored).</typeparam>
    /// <typeparam name="TResult">The type of the constant result.</typeparam>
    /// <param name="value">The value to return.</param>
    /// <returns>A function that always returns the specified value.</returns>
    public static Func<T, TResult> Constant<T, TResult>(TResult value) =>
        _ => value;

    /// <summary>
    /// Creates a predicate that returns true if any of the provided predicates return true.
    /// </summary>
    /// <typeparam name="T">The type of the predicate input.</typeparam>
    /// <param name="predicates">The predicates to combine.</param>
    /// <returns>A predicate that returns true if any of the input predicates return true.</returns>
    /// <exception cref="ArgumentNullException">Thrown when predicates is null.</exception>
    public static Func<T, bool> Or<T>(params Func<T, bool>[] predicates)
    {
        ArgumentNullException.ThrowIfNull(predicates);
        return x => predicates.Any(predicate => predicate(x));
    }

    /// <summary>
    /// Creates a predicate that returns true if all of the provided predicates return true.
    /// </summary>
    /// <typeparam name="T">The type of the predicate input.</typeparam>
    /// <param name="predicates">The predicates to combine.</param>
    /// <returns>A predicate that returns true if all of the input predicates return true.</returns>
    /// <exception cref="ArgumentNullException">Thrown when predicates is null.</exception>
    public static Func<T, bool> And<T>(params Func<T, bool>[] predicates)
    {
        ArgumentNullException.ThrowIfNull(predicates);
        return x => predicates.All(predicate => predicate(x));
    }
}