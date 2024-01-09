using DomainService.DBService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public IQueryable<Answer> GetAnswers()
        {
            return Get().Where(x => x.IsDeleted != true);
        }            
    }
}
