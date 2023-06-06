using DomainService.DBService;
using DomainService.DtoModels;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AnswerBlankRepository : GenericRepository<AnswerBlank>, IAnswerBlankRepository
    {
        public AnswerBlankRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public AnswerBlank? GetAnswerBlankByStudentIdAndExamenId(int studentId, int examId)
        {
            return Get().Include(x => x.Answers).ThenInclude(x=>x.Question).FirstOrDefault(x => x.StudentId == studentId && x.ExamTicket.ExamenId == examId);
        }

        public IQueryable<AnswerBlankAndTicketDto> GetAnswerBlanksAndTicketByStudentId(int studentId)
        {
            var answerBlanks = Get().Include(x => x.Answers).ThenInclude(x => x.Question).Where(x => x.StudentId == studentId);
            var answerBlankAndTicketDto = answerBlanks.Select(x => new AnswerBlankAndTicketDto()
            {
                AnswerBlank = x,
                Ticket = x.ExamTicket
            });

            return answerBlankAndTicketDto;
        }

        public AnswerBlank GetAnswerBlankById(int id)
        {
            var answerBlank = Get().Include(x => x.Answers)
                                   .Include(x => x.ExamTicket).ThenInclude(x => x.Examen)
                                   .FirstOrDefault(x => x.Id == id);

            answerBlank.TimeToEndInMinutes =
                (answerBlank.CreateDateTime.Value.AddMinutes((double)answerBlank.ExamTicket.Examen.ExamDurationInMinutes) - DateTime.Now).TotalMinutes;

            return answerBlank;
        }
    }
}
