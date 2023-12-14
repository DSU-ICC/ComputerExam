using DomainService.Entity;

namespace DomainService.DtoModels
{
    public class ForCheckingDto
    {
        public int? StudentId { get; set; }
        public string? FioTeacher { get; set; }
        public AnswerBlank? AnswerBlank { get; set; }
    }
}
