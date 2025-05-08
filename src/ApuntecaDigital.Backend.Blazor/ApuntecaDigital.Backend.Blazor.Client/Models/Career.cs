namespace ApuntecaDigital.Backend.Blazor.Client.Models;

public class Career
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CareerListResponse
{
    public List<Career> Careers { get; set; } = new List<Career>();
}
