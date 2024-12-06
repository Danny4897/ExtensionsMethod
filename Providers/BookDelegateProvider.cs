using ExtensionMethods.Services;
using static ExtensionMethods.Services.BookAuthorService;

namespace ExtensionMethods.Providers
{
    public class BookDelegateProvider
    {
        private readonly BookAuthorService _bookAuthorService;

        public BookDelegateProvider(BookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }

        public CreateBookDelegate CreateBook => _bookAuthorService.CreateBook;
        public ReadBookDelegate ReadBook => _bookAuthorService.ReadBook;
        public UpdateBookDelegate UpdateBook => _bookAuthorService.UpdateBook;
        public DeleteBookDelegate DeleteBook => _bookAuthorService.DeleteBook;
        public ListAuthorsDelegate ListAuthors => _bookAuthorService.ListAuthors;
        public AddAuthorDelegate AddAuthor => _bookAuthorService.AddAuthor;
        public RemoveAuthorDelegate RemoveAuthor => _bookAuthorService.RemoveAuthor;
    }
}
