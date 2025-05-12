using ApuntecaDigital.Backend.Web.Classes;

namespace ApuntecaDigital.Backend.Web.Careers;

public record CareerRecord(int Id, string Name, List<SimpleClassRecord> Classes);
