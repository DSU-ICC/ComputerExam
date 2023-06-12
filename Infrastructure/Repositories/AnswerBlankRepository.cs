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

        public IQueryable<AnswerBlank> GetAnswerBlanksAndTicketByStudentId(int studentId)
        {
            var answerBlanks = Get().Include(x=>x.ExamTicket)
                                    .Include(x => x.Answers).ThenInclude(x => x.Question)
                                    .Where(x => x.StudentId == studentId);

            return answerBlanks;
        }

        public AnswerBlankDto GetAnswerBlankById(int id)
        {
            var answerBlank = Get().Include(x => x.Answers)
                                   .Include(x => x.ExamTicket).ThenInclude(x => x.Examen)
                                   .FirstOrDefault(x => x.Id == id);

            var answerBlankDto = new AnswerBlankDto()
            {
                AnswerBlank = answerBlank,
                TimeToEndInSeconds = (int?)
                        (answerBlank.CreateDateTime.Value.AddMinutes((double)answerBlank.ExamTicket.Examen.ExamDurationInMitutes) - DateTime.Now).TotalSeconds
            };

            return answerBlankDto;
        }
    }
}
