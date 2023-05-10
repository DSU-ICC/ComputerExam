using DomainService.DBService;
using DomainService.Entity;
using DSUContextDBService.Interface;
using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ExamenRepository : GenericRepository<Examen>, IExamenRepository
    {
        private readonly IDsuDbService _dsuDbService;
        public ExamenRepository(ApplicationContext dbContext, IDsuDbService dsuDbService) : base(dbContext)
        {
            _dsuDbService = dsuDbService;
        }

        public IQueryable<Examen>? GetExamensByStudentId(int studentId)
        {
            var student = _dsuDbService.GetCaseSStudents().FirstOrDefault(x => x.Id == studentId);
            if (student != null)
            {
                var examens = Get().Where(x => x.DepartmentId == student.DepartmentId && x.Course == student.Course);
                return examens;
            }
            return null;
        }

        public IQueryable<Examen> GetExamens()
        {
            return Get().Where(x => x.IsDeleted == false);
        }
    }
}
