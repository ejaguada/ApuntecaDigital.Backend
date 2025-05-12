using ApuntecaDigital.Backend.UseCases.Books;
using ApuntecaDigital.Backend.UseCases.Classes;

namespace ApuntecaDigital.Backend.UseCases.Subjects;
public record UpdateSubjectDTO(int Id, string Name, int ClassId);
