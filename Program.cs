using ExtensionMethods;
using ExtensionMethods.OptionSample;
using ExtensionMethods.OptionSample.Processes;
using static ExtensionMethods.OptionSample.Processes.AddAuthorDefaults;
using static ExtensionMethods.OptionSample.Processes.FormatBookDefaults;
using static ExtensionMethods.OptionSample.Processes.FormatNameDefaults;
using static ExtensionMethods.OptionSample.Processes.FormatNameListDefaults;


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
    // .DoOptional(Console.WriteLine);

var formattedBook = TitleThanNamesInitialsList.FormatBook(
    ListDisplayedNames.Apply(FormatFullName))(book);

Console.WriteLine(formattedBook);