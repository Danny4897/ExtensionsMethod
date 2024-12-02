namespace ExtensionMethods.OptionSample.Processes;

public delegate string FormatBook(BookType book);
public delegate string FormatBookExt(FormatNameList formatNameList, BookType book);

public static class FormatBookExtensions
{
    public static FormatBook FormatBook(this FormatBookExt formatter, FormatNameList namesFormatter) =>
        (book) => formatter(namesFormatter, book);
}

public static class FormatBookDefaults
{
    public static FormatBookExt NamesThanTitle => (namesFormatter, book) =>
        $"{namesFormatter(book.authors)} : {book.title}";
    
    public static FormatBookExt TitleThanNames => (namesFormatter, book) =>
        $"{book.title} by {namesFormatter(book.authors)}";
    
    public static FormatBookExt TitleThanNamesInitialsList => (namesFormatter, book) =>
        $"{book.title} by : {string.Join(" - ", book.authors.Select(name => name.Map(
            fullName => $"{fullName.FirstName} {fullName.LastName}",
            mononym => $"{mononym.Name}")))}";
}