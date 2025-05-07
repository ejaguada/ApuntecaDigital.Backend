namespace ApuntecaDigital.Backend.Web.Careers;

public class CreateCareerResponse(int id, string name)
{
  public int Id { get; set; } = id;
  public string Name { get; set; } = name;
}
