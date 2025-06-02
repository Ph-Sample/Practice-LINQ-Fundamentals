// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace LibraryLINQ; 

public class Program
{
    private static readonly IConfiguration Configuration = SetupConfiguration();

    private static BookList Books { get; set;  }
    
    /// <summary>
    /// Takes in arguments from the user.
    /// </summary>
    /// <param name="args">A list of String commands from the terminal.</param>
    public static void Main(string[] args)
    {
        Loader loader = new(Configuration);
        Books = loader.LoadBookList();

        Console.WriteLine("List all the books read.");
        Console.WriteLine(ListAllBooks(Books));
        Console.WriteLine("Display the newest book.");
        Console.WriteLine($"{Books.FindNewest()}{Environment.NewLine}");
        Console.WriteLine("List books by author.");
        Console.WriteLine(ListBooksByAuthor(Books) + Environment.NewLine);
        Console.WriteLine("List books alphabetically.");
        Console.WriteLine(ListAllTitlesAlphabetically(Books));
        Console.WriteLine("List books published after the year 2000.");
        Console.WriteLine(ListBookTitlesPublishedAfter2000(Books));
    }

    private static string ListAllBooks(BookList books)
    {
        StringBuilder strBuilder = new();
        
        foreach (string line in books.Select(b => b.ToString()))
        {
            strBuilder.AppendLine(line);
        }

        return strBuilder.ToString();
    }

    private static string ListBooksByAuthor(BookList books)
    {
        StringBuilder strBuilder = new();
        
        foreach (string author in books.GetAuthors())
        {
            strBuilder.AppendLine(author);

            foreach (string title in books.GetAllTitlesAuthoredBy(author))
            {
                strBuilder.Append(title);
            }
        }
        
        return strBuilder.ToString();
    }

    private static string ListAllTitlesAlphabetically(BookList books)
    {
        StringBuilder strBuilder = new();
        
        foreach (string title in books.ListAllTitlesAlphabetically())
        {
            strBuilder.AppendLine(title);
        }
        
        return strBuilder.ToString();
    }

    private static string ListBookTitlesPublishedAfter2000(BookList books)
    {
        StringBuilder strBuilder = new();

        foreach (string title in books.BookTitlesPublishedAfter2000())
        {
            strBuilder.AppendLine(title);
        }

        return strBuilder.ToString();
    }

    private static IConfiguration SetupConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}