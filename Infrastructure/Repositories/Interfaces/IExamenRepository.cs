using DomainService.DtoModels;
using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IExamenRepository : IGenericRepository<Examen>
    {
        public IQueryable<Examen> GetExamens();
        public IQueryable<ExamenDto> GetExamensByEmployeeId(Guid employeeId);
        public IQueryable<ExamenDto> GetExamensByAuditoriumId(Guid auditoriumId);
        public List<ExamenStudentDto> GetExamensByStudentId(int studentId);
        public List<StudentsDto> GetStudentsByExamenId(int examenId);
        public List<ForCheckingDto>? GetStudentsByExamenIdForChecking(int examenId);
        public Task<AnswerBlank?> StartExamen(int studentId, int examId);
        public Task<Examen?> CopyExamen(int examenId, DateTime newExamDate);
        public Task DeleteExamen(int id);
    }
}
