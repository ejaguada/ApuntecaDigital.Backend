namespace ApuntecaDigital.Backend.Web.Classes;

public class CreateClassResponse(int id, string name, int year, int careerId)
{
  public int Id { get; set; } = id;
  public string Name { get; set; } = name;
  public int Year { get; set; } = year;
  public int CareerId { get; set; } = careerId;
}
