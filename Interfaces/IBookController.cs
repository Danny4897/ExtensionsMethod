using Microsoft.AspNetCore.Mvc;
using ExtensionMethods.OptionSample;
using System.Collections.Generic;

namespace ExtensionMethods.Controllers
{
    public interface IBookController
    {
        IActionResult CreateBook(string title, [FromBody] IEnumerable<NameType> authors);
        IActionResult GetBook(string title);
        IActionResult UpdateBook(string title, string newTitle);
        IActionResult DeleteBook(string title);
        IActionResult ListAuthors(string title);
        IActionResult AddAuthor(string title, [FromBody] NameType newAuthor);
        IActionResult RemoveAuthor(string title, [FromBody] NameType authorToRemove);
    }
}
