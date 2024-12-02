namespace ExtensionMethods;

public class Option<T>
{
    private readonly T _content;
    private readonly bool _hasValue;
    
    private Option(T content, bool hasValue) =>
    (_content, _hasValue) = (content, hasValue);
    
    public static Option<T> Some(T content) =>
    new(content, true);
    
    public static Option<T> None() =>
    new(default, false);
    
    public T Get(T defaultValue) =>
    _hasValue ? _content : defaultValue;

    public Option<R> Map<R>(Func<T, R> map) =>
        _hasValue
            ? Option<R>.Some(map(_content))
            : Option<R>.None();
    
    public Option<R> Bind<R>(Func<T, Option<R>> map) =>
    _hasValue
        ? map(_content)
        : Option<R>.None();
    
    public override string ToString() =>
    _hasValue
        ? _content!.ToString() ?? string.Empty
        : "None";
}