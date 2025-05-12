using ApuntecaDigital.Backend.Core.ClassAggregate;

namespace ApuntecaDigital.Backend.Core.CareerAggregate;

public class Career(string name) : EntityBase, IAggregateRoot
{
  // Example of validating primary constructor inputs
  // See: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors#initialize-base-class
  public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public virtual ICollection<Class> Classes { get; private set; } = new List<Class>();
  public void UpdateName(string newName) => Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
}
