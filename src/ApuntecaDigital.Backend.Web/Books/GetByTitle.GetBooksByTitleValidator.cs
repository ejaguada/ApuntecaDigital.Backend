using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Books;

public class GetBooksByTitleValidator : Validator<GetBooksByTitleRequest>
{
  public GetBooksByTitleValidator()
  {
    RuleFor(x => x.Title)
      .NotEmpty();
  }
}
