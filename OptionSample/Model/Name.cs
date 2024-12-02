using System.Collections;

namespace ExtensionMethods.OptionSample;

public abstract record NameType();

public record FullNameType(string FirstName, string LastName) : NameType;

public record Mononym(string Name) : NameType;

public static class NameExtensions
{
    public static R Map<R>(this NameType name
        , Func<FullNameType, R> mapFullName
        , Func<Mononym, R> mapMononym) =>
        name switch
        {
            FullNameType fullName => mapFullName(fullName),
            Mononym mononym => mapMononym(mononym),
            _ => throw new Exception($"Unknown NameType: {name}")
        };
    
    public static List<R> MapList<R>(this IEnumerable<NameType> names, Func<FullNameType, R> mapFullName, Func<Mononym, R> mapMononym) =>
        names.Select(name => name switch
        {
            FullNameType fullName => mapFullName(fullName),
            Mononym mononym => mapMononym(mononym),
            _ => throw new Exception($"Unknown NameType: {name}")
        }).ToList();
}

public static class Name
{
    public static NameType? Create(string FirstName, string LastName, bool mononym = false) =>
        (FirstName, LastName) switch
        {
            (null, _) when mononym => new Mononym(LastName),
            (null, _) when !mononym => null,
            (_, null) when mononym => new Mononym(FirstName),
            (_, null) when !mononym => null,
            (_, _) => new FullNameType(FirstName, LastName),
        };

    public static NameType[]? CreateMany(params NameType[]? names) =>
        names.Where(n => n is not null).ToArray();
}