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

        public async Task DeleteQuestion(int id)
        {
            try
            {
                await Remove(id);
            }
            catch (Exception)
            {
                var question = FindById(id);
                question.IsDeleted = true;
                await Update(question);
            }
        }
    }
}
