using ApuntecaDigital.Backend.Web.Books;
using ApuntecaDigital.Backend.Web.Classes;

namespace ApuntecaDigital.Backend.Web.Subjects;

public record SubjectForBookRecord(int Id, string Name, ClassForBookRecord Class);
