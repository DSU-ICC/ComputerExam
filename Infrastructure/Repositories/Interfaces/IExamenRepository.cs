using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IExamenRepository : IGenericRepository<Examen>
    {
        public IQueryable<Examen>? GetExamensByStudentId(int studentId);
        public IQueryable<Examen> GetExamens();
    }
}
