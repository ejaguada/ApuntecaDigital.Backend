using FastEndpoints;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Books;

public class GetBookValidator : Validator<GetBookByIdRequest>
{
  public GetBookValidator()
  {
    RuleFor(x => x.BookId)
      .GreaterThan(0);
  }
}
