using ApuntecaDigital.Backend.UseCases.Subjects;

namespace ApuntecaDigital.Backend.UseCases.Books;
public record UpdateBookDTO(int Id, string Title, string Author, string Isbn, int SubjectId);
