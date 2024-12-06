using ExtensionMethods.OptionSample;
using System.Collections.Generic;

namespace ExtensionMethods.Services
{
    public interface IAuthorService
    {
        Option<NameType> CreateAuthor(string firstName, string lastName);
        Option<NameType> ReadAuthor(string firstName, string lastName);
        Option<NameType> UpdateAuthor(string currentFirstName, string currentLastName, string newFirstName, string newLastName);
        bool DeleteAuthor(string firstName, string lastName);
        Option<IEnumerable<BookType>> ListBooksByAuthor(NameType author);
    }
}
