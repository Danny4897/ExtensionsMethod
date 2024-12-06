namespace ExtensionMethods;

public sealed class Option<T>
{
    private readonly T? _content;
    private readonly bool _hasValue;
    
    private Option(T? content, bool hasValue) =>
        (_content, _hasValue) = (content, hasValue);
    
    public static Option<T> Some(T content)
    {
        ArgumentNullException.ThrowIfNull(content);
        return new(content, true);
    }
    
    public static Option<T> None() => new(default, false);

    public bool IsSome => _hasValue;
    public bool IsNone => !_hasValue;
    
    public T GetValueOrDefault(T defaultValue) =>
        _hasValue ? _content! : defaultValue;
    
    public T GetValueOrThrow() =>
        _hasValue ? _content! : throw new InvalidOperationException("Option has no value");
    
    public Option<R> Map<R>(Func<T, R> map)
    {
        ArgumentNullException.ThrowIfNull(map);
        return _hasValue
            ? Option<R>.Some(map(_content!))
            : Option<R>.None();
    }
    
    public Option<R> Bind<R>(Func<T, Option<R>> bind)
    {
        ArgumentNullException.ThrowIfNull(bind);
        return _hasValue
            ? bind(_content!)
            : Option<R>.None();
    }

    public T Match(T noneValue, Func<T, T> some)
    {
        ArgumentNullException.ThrowIfNull(some);
        return _hasValue ? some(_content!) : noneValue;
    }

    public void Match(Action none, Action<T> some)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);
        
        if (_hasValue)
            some(_content!);
        else
            none();
    }

    public R Match<R>(Func<R> none, Func<T, R> some)
    {
        ArgumentNullException.ThrowIfNull(some);
        ArgumentNullException.ThrowIfNull(none);
        
        return _hasValue ? some(_content!) : none();
    }
    
    public override string ToString() =>
        _hasValue
            ? _content?.ToString() ?? string.Empty
            : "None";

    public override bool Equals(object? obj) =>
        obj is Option<T> other && Equals(other);

    private bool Equals(Option<T> other)
    {
        if (_hasValue != other._hasValue)
            return false;
        
        return !_hasValue || EqualityComparer<T>.Default.Equals(_content!, other._content!);
    }

    public override int GetHashCode() =>
        _hasValue ? HashCode.Combine(_content) : 0;

    public static bool operator ==(Option<T> left, Option<T> right) =>
        left.Equals(right);

    public static bool operator !=(Option<T> left, Option<T> right) =>
        !left.Equals(right);
}