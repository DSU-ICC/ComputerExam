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

        public IQueryable<AnswerBlank>? GetAnswerBlanks()
        {
            return Get().Include(x=>x.Answers).ThenInclude(x=>x.Question).Where(x => x.IsDeleted != true);
        }

        public AnswerBlank? GetAnswerBlankByStudentIdAndExamenId(int studentId, int examId)
        {
            return GetAnswerBlanks().FirstOrDefault(x => x.StudentId == studentId && x.ExamTicket.ExamenId == examId);
        }

        public IQueryable<AnswerBlank> GetAnswerBlanksAndTicketByStudentId(int studentId)
        {
            var answerBlanks = GetAnswerBlanks().Include(x=>x.ExamTicket).Where(x => x.StudentId == studentId);

            return answerBlanks;
        }

        public AnswerBlankDto GetAnswerBlankById(int id)
        {
            var answerBlank = GetAnswerBlanks().Include(x => x.ExamTicket).ThenInclude(x => x.Examen).FirstOrDefault(x => x.Id == id);

            var answerBlankDto = new AnswerBlankDto()
            {
                AnswerBlank = answerBlank,
                TimeToEndInSeconds = answerBlank.GetTimeToEndInSeconds()
            };

            return answerBlankDto;
        }
    }
}
