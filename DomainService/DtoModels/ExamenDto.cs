using DomainService.Entity;
using DSUContextDBService.Models;

namespace DomainService.DtoModels
{
    public class ExamenDto
    {
        public int? ExamenId { get; set; }
        public string? Discipline { get; set; }
        public CaseSDepartment? Department { get; set; }
        public CaseCEdukind? Edukind { get; set; }
        public int? Course { get; set; }
        public string? Group { get; set; }
        public DateTime? ExamDate { get; set; }
        public DateTime? EndExamDate { get; set; }
        public int? ExamDurationInMitutes { get; set; }
        public List<ExamTicket>? ExamTickets { get; set; }
        public int? TeacherId { get; set; }
        public Guid? AuditoriumId { get; set; }
    }
}
