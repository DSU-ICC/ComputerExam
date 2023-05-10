using DomainService.DBService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Question> GetQuestions()
        {
            return Get().Where(x => x.IsDeleted == false);
        }
    }
}
