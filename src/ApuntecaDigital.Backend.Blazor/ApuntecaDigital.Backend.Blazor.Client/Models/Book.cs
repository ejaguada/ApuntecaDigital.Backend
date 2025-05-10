namespace ApuntecaDigital.Backend.Blazor.Client.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int CareerId { get; set; }
    public int ClassId { get; set; }
    public int SubjectId { get; set; }
}

public class BookListResponse
{
    public List<Book> Books { get; set; } = new List<Book>();
}
