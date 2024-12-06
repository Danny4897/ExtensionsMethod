using ExtensionMethods;
using ExtensionMethods.OptionSample;
using ExtensionMethods.OptionSample.Processes;
using static ExtensionMethods.OptionSample.Processes.AddAuthorDefaults;
using static ExtensionMethods.OptionSample.Processes.FormatBookDefaults;
using static ExtensionMethods.OptionSample.Processes.FormatNameDefaults;
using static ExtensionMethods.OptionSample.Processes.FormatNameListDefaults;

// Original book processing code
Console.WriteLine("=== Original Book Processing ===\n");

NameType[] authors = Name.CreateMany(
    Name.Create("Franco", "Pigna")
    , Name.Create("Carlo", "Alberto"));
if (authors is null) return;

var title = Title.Create("Viaggio in Oriente II");
if (title is null) return;

var book = Book.Create(title, authors);
if (book is null) return;

var alternativeTitle = Title.Create(book.title.ToString().ToUpper());

authors
    .Select(AddUniqueAuthor.AddToBook(book))
    .Select(book => book with { title = alternativeTitle });

IEnumerable<BookType> authorsValue =
    authors
        .Select(AddUniqueAuthor.AddToBook(book));

var formattedBook = TitleThanNamesInitialsList.FormatBook(
    ListDisplayedNames.Apply(FormatFullName))(book);

Console.WriteLine(formattedBook);

// FunctionalExtensions Examples
Console.WriteLine("\n=== FunctionalExtensions Examples ===\n");

// Null Handling Examples
Console.WriteLine("=== Null Handling Examples ===");

// 1. IfNotNull and IfNull Examples
Console.WriteLine("\n1. IfNotNull and IfNull Examples:");
string? text = "Hello";
string? nullText = null;

text.IfNotNull(t => Console.WriteLine($"Text is: {t}"));
nullText.IfNull(() => Console.WriteLine("Text is null"));

// 2. Match (Action) Example
Console.WriteLine("\n2. Match (Action) Example:");
text.Match(
    ifNotNull: t => Console.WriteLine($"Text is: {t}"),
    ifNull: () => Console.WriteLine("Text is null")
);

// 3. Match (Func) Example
Console.WriteLine("\n3. Match (Func) Example:");
int length = text.Match(
    ifNotNull: t => t.Length,
    ifNull: () => 0
);
Console.WriteLine($"Length is: {length}");

// 4. DefaultIfNull Examples
Console.WriteLine("\n4. DefaultIfNull Examples:");
string? result1 = nullText.DefaultIfNull("Default Value");
Console.WriteLine($"Default value: {result1}");

string? result2 = nullText.DefaultIfNull(() => "Generated Default");
Console.WriteLine($"Generated default: {result2}");

// 5. ThrowIfNull Examples
Console.WriteLine("\n5. ThrowIfNull Examples:");
try
{
    nullText.ThrowIfNull("Custom error message");
}
catch (Exception ex)
{
    Console.WriteLine($"Caught exception: {ex.Message}");
}

try
{
    nullText.ThrowIfNull(() => new InvalidOperationException("Custom exception"));
}
catch (Exception ex)
{
    Console.WriteLine($"Caught custom exception: {ex.Message}");
}

// 1. IfNotNull Example
Console.WriteLine("\n1. IfNotNull Example:");
string? nullableText = "Hello, World!";
nullableText.IfNotNull(text => Console.WriteLine($"Text is not null: {text}"));
string? nullText = null;
nullText.IfNotNull(text => Console.WriteLine("This won't be printed"));

// 2. ForEach Examples
Console.WriteLine("\n2. ForEach Examples:");
var numbers = new[] { 1, 2, 3, 4, 5 };
Console.WriteLine("Simple ForEach:");
numbers.ForEach(n => Console.Write($"{n} "));

Console.WriteLine("\nForEach with index:");
numbers.ForEach((n, i) => Console.Write($"({i}:{n}) "));

// 3. Map Example
Console.WriteLine("\n\n3. Map Example:");
string? input = "42";
var result = input.Map(s => s.Length);
Console.WriteLine($"Length of '{input}' is: {result}");

// 4. Compose Example
Console.WriteLine("\n4. Compose Example:");
Func<int, int> double_ = x => x * 2;
Func<int, string> toString = x => $"Number is: {x}";
var doubleAndFormat = double_.Compose(toString);
Console.WriteLine(doubleAndFormat(5));

// 5. TryExecute Example
Console.WriteLine("\n5. TryExecute Example:");
Func<int> riskyOperation = () => int.Parse("not a number");
var tryResult = riskyOperation.TryExecute();
tryResult.IfSome(value => Console.WriteLine($"Success: {value}"))
        .IfNone(() => Console.WriteLine("Operation failed"));

// 6. Memoize Example
Console.WriteLine("\n6. Memoize Example:");
Func<int, int> expensiveOperation = n => {
    Console.WriteLine($"Computing for {n}...");
    Thread.Sleep(100); // Simulate expensive operation
    return n * n;
};

var memoizedOperation = expensiveOperation.Memoize();
Console.WriteLine("First call:");
Console.WriteLine($"Result: {memoizedOperation(5)}");
Console.WriteLine("Second call (cached):");
Console.WriteLine($"Result: {memoizedOperation(5)}");

// 7. Constant Example
Console.WriteLine("\n7. Constant Example:");
var always42 = FunctionalExtensions.Constant<string, int>(42);
Console.WriteLine($"Result for 'test': {always42("test")}");
Console.WriteLine($"Result for 'other': {always42("other")}");

// 8. Or and And Examples
Console.WriteLine("\n8. Or and And Examples:");
Func<int, bool> isEven = x => x % 2 == 0;
Func<int, bool> isPositive = x => x > 0;
Func<int, bool> lessThan10 = x => x < 10;

var isEvenOrPositive = FunctionalExtensions.Or(isEven, isPositive);
var isEvenAndLessThan10 = FunctionalExtensions.And(isEven, lessThan10);

Console.WriteLine("Testing number 6:");
Console.WriteLine($"Is even or positive: {isEvenOrPositive(6)}");
Console.WriteLine($"Is even and less than 10: {isEvenAndLessThan10(6)}");

Console.WriteLine("\nTesting number 15:");
Console.WriteLine($"Is even or positive: {isEvenOrPositive(15)}");
Console.WriteLine($"Is even and less than 10: {isEvenAndLessThan10(15)}");

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();