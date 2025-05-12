using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Subjects;

namespace ApuntecaDigital.Backend.UseCases.Classes;
public record UpdateClassDTO(int Id, string Name, int Year, int CareerId); 
