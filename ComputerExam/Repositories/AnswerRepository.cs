using ComputerExam.Common;
using ComputerExam.Models;
using ComputerExam.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
