using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        public IQueryable<Question> GetQuestions();
    }
}
