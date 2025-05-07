namespace ApuntecaDigital.Backend.Web.Subjects;

public class UpdateSubjectResponse(SubjectRecord subject)
{
  public SubjectRecord Subject { get; set; } = subject;
}
