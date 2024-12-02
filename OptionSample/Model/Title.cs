namespace ExtensionMethods.OptionSample;

public record TitleType(string title);

public static class Title
{
    public static TitleType? Create(string title) =>
        title is not null or " " ? new TitleType(title) : null;
}