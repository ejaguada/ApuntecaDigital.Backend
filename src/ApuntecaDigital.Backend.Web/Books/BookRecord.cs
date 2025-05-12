using ApuntecaDigital.Backend.Web.Subjects;

namespace ApuntecaDigital.Backend.Web.Books;

public record BookRecord(int Id, string Title, string Author, string Isbn, SubjectForBookRecord Subject);
