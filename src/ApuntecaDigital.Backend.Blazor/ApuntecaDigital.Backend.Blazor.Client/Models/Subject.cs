namespace ApuntecaDigital.Backend.Blazor.Client.Models;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ClassId { get; set; }
}

public class SubjectListResponse
{
    public List<Subject> Subjects { get; set; } = new List<Subject>();
}
