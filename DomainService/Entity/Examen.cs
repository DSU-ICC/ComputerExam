namespace DomainService.Entity
{
    public class Examen
    {
        public int Id { get; set; }
        public int? IdTeacher { get; set; }
        public int? DepartmentId { get; set; }
        public int? Course { get; set; }
        public string? NGroup { get; set; }
        public string? Discipline { get; set; }
    }
}
