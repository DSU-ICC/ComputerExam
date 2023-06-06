using DomainService.DtoModels;
using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IAnswerBlankRepository : IGenericRepository<AnswerBlank>
    {
        public AnswerBlank? GetAnswerBlankByStudentIdAndExamenId(int studentId, int examId);
        public IQueryable<AnswerBlankAndTicketDto> GetAnswerBlanksAndTicketByStudentId(int studentId);
        public AnswerBlank GetAnswerBlankById(int id);
    }
}
