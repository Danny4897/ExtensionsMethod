using Microsoft.AspNetCore.Mvc;

namespace ExtensionMethods.Controllers
{
    public interface IAuthorController
    {
        IActionResult CreateAuthor(string firstName, string lastName);
        IActionResult GetAuthor(string firstName, string lastName);
        IActionResult UpdateAuthor(string firstName, string lastName, string newFirstName, string newLastName);
        IActionResult DeleteAuthor(string firstName, string lastName);
        IActionResult ListBooksByAuthor(string firstName, string lastName);
    }
}
