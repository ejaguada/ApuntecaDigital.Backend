using ApuntecaDigital.Backend.Core.ContributorAggregate;
using ApuntecaDigital.Backend.Core.CareerAggregate;
using ApuntecaDigital.Backend.Core.ClassAggregate;
using ApuntecaDigital.Backend.Core.SubjectAggregate;
using ApuntecaDigital.Backend.Core.BookAggregate;

namespace ApuntecaDigital.Backend.Infrastructure.Data;

public static class SeedData
{
  public static readonly Contributor Contributor1 = new("Ardalis");
  public static readonly Contributor Contributor2 = new("Snowfrog");

  public static async Task InitializeAsync(AppDbContext dbContext)
  {
    if (await dbContext.Contributors.AnyAsync()) return; // DB has been seeded

    await PopulateTestDataAsync(dbContext);
  }

  public static async Task PopulateTestDataAsync(AppDbContext dbContext)
  {
    // Optional: Clean previous data (if needed)
    dbContext.Books.RemoveRange(dbContext.Books);
    dbContext.Subjects.RemoveRange(dbContext.Subjects);
    dbContext.Classes.RemoveRange(dbContext.Classes);
    dbContext.Careers.RemoveRange(dbContext.Careers);
    await dbContext.SaveChangesAsync();

    // Add contributors
    dbContext.Contributors.AddRange([Contributor1, Contributor2]);

    // Careers
    var career1 = new Career("Software Engineering");
    var career2 = new Career("Data Science");
    dbContext.Careers.AddRange([career1, career2]);
    await dbContext.SaveChangesAsync(); // Get IDs

    // Classes with career references
    var class1 = new Class("Introduction to Programming", 2025, career1.Id);
    var class2 = new Class("Data Structures and Algorithms", 2025, career2.Id);
    dbContext.Classes.AddRange([class1, class2]);
    await dbContext.SaveChangesAsync(); // Get IDs

    // Subjects with class references
    var subject1 = new Subject("C#", class1.Id);
    var subject2 = new Subject("Algorithms", class2.Id);
    dbContext.Subjects.AddRange([subject1, subject2]);
    await dbContext.SaveChangesAsync(); // Get IDs

    // Books with subject references
    var book1 = new Book("Clean Code", "Robert C. Martin", "978-0132350884", "A handbook of agile software craftsmanship", subject1.Id);
    var book2 = new Book("Introduction to Algorithms", "Thomas H. Cormen", "978-0262033848", "A comprehensive guide to algorithms", subject2.Id);
    dbContext.Books.AddRange([book1, book2]);

    await dbContext.SaveChangesAsync();
  }
}
