using ComputerExam.Common;
using ComputerExam.Models;
using ComputerExam.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Repositories
{
    public class ExamenRepository : GenericRepository<Examen>, IExamenRepository
    {
        public ExamenRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
