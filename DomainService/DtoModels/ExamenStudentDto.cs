using DomainService.Entity;

namespace DomainService.DtoModels
{
    public class ExamenStudentDto
    {
        public int? ExamenId { get; set; }
        public string? Discipline { get; set; }
        public AnswerBlank? AnswerBlank { get; set; }
        public DateTime? ExamDate { get; set; }
        public bool? IsActiveNow { get; set; }
    }
}
