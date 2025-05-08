using ApuntecaDigital.Backend.Infrastructure.Data.Config;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Books;

/// <summary>
/// See: https://fast-endpoints.com/docs/validation
/// </summary>
public class CreateBookValidator : Validator<CreateBookRequest>
{
  public CreateBookValidator()
  {
    RuleFor(x => x.Title)
      .NotEmpty()
      .WithMessage("Title is required.")
      .MinimumLength(2)
      .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
      
    RuleFor(x => x.Author)
      .NotEmpty()
      .WithMessage("Author is required.")
      .MinimumLength(2)
      .MaximumLength(DataSchemaConstants.DEFAULT_NAME_LENGTH);
      
    RuleFor(x => x.Isbn)
      .NotEmpty()
      .WithMessage("ISBN is required.")
      .MinimumLength(10)
      .MaximumLength(20);
  }
}
