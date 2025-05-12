using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Classes;

namespace ApuntecaDigital.Backend.UseCases.Subjects;
public record SubjectDTO(int Id, string Name, int ClassId, SimpleClassDTO? Class = null, List<SimpleBookDTO>? Books = null);
