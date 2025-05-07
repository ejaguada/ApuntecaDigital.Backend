namespace ApuntecaDigital.Backend.UseCases.Subjects.List;

public record ListSubjectsQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<SubjectDTO>>>;
