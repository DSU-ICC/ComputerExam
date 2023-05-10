using DomainService.DtoModels;
using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IExamenRepository : IGenericRepository<Examen>
    {
        public IQueryable<Examen> GetExamens();
        public IQueryable<ExamenDto> GetExamensByEmployeeId(Guid employeeId);
        public IQueryable<ExamenStudentDto> GetExamensByStudentId(int studentId);
        public IQueryable<StudentsDto> GetStudentsByExamenId(int examenId);
        public IQueryable<ForCheckingDto> GetStudentsByExamenIdForChecking(int examenId);
        public Task<StartExamenDto>? StartExamen(int studentId, int examId);
        public Task DeleteExamen(int id);
    }
}
