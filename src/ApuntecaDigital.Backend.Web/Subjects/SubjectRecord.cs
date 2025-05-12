using ApuntecaDigital.Backend.Web.Books;
using ApuntecaDigital.Backend.Web.Classes;

namespace ApuntecaDigital.Backend.Web.Subjects;

public record SubjectRecord(int Id, string Name, SimpleClassRecord Class, List<SimpleBookRecord> Books);
