using ApuntecaDigital.Backend.Web.Careers;
using ApuntecaDigital.Backend.Web.Subjects;

namespace ApuntecaDigital.Backend.Web.Classes;

public record ClassRecord(int Id, string Name, int Year, SimpleCareerRecord Career, List<SimpleSubjectRecord> Subjects);
