using ExtensionMethods.Services;
using static ExtensionMethods.Services.AuthorService;

namespace ExtensionMethods.Providers
{
    public class AuthorDelegateProvider
    {
        private readonly AuthorService _authorService;

        public AuthorDelegateProvider(AuthorService authorService)
        {
            _authorService = authorService;
        }

        public CreateAuthorDelegate CreateAuthor => _authorService.CreateAuthor;
        public ReadAuthorDelegate ReadAuthor => _authorService.ReadAuthor;
        public UpdateAuthorDelegate UpdateAuthor => _authorService.UpdateAuthor;
        public DeleteAuthorDelegate DeleteAuthor => _authorService.DeleteAuthor;
        public ListBooksByAuthorDelegate ListBooksByAuthor => _authorService.ListBooksByAuthor;
    }
}
