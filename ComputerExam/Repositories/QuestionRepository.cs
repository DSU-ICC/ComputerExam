using ComputerExam.Common;
using ComputerExam.Models;
using ComputerExam.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
