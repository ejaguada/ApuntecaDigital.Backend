using ApuntecaDigital.Backend.UseCases.Careers;
using ApuntecaDigital.Backend.UseCases.Subjects;

namespace ApuntecaDigital.Backend.UseCases.Classes;
public record ClassDTO(int Id, string Name, int Year, int CareerId, SimpleCareerDTO Career, List<SimpleSubjectDTO> Subjects);
