#region Sample
// using ExtensionMethods;
// using ExtensionMethods.OptionSample;
// using ExtensionMethods.OptionSample.Processes;
// using static ExtensionMethods.OptionSample.Processes.AddAuthorDefaults;
// using static ExtensionMethods.OptionSample.Processes.FormatBookDefaults;
// using static ExtensionMethods.OptionSample.Processes.FormatNameDefaults;
// using static ExtensionMethods.OptionSample.Processes.FormatNameListDefaults;

// // Helper method to print authors
// void PrintAuthors(IEnumerable<NameType?> authors)
// {
//     authors.ForEach(author =>
//     {
//         author.IfNotNull(a =>
//         {
//             Console.WriteLine($"Author: {a.firstName} {a.lastName}");
//         });
//     });
// }

// // Original book processing code
// Console.WriteLine("=== Original Book Processing ===\n");

// NameType[] authors = Name.CreateMany(
//     Name.Create("Franco", "Pigna")
//     , Name.Create("Carlo", "Alberto"));
// if (authors is null) return;

// var title = Title.Create("Viaggio in Oriente II");
// if (title is null) return;

// var book = Book.Create(title, authors);
// if (book is null) return;

// var alternativeTitle = Title.Create(book.title.ToString().ToUpper());

// authors
//     .Select(AddUniqueAuthor.AddToBook(book))
//     .Select(book => book with { title = alternativeTitle });

// IEnumerable<BookType> authorsValue =
//     authors
//         .Select(AddUniqueAuthor.AddToBook(book));

// var formattedBook = TitleThanNamesInitialsList.FormatBook(
//     ListDisplayedNames.Apply(FormatFullName))(book);

// Console.WriteLine(formattedBook);

// // FunctionalExtensions Examples
// Console.WriteLine("\n=== FunctionalExtensions Examples ===\n");

// // 1. IfNotNull Example
// Console.WriteLine("1. IfNotNull Example:");
// string? nullableText = "Hello, World!";
// nullableText.IfNotNull(text => Console.WriteLine($"Text is not null: {text}"));
// string? nullText = null;
// nullText.IfNotNull(text => Console.WriteLine("This won't be printed"));

// // 2. IfNull Example
// Console.WriteLine("\n2. IfNull Example:");
// nullText.IfNull(() => Console.WriteLine("Text is null"));

// // 3. Else Example
// Console.WriteLine("\n3. Else Example:");
// nullableText.Else(text => Console.WriteLine($"Text is: {text}"));

// // 4. Match Example
// Console.WriteLine("\n4. Match Example:");
// nullableText.Match(
//     ifNotNull: text => Console.WriteLine($"Text is: {text}"),
//     ifNull: () => Console.WriteLine("Text is null")
// );

// // 5. DefaultIfNull Example
// Console.WriteLine("\n5. DefaultIfNull Example:");
// string defaultValue = nullableText.DefaultIfNull("Default Value");
// Console.WriteLine($"Default value: {defaultValue}");

// // 6. ThrowIfNull Example
// Console.WriteLine("\n6. ThrowIfNull Example:");
// try
// {
//     nullText.ThrowIfNull("Custom error message");
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"Caught exception: {ex.Message}");
// }

// // 7. ForEach Example
// Console.WriteLine("\n7. ForEach Example:");
// var numbers = new[] { 1, 2, 3, 4, 5 };
// numbers.ForEach(n => Console.Write($"{n} "));

// // 8. Map Example
// Console.WriteLine("\n8. Map Example:");
// string? input = "42";
// var result = input.Map(s => s.Length);
// Console.WriteLine($"Length of '{input}' is: {result}");

// // 9. Compose Example
// Console.WriteLine("\n9. Compose Example:");
// Func<int, int> double_ = x => x * 2;
// Func<int, string> toString = x => $"Number is: {x}";
// var doubleAndFormat = double_.Compose(toString);
// Console.WriteLine(doubleAndFormat(5));

// // 10. TryExecute Example
// Console.WriteLine("\n10. TryExecute Example:");
// Func<int> riskyOperation = () => int.Parse("not a number");
// var tryResult = riskyOperation.TryExecute();
// tryResult.IfSome(value => Console.WriteLine($"Success: {value}"))
//         .IfNone(() => Console.WriteLine("Operation failed"));

// // 11. Memoize Example
// Console.WriteLine("\n11. Memoize Example:");
// Func<int, int> expensiveOperation = n => {
//     Console.WriteLine($"Computing for {n}...");
//     Thread.Sleep(100); // Simulate expensive operation
//     return n * n;
// };

// var memoizedOperation = expensiveOperation.Memoize();
// Console.WriteLine("First call:");
// Console.WriteLine($"Result: {memoizedOperation(5)}");
// Console.WriteLine("Second call (cached):");
// Console.WriteLine($"Result: {memoizedOperation(5)}");

// // 12. Constant Example
// Console.WriteLine("\n12. Constant Example:");
// var always42 = FunctionalExtensions.Constant<string, int>(42);
// Console.WriteLine($"Result for 'test': {always42("test")}");
// Console.WriteLine($"Result for 'other': {always42("other")}");

// // 13. Or and And Examples
// Console.WriteLine("\n13. Or and And Examples:");
// Func<int, bool> isEven = x => x % 2 == 0;
// Func<int, bool> isPositive = x => x > 0;
// Func<int, bool> lessThan10 = x => x < 10;

// var isEvenOrPositive = FunctionalExtensions.Or(isEven, isPositive);
// var isEvenAndLessThan10 = FunctionalExtensions.And(isEven, lessThan10);

// Console.WriteLine("Testing number 6:");
// Console.WriteLine($"Is even or positive: {isEvenOrPositive(6)}");
// Console.WriteLine($"Is even and less than 10: {isEvenAndLessThan10(6)}");

// Console.WriteLine("\nTesting number 15:");
// Console.WriteLine($"Is even or positive: {isEvenOrPositive(15)}");
// Console.WriteLine($"Is even and less than 10: {isEvenAndLessThan10(15)}");

// // OptionExtensions Examples
// Console.WriteLine("\n=== OptionExtensions Examples ===\n");

// // 1. IfSome Example
// Console.WriteLine("1. IfSome Example:");
// var someValue = Option.Some(42);
// someValue.IfSome(value => Console.WriteLine($"Value is: {value}"));

// // 2. IfNone Example
// Console.WriteLine("\n2. IfNone Example:");
// var noneValue = Option.None<int>();
// noneValue.IfNone(() => Console.WriteLine("Value is none"));

// // 3. Else Example
// Console.WriteLine("\n3. Else Example:");
// someValue.Else(() => Console.WriteLine("This won't be printed"));

// // 4. Match Example
// Console.WriteLine("\n4. Match Example:");
// someValue.Match(
//     ifSome: value => Console.WriteLine($"Value is: {value}"),
//     ifNone: () => Console.WriteLine("Value is none")
// );

// // 5. DefaultIfNone Example
// Console.WriteLine("\n5. DefaultIfNone Example:");
// var defaultValue = noneValue.DefaultIfNone(42);
// Console.WriteLine($"Default value: {defaultValue}");

// // 6. ThrowIfNone Example
// Console.WriteLine("\n6. ThrowIfNone Example:");
// try
// {
//     noneValue.ThrowIfNone("Custom error message");
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"Caught exception: {ex.Message}");
// }

// // 7. Map Example
// Console.WriteLine("\n7. Map Example:");
// var mappedValue = someValue.Map(value => value * 2);
// Console.WriteLine($"Mapped value: {mappedValue}");

// // 8. Bind Example
// Console.WriteLine("\n8. Bind Example:");
// var boundValue = someValue.Bind(value => Option.Some(value * 2));
// Console.WriteLine($"Bound value: {boundValue}");

// // 9. OrElse Example
// Console.WriteLine("\n9. OrElse Example:");
// var orElseValue = noneValue.OrElse(Option.Some(42));
// Console.WriteLine($"OrElse value: {orElseValue}");

// // 10. Where Example
// Console.WriteLine("\n10. Where Example:");
// var whereValue = someValue.Where(value => value > 0);
// Console.WriteLine($"Where value: {whereValue}");

// // 11. SelectMany Example
// Console.WriteLine("\n11. SelectMany Example:");
// var selectManyValue = someValue.SelectMany(value => Option.Some(value * 2));
// Console.WriteLine($"SelectMany value: {selectManyValue}");

// // Complex Functional and Option Extensions Examples
// Console.WriteLine("\n=== Complex Functional and Option Extensions Examples ===\n");

// // Example 1: Processing Books with Authors
// Console.WriteLine("Example 1: Processing Books with Authors");

// var bookOption = Option.Some(book);

// // Check if book is available and process authors
// bookOption.IfSome(b =>
// {
//     Console.WriteLine($"Processing book: {b.title}");
//     PrintAuthors(b.authors);
// }).IfNone(() => Console.WriteLine("No book available"));

// // Example 2: Transforming Book Titles
// Console.WriteLine("\nExample 2: Transforming Book Titles");

// var transformedBook = bookOption.Map(b =>
// {
//     var newTitle = b.title.ToString().ToUpper();
//     return b with { title = Title.Create(newTitle) };
// });

// transformedBook.IfSome(b => Console.WriteLine($"Transformed title: {b.title}"));

// // Example 3: Safe Book Title Update
// Console.WriteLine("\nExample 3: Safe Book Title Update");

// var updatedBook = bookOption.Bind(b =>
// {
//     var newTitleOption = Title.Create(b.title.ToString() + " - Updated");
//     return newTitleOption.Map(newTitle => b with { title = newTitle });
// });

// updatedBook.IfSome(b => Console.WriteLine($"Updated book title: {b.title}"));

// // Example 4: Filtering Authors by Criteria
// Console.WriteLine("\nExample 4: Filtering Authors by Criteria");

// bookOption.IfSome(b =>
// {
//     var filteredAuthors = b.authors.Where(author =>
//         author.Match(
//             ifNotNull: a => a.firstName.StartsWith("C"),
//             ifNull: () => false
//         )
//     );

//     PrintAuthors(filteredAuthors);
// });

// Console.WriteLine("\nPress any key to exit...");
// Console.ReadKey();
#endregion

using ExtensionMethods.DI;
using ExtensionMethods.SettingsOption;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add authorization services
builder.Services.AddAuthorization();

// Configure options from appsettings.json
services.ConfigureSettings(builder.Configuration);

// Add dependency injection for services
services.AddProjectServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();