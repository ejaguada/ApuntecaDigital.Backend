using ApuntecaDigital.Backend.Core.CareerAggregate;

namespace ApuntecaDigital.Backend.Core.ClassAggregate;

public class Class(string name, int year, int careerId) : EntityBase, IAggregateRoot
{
  // Example of validating primary constructor inputs
  // See: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors#initialize-base-class
  public string Name { get; private set; } = Guard.Against.NullOrEmpty(name, nameof(name));
  public int Year { get; private set; } = Guard.Against.NegativeOrZero(year, nameof(year));
  
  // Foreign key to Career
  public int CareerId { get; private set; } = Guard.Against.NegativeOrZero(careerId, nameof(careerId));
  
  // Navigation property
  public Career? Career { get; private set; }
  
  public void UpdateName(string newName) => Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
  public void UpdateYear(int newYear) => Year = Guard.Against.NegativeOrZero(newYear, nameof(newYear));
  public void UpdateCareerId(int newCareerId) => CareerId = Guard.Against.NegativeOrZero(newCareerId, nameof(newCareerId));
}
