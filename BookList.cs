using System.Collections;

namespace LibraryLINQ;

public class BookList(IEnumerable<Book> books): IEnumerable<Book>
{
    private IList<Book> Books { get; } = books.ToList();
    private int _current = -1;

    public void Add(Book book)
    {
        Books.Add(book);
    }    
    
    public Book FindNewest()
    {
        return Books
            .OrderByDescending(b => b.YearPublished)
            .First();
    }

    public IEnumerable<string> GetAuthors()
    {
        return Books.Select(b => b.Author).Distinct();
    }

    public IEnumerable<string> GetAllTitlesAuthoredBy(string author)
    {
        return Books
            .Where(b => b.Author == author)
            .Select(b => b.Title);
    }

    public IEnumerable<string> ListAllTitlesAlphabetically()
    {
        return Books
            .OrderBy(b => b.Title, StringComparer.OrdinalIgnoreCase)
            .Select(b => b.Title);
    }

    public IEnumerable<string> BookTitlesPublishedAfter2000()
    {
        return Books
            .Where(b => b.YearPublished > 2000)
            .Select(b => b.Title);
    }

    public IEnumerator<Book> GetEnumerator()
    {
        return Books.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}