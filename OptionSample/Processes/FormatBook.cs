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

/*** Explanation
 *
 * The code defines several delegates and extension methods to handle formatting and processing of book and author information. Here's a breakdown of the key components:

### Delegates
1. **AddAuthor**: 
   ```csharp
   public delegate BookType AddAuthor(BookType bookType, NameType author);
   ```
   - Represents a method that takes a `BookType` and a `NameType` (author) and returns a `BookType`.

2. **FormatName**:
   ```csharp
   public delegate string FormatName(NameType name);
   ```
   - Represents a method that takes a `NameType` and returns a formatted string.

3. **FormatNameList**:
   ```csharp
   public delegate string FormatNameList(NameType[] names);
   ```
   - Represents a method that takes an array of `NameType` and returns a formatted string.

4. **FormatNameListExt**:
   ```csharp
   public delegate string FormatNameListExt(FormatName nameFormatter, NameType[] names);
   ```
   - Represents a method that takes a `FormatName` delegate and an array of `NameType`, and returns a formatted string.

5. **FormatBook**:
   ```csharp
   public delegate string FormatBook(BookType book);
   ```
   - Represents a method that takes a `BookType` and returns a formatted string.

6. **FormatBookExt**:
   ```csharp
   public delegate string FormatBookExt(FormatNameList formatNameList, BookType book);
   ```
   - Represents a method that takes a `FormatNameList` delegate and a `BookType`, and returns a formatted string.

### Extension Methods and Default Implementations
1. **AddAuthorExtension**:
   ```csharp
   public static Func<NameType, BookType> AddToBook(this AddAuthor strategy, BookType book) =>
       (author) => strategy(book, author);
   ```
   - Provides an extension method `AddToBook` for the `AddAuthor` delegate, which returns a function that adds an author to a book.

2. **AddAuthorDefaults**:
   ```csharp
   public static AddAuthor AddUniqueAuthor => (book, author) =>
       book.authors.Contains(author)
           ? book
           : book with { authors = [..book.authors, author] };
   ```
   - Provides a default implementation `AddUniqueAuthor` that adds an author to a book only if the author is not already present.

3. **FormatNameListExtensions**:
   ```csharp
   public static FormatNameList Apply(this FormatNameListExt formatter, FormatName nameFormatter) =>
       names => formatter(nameFormatter, names);
   ```
   - Provides an extension method `Apply` for the `FormatNameListExt` delegate, which returns a `FormatNameList` delegate.

4. **FormatNameListDefaults**:
   ```csharp
   public static FormatNameListExt CommaSeparatedNames => (nameFormatter, names) =>
       string.Join(", ", names.Select(nameFormatter.Invoke));
   
   public static FormatNameListExt ListDisplayedNames => (nameFormatter, names) =>
       string.Join("\n", names.Select(nameFormatter.Invoke));
   ```
   - Provides default implementations for formatting a list of names, either as a comma-separated string or as a newline-separated string.

5. **FormatNameDefaults**:
   ```csharp
   public static FormatName FormatFullName => (name) =>
       name.Map(
           fullName => $"{fullName.FirstName} {fullName.LastName}",
           mononym => $"{mononym.Name}"
       );

   public static FormatName FormatInitials => (name) =>
       name.Map(
           fullName => $"{fullName.FirstName[..1]} {fullName.LastName[..1]}",
           mononym => $"{mononym.Name[..1]}"
       );
   ```
   - Provides default implementations for formatting a name as a full name or as initials.

6. **FormatBookExtensions**:
   ```csharp
   public static FormatBook FormatBook(this FormatBookExt formatter, FormatNameList namesFormatter) =>
       (book) => formatter(namesFormatter, book);
   ```
   - Provides an extension method `FormatBook` for the `FormatBookExt` delegate, which returns a `FormatBook` delegate.

7. **FormatBookDefaults**:
   ```csharp
   public static FormatBookExt NamesThanTitle => (namesFormatter, book) =>
       $"{namesFormatter(book.authors)} : {book.title}";

   public static FormatBookExt TitleThanNames => (namesFormatter, book) =>
       $"{book.title} by {namesFormatter(book.authors)}";

   public static FormatBookExt TitleThanNamesInitialsList => (namesFormatter, book) =>
       $"{book.title} by : {string.Join(" - ", book.authors.Select(name => name.Map(
           fullName => $"{fullName.FirstName} {fullName.LastName}",
           mononym => $"{mononym.Name}")))}";
   ```
   - Provides default implementations for formatting a book, either with names followed by the title, title followed by names, or title followed by names with initials.
 */