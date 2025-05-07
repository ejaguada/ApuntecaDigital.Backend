using FastEndpoints;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Books;

public class DeleteBookValidator : Validator<DeleteBookRequest>
{
  public DeleteBookValidator()
  {
    RuleFor(x => x.BookId)
      .GreaterThan(0);
  }
}
