namespace ExtensionMethods.OptionSample;

public record BookType(TitleType title, NameType[] authors);

public static class Book
{
    public static BookType Create(TitleType title, params NameType[] authors) =>
    new BookType(title, authors);
}