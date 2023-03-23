namespace ComputerExam.Models
{
    public class Examen
    {
        public int Id { get; set; }
        public int? IdTeacher { get; set; }
        public int? DepartmentId { get; set; }
        public string? Course { get; set; }
        public string? Discipline { get; set; }
    }
}
