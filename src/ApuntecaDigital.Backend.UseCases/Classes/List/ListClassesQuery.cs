namespace ApuntecaDigital.Backend.UseCases.Classes.List;

public record ListClassesQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<ClassDTO>>>;
