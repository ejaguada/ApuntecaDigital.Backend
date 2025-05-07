namespace ApuntecaDigital.Backend.Web.Subjects;

public class CreateSubjectResponse(int id, string name, int classId)
{
  public int Id { get; set; } = id;
  public string Name { get; set; } = name;
  public int ClassId { get; set; } = classId;
}
