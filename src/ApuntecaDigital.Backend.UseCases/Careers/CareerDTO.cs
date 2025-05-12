using ApuntecaDigital.Backend.UseCases.Classes;

namespace ApuntecaDigital.Backend.UseCases.Careers;
public record CareerDTO(int Id, string Name, List<SimpleClassDTO> Classes);
