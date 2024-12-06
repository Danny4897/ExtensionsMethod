using ExtensionMethods.Providers;
using ExtensionMethods.OptionSample;
using Microsoft.AspNetCore.Mvc;

namespace ExtensionMethods.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase, IAuthorController
    {
        private readonly AuthorDelegateProvider _delegateProvider;

        public AuthorController(AuthorDelegateProvider delegateProvider)
        {
            _delegateProvider = delegateProvider;
        }

        /// <summary>
        /// Creates a new author.
        /// </summary>
        /// <param name="firstName">The first name of the author.</param>
        /// <param name="lastName">The last name of the author.</param>
        /// <returns>ActionResult indicating the result of the operation.</returns>
        [HttpPost("create")]
        public IActionResult CreateAuthor(string firstName, string lastName)
        {
            var result = _delegateProvider.CreateAuthor(firstName, lastName);
            return result.Match<IActionResult>(
                author => CreatedAtAction(nameof(GetAuthor), new { firstName = author.firstName, lastName = author.lastName }, author),
                () => BadRequest("Failed to create author.")
            );
        }

        /// <summary>
        /// Retrieves an author by name.
        /// </summary>
        /// <param name="firstName">The first name of the author.</param>
        /// <param name="lastName">The last name of the author.</param>
        /// <returns>ActionResult with the author details or NotFound.</returns>
        [HttpGet("get/{firstName}/{lastName}")]
        public IActionResult GetAuthor(string firstName, string lastName)
        {
            var result = _delegateProvider.ReadAuthor(firstName, lastName);
            return result.Match<IActionResult>(
                author => Ok(author),
                () => NotFound()
            );
        }

        /// <summary>
        /// Updates an author's details.
        /// </summary>
        /// <param name="firstName">The current first name of the author.</param>
        /// <param name="lastName">The current last name of the author.</param>
        /// <param name="newFirstName">The new first name of the author.</param>
        /// <param name="newLastName">The new last name of the author.</param>
        /// <returns>ActionResult with the updated author details or NotFound.</returns>
        [HttpPut("update/{firstName}/{lastName}")]
        public IActionResult UpdateAuthor(string firstName, string lastName, string newFirstName, string newLastName)
        {
            var result = _delegateProvider.UpdateAuthor(firstName, lastName, newFirstName, newLastName);
            return result.Match<IActionResult>(
                author => Ok(author),
                () => NotFound()
            );
        }

        /// <summary>
        /// Deletes an author.
        /// </summary>
        /// <param name="firstName">The first name of the author.</param>
        /// <param name="lastName">The last name of the author.</param>
        /// <returns>ActionResult indicating success or NotFound.</returns>
        [HttpDelete("delete/{firstName}/{lastName}")]
        public IActionResult DeleteAuthor(string firstName, string lastName)
        {
            var success = _delegateProvider.DeleteAuthor(firstName, lastName);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Lists books by an author.
        /// </summary>
        /// <param name="firstName">The first name of the author.</param>
        /// <param name="lastName">The last name of the author.</param>
        /// <returns>ActionResult with the list of books or NotFound.</returns>
        [HttpGet("list-books/{firstName}/{lastName}")]
        public IActionResult ListBooksByAuthor(string firstName, string lastName)
        {
            var authorOption = _delegateProvider.ReadAuthor(firstName, lastName);
            return authorOption.Match<IActionResult>(
                author =>
                {
                    var booksResult = _delegateProvider.ListBooksByAuthor(author);
                    return booksResult.Match<IActionResult>(
                        books => Ok(books),
                        () => NotFound()
                    );
                },
                () => NotFound()
            );
        }
    }
}
