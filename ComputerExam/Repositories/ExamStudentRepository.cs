using ComputerExam.Common;
using ComputerExam.Models;
using ComputerExam.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Repositories
{
    public class ExamStudentRepository : GenericRepository<ExamStudent>, IExamStudentRepository
    {
        public ExamStudentRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
