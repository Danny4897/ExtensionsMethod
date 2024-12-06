using ExtensionMethods.OptionSample;
using System.Collections.Generic;

namespace ExtensionMethods.Services
{
    public interface IBookService
    {
        Option<BookType> CreateBook(string title, IEnumerable<NameType> authors);
        Option<BookType> ReadBook(string title);
        Option<BookType> UpdateBook(string currentTitle, string newTitle);
        bool DeleteBook(string title);
        Option<IEnumerable<NameType>> ListAuthors(string title);
        Option<BookType> AddAuthor(string title, NameType newAuthor);
        Option<BookType> RemoveAuthor(string title, NameType authorToRemove);
        Option<BookType> UpdateAuthor(string title, NameType currentAuthor, NameType newAuthor);
    }
}
