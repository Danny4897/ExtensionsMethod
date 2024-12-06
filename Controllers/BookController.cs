using ExtensionMethods.Providers;
using ExtensionMethods.OptionSample;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ExtensionMethods.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase, IBookController
    {
        private readonly BookDelegateProvider _delegateProvider;

        public BookController(BookDelegateProvider delegateProvider)
        {
            _delegateProvider = delegateProvider;
        }

        /// <summary>
        /// Creates a new book.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <param name="authors">The authors of the book.</param>
        /// <returns>ActionResult indicating the result of the operation.</returns>
        [HttpPost("create")]
        public IActionResult CreateBook(string title, [FromBody] IEnumerable<NameType> authors)
        {
            var result = _delegateProvider.CreateBook(title, authors);
            return result.Match<IActionResult>(
                book => CreatedAtAction(nameof(GetBook), new { title = book.title.ToString() }, book),
                () => BadRequest("Failed to create book.")
            );
        }

        /// <summary>
        /// Retrieves a book by title.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <returns>ActionResult with the book details or NotFound.</returns>
        [HttpGet("get/{title}")]
        public IActionResult GetBook(string title)
        {
            var result = _delegateProvider.ReadBook(title);
            return result.Match<IActionResult>(
                book => Ok(book),
                () => NotFound()
            );
        }

        /// <summary>
        /// Updates a book's title.
        /// </summary>
        /// <param name="title">The current title of the book.</param>
        /// <param name="newTitle">The new title for the book.</param>
        /// <returns>ActionResult with the updated book details or NotFound.</returns>
        [HttpPut("update/{title}")]
        public IActionResult UpdateBook(string title, string newTitle)
        {
            var result = _delegateProvider.UpdateBook(title, newTitle);
            return result.Match<IActionResult>(
                book => Ok(book),
                () => NotFound()
            );
        }

        /// <summary>
        /// Deletes a book.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <returns>ActionResult indicating success or NotFound.</returns>
        [HttpDelete("delete/{title}")]
        public IActionResult DeleteBook(string title)
        {
            var success = _delegateProvider.DeleteBook(title);
            return success ? NoContent() : NotFound();
        }

        /// <summary>
        /// Lists authors of a book.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <returns>ActionResult with the list of authors or NotFound.</returns>
        [HttpGet("list-authors/{title}")]
        public IActionResult ListAuthors(string title)
        {
            var result = _delegateProvider.ListAuthors(title);
            return result.Match<IActionResult>(
                authors => Ok(authors),
                () => NotFound()
            );
        }

        /// <summary>
        /// Adds an author to a book.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <param name="newAuthor">The author to add.</param>
        /// <returns>ActionResult with the updated book details or NotFound.</returns>
        [HttpPost("add-author/{title}")]
        public IActionResult AddAuthor(string title, [FromBody] NameType newAuthor)
        {
            var result = _delegateProvider.AddAuthor(title, newAuthor);
            return result.Match<IActionResult>(
                book => Ok(book),
                () => NotFound()
            );
        }

        /// <summary>
        /// Removes an author from a book.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <param name="authorToRemove">The author to remove.</param>
        /// <returns>ActionResult with the updated book details or NotFound.</returns>
        [HttpDelete("remove-author/{title}")]
        public IActionResult RemoveAuthor(string title, [FromBody] NameType authorToRemove)
        {
            var result = _delegateProvider.RemoveAuthor(title, authorToRemove);
            return result.Match<IActionResult>(
                book => Ok(book),
                () => NotFound()
            );
        }
    }
}
