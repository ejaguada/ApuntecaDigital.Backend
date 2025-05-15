namespace ApuntecaDigital.Backend.Web.Books;

public class CreateBookResponse(int id, string title, string author, string isbn, int subjectId)
{
  public int Id { get; set; } = id;
  public string Title { get; set; } = title;
  public string Author { get; set; } = author;
  public string Isbn { get; set; } = isbn;
  public int SubjectId { get; set; } = subjectId;
}
