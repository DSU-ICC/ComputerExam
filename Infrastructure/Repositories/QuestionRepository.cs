using DomainService.DBService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        private readonly IAnswerRepository _answerRepository;
        public QuestionRepository(ApplicationContext dbContext, IAnswerRepository answerRepository) : base(dbContext)
        {
            _answerRepository = answerRepository;
        }

        public IQueryable<Question> GetQuestions()
        {
            return Get().Where(x => x.IsDeleted != true);
        }

        public async Task DeleteQuestion(int id)
        {
            var question = Get().FirstOrDefault(x=>x.Id == id);
            if (question != null)
            {
                if (_answerRepository.Get().Any(x => x.QuestionId == id))
                {
                    question.IsDeleted = true;
                    await Update(question);
                }
                else
                    await Remove(id);
            }
            else
                throw new Exception();
        }
    }
}
