using ApuntecaDigital.Backend.UseCases.Classes;

namespace ApuntecaDigital.Backend.UseCases.Subjects;
public record SimpleSubjectDTO(int Id, string Name, int ClassId, SimpleClassDTO Class);
