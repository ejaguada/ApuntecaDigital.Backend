namespace ApuntecaDigital.Backend.Blazor.Client.Models;

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public int CareerId { get; set; }
}

public class ClassListResponse
{
    public List<Class> Classes { get; set; } = new List<Class>();
}
