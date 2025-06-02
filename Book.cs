namespace LibraryLINQ;

public class Book(string title, string author, int yearPublished)
{
    public string Title { get; set; } = title;

    public string Author { get; set; } = author;

    public int YearPublished { get; set; } = yearPublished;

    public override string ToString()
    {
        return $"\"{Title}\" by {Author}, published {YearPublished}";
    }
}