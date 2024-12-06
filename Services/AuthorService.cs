using ExtensionMethods.OptionSample;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly List<NameType> _authors = new();
        private readonly List<BookType> _books;

        public AuthorService(List<BookType> books)
        {
            _books = books;
        }

        #region Delegates
        public delegate Option<NameType> CreateAuthorDelegate(string firstName, string lastName);
        public delegate Option<NameType> ReadAuthorDelegate(string firstName, string lastName);
        public delegate Option<NameType> UpdateAuthorDelegate(string currentFirstName, string currentLastName, string newFirstName, string newLastName);
        public delegate bool DeleteAuthorDelegate(string firstName, string lastName);
        public delegate Option<IEnumerable<BookType>> ListBooksByAuthorDelegate(NameType author);
        #endregion

        #region Methods
        public CreateAuthorDelegate CreateAuthor => (firstName, lastName) =>
        {
            var author = Name.Create(firstName, lastName);
            _authors.Add(author);
            return Option.Some(author);
        };

        public ReadAuthorDelegate ReadAuthor => (firstName, lastName) =>
        {
            return _authors.FirstOrDefault(a => a.firstName == firstName && a.lastName == lastName).ToOption();
        };

        public UpdateAuthorDelegate UpdateAuthor => (currentFirstName, currentLastName, newFirstName, newLastName) =>
        {
            var authorOption = ReadAuthor(currentFirstName, currentLastName);
            return authorOption.Map(author =>
            {
                var updatedAuthor = Name.Create(newFirstName, newLastName);
                _authors.Remove(author);
                _authors.Add(updatedAuthor);
                UpdateBooksWithAuthor(author, updatedAuthor);
                return updatedAuthor;
            });
        };

        public DeleteAuthorDelegate DeleteAuthor => (firstName, lastName) =>
        {
            var authorOption = ReadAuthor(firstName, lastName);
            authorOption.IfSome(author =>
            {
                _authors.Remove(author);
                RemoveAuthorFromBooks(author);
            });
            return authorOption.IsSome;
        };

        public ListBooksByAuthorDelegate ListBooksByAuthor => (author) =>
        {
            var booksByAuthor = _books.Where(book => book.authors.Contains(author)).ToList();
            return booksByAuthor.Any() ? Option.Some(booksByAuthor.AsEnumerable()) : Option.None<IEnumerable<BookType>>();
        };
        #endregion

        #region Helpers
        private void UpdateBooksWithAuthor(NameType oldAuthor, NameType newAuthor)
        {
            foreach (var book in _books)
            {
                var updatedAuthors = book.authors.Select(a => a.Equals(oldAuthor) ? newAuthor : a).ToArray();
                var updatedBook = book with { authors = updatedAuthors };
                _books.Remove(book);
                _books.Add(updatedBook);
            }
        }

        private void RemoveAuthorFromBooks(NameType author)
        {
            foreach (var book in _books)
            {
                var updatedAuthors = book.authors.Where(a => !a.Equals(author)).ToArray();
                var updatedBook = book with { authors = updatedAuthors };
                _books.Remove(book);
                _books.Add(updatedBook);
            }
        }
        #endregion
    }
}
