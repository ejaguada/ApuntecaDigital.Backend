using ApuntecaDigital.Backend.UseCases.Subjects;

namespace ApuntecaDigital.Backend.UseCases.Books;
public record BookDTO(int Id, string Title, string Author, string Isbn, int SubjectId, SimpleSubjectDTO? Subject = null);
