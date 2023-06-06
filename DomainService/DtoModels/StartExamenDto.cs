using DomainService.Entity;

namespace DomainService.DtoModels
{
    public class StartExamenDto
    {
        public AnswerBlank? AnswerBlank { get; set; }
        public ExamTicket? ExamTicket { get; set; }
    }
}
