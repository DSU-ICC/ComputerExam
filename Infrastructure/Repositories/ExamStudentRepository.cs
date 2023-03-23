using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ExamStudentRepository : GenericRepository<ExamStudent>, IExamStudentRepository
    {
        public ExamStudentRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
