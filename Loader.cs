using Microsoft.Extensions.Configuration;

namespace LibraryLINQ;

public class Loader(IConfiguration configuration)
{
    /// <summary>
    /// Loads the file indicated by appsettings.json, and creates a BookList with it.
    /// Assumes each linebreak is a different book listing.
    /// Assumes each '|' divides the title, author, and year published, respectively.
    /// </summary>
    /// <returns> A BookList object that is not empty. </returns>
    /// <exception cref="NullReferenceException">
    /// Thrown if "File Addresses:Books" is not set in configuration.
    /// </exception>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the file of books does not exist at the configured location.
    /// </exception>
    public BookList LoadBookList()
    {
        string booksAddress = configuration["File Addresses:Books"] ?? 
                              throw new NullReferenceException(nameof(booksAddress));

        return CreateBookList(ReadFile(booksAddress));
    }

    private static IEnumerable<string> ReadFile(string address)
    {
        if (!File.Exists(address))
        {
            throw new FileNotFoundException($"'{address}' file not found.");
        }
        
        return File.ReadAllLines(address)
            .Select(l => l.Trim())
            .Where(l => l != string.Empty);
    }

    private static Book ParseBookFromLine(string line)
    {
        string[] lineParts = line.Split('|');

        string title = lineParts[0].Trim();
        string author = lineParts[1].Trim();
        int yearPublished = int.Parse(lineParts[2]);
        
        return new Book(title, author, yearPublished);
    }

    private static BookList CreateBookList(IEnumerable<string> fileLines)
    {
        return new BookList(fileLines.Select(ParseBookFromLine).ToList());
    }
}