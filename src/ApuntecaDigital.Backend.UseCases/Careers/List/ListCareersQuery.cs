namespace ApuntecaDigital.Backend.UseCases.Careers.List;

public record ListCareersQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<CareerDTO>>>;
