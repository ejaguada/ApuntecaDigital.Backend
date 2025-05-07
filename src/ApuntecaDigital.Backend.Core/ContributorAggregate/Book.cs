namespace ApuntecaDigital.Backend.Core.CareerAggregate;

public class Book(string title, string author, string isbn, string description, int subjectId) : EntityBase, IAggregateRoot
{
  // Example of validating primary constructor inputs
  // See: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors#initialize-base-class
  public string Title { get; private set; } = Guard.Against.NullOrEmpty(title, nameof(title));
  public string Author { get; private set; } = Guard.Against.NullOrEmpty(author, nameof(author));
  public string Isbn { get; private set; } = Guard.Against.NullOrEmpty(isbn, nameof(isbn));
  public string Description { get; private set; } = Guard.Against.NullOrEmpty(description, nameof(description));
  public Subject? Subject { get; private set; }
  public int SubjectId { get; private set; } = Guard.Against.NegativeOrZero(subjectId, nameof(subjectId));

  public void UpdateSubjectId(int newSubjectId) => SubjectId = Guard.Against.NegativeOrZero(newSubjectId, nameof(newSubjectId));
  public void UpdateAuthor(string newAuthor) => Author = Guard.Against.NullOrEmpty(newAuthor, nameof(newAuthor));
  public void UpdateIsbn(string newIsbn) => Isbn = Guard.Against.NullOrEmpty(newIsbn, nameof(newIsbn));
  public void UpdateDescription(string newDescription) => Description = Guard.Against.NullOrEmpty(newDescription, nameof(newDescription));
  public void UpdateTitle(string newTitle) => Title = Guard.Against.NullOrEmpty(newTitle, nameof(newTitle));
}
