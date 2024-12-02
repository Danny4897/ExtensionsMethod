namespace ExtensionMethods.OptionSample.Processes;

public delegate BookType AddAuthor(BookType bookType, NameType author);

public static class AddAuthorExtension
{
    public static Func<NameType, BookType> AddToBook(this AddAuthor strategy, BookType book) =>
    (author) => strategy(book, author);
}

public static class AddAuthorDefaults
{
    public static AddAuthor AddUniqueAuthor => (book, author) =>
        book.authors.Contains(author)
            ? book
            : book with { authors = [..book.authors, author] };
}