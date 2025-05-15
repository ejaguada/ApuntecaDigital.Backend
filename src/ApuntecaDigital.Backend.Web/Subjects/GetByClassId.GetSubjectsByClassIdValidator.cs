using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Subjects;

public class GetSubjectsByClassIdValidator : Validator<GetSubjectsByClassIdRequest>
{
    public GetSubjectsByClassIdValidator() // Fixed constructor name to match the class name
    {
        RuleFor(x => x.Id)
          .NotEmpty();
    }
}
