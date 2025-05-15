using ApuntecaDigital.Backend.Infrastructure.Data.Config;
using FluentValidation;

namespace ApuntecaDigital.Backend.Web.Books;

public class UpdateBookValidator : Validator<UpdateBookRequest>
{
  public UpdateBookValidator()
  {
    RuleFor(x => x.Id)
      .NotEmpty()
      .WithMessage("Id is required.");

    RuleFor(x => x.BookId)
      .Equal(x => x.Id)
      .WithMessage("BookId must match Id.");

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
