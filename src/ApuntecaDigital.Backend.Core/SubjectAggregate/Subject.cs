using ApuntecaDigital.Backend.Core.ClassAggregate;

namespace ApuntecaDigital.Backend.Core.SubjectAggregate;

public class Subject(string name, int classId) : EntityBase, IAggregateRoot
{
  // Example of validating primary constructor inputs
  // See: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors#initialize-base-class
  public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  
  // Foreign key to Class
  public int ClassId { get; private set; } = Guard.Against.NegativeOrZero(classId, nameof(classId));
  
  // Navigation property
  public Class? Class { get; private set; }
  
  public void UpdateName(string newName) => Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
  public void UpdateClassId(int newClassId) => ClassId = Guard.Against.NegativeOrZero(newClassId, nameof(newClassId));
}
