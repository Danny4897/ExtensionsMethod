using ExtensionMethods.OptionSample;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods.Services
{
    public class BookAuthorService : IBookService
    {
        private readonly List<BookType> _books = new();

        #region Delegates
        public delegate Option<BookType> CreateBookDelegate(string title, IEnumerable<NameType> authors);
        public delegate Option<BookType> ReadBookDelegate(string title);
        public delegate Option<BookType> UpdateBookDelegate(string currentTitle, string newTitle);
        public delegate bool DeleteBookDelegate(string title);
        public delegate Option<IEnumerable<NameType>> ListAuthorsDelegate(string title);
        public delegate Option<BookType> AddAuthorDelegate(string title, NameType newAuthor);
        public delegate Option<BookType> RemoveAuthorDelegate(string title, NameType authorToRemove);
        public delegate Option<BookType> UpdateAuthorDelegate(string title, NameType currentAuthor, NameType newAuthor);
        #endregion

        #region Methods
        public CreateBookDelegate CreateBook => (title, authors) =>
        {
            var bookOption = Title.Create(title).Map(t => Book.Create(t, authors.ToArray()));
            bookOption.IfSome(book => _books.Add(book));
            return bookOption;
        };

        public ReadBookDelegate ReadBook => (title) =>
        {
            return _books.FirstOrDefault(b => b.title.ToString().Equals(title, StringComparison.OrdinalIgnoreCase)).ToOption();
        };

        public UpdateBookDelegate UpdateBook => (currentTitle, newTitle) =>
        {
            var bookOption = ReadBook(currentTitle);
            return bookOption.Bind(book =>
            {
                var newTitleOption = Title.Create(newTitle);
                return newTitleOption.Map(newTitleValue =>
                {
                    var updatedBook = book with { title = newTitleValue };
                    _books.Remove(book);
                    _books.Add(updatedBook);
                    return updatedBook;
                });
            });
        };

        public DeleteBookDelegate DeleteBook => (title) =>
        {
            var bookOption = ReadBook(title);
            bookOption.IfSome(book => _books.Remove(book));
            return bookOption.IsSome;
        };

        public ListAuthorsDelegate ListAuthors => (title) =>
        {
            return ReadBook(title).Map(book => book.authors);
        };

        public AddAuthorDelegate AddAuthor => (title, newAuthor) =>
        {
            return ReadBook(title).Map(book =>
            {
                var updatedAuthors = book.authors.Append(newAuthor).ToArray();
                var updatedBook = book with { authors = updatedAuthors };
                _books.Remove(book);
                _books.Add(updatedBook);
                return updatedBook;
            });
        };

        public RemoveAuthorDelegate RemoveAuthor => (title, authorToRemove) =>
        {
            return ReadBook(title).Map(book =>
            {
                var updatedAuthors = book.authors.Where(a => !a.Equals(authorToRemove)).ToArray();
                var updatedBook = book with { authors = updatedAuthors };
                _books.Remove(book);
                _books.Add(updatedBook);
                return updatedBook;
            });
        };

        public UpdateAuthorDelegate UpdateAuthor => (title, currentAuthor, newAuthor) =>
        {
            return ReadBook(title).Map(book =>
            {
                var updatedAuthors = book.authors.Select(a => a.Equals(currentAuthor) ? newAuthor : a).ToArray();
                var updatedBook = book with { authors = updatedAuthors };
                _books.Remove(book);
                _books.Add(updatedBook);
                return updatedBook;
            });
        };
        #endregion
    }
}
