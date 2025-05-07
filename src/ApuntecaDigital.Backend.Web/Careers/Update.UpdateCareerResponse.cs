namespace ApuntecaDigital.Backend.Web.Careers;

public class UpdateCareerResponse(CareerRecord career)
{
  public CareerRecord Career { get; set; } = career;
}
