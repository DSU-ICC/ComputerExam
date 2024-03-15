using DomainService.DtoModels;
using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IExamenRepository : IGenericRepository<Examen>
    {
        public IQueryable<Examen> GetExamens();
        public IQueryable<Examen> GetExamensWithFilter(int? departmentId = null, int? course = null, string? ngroup = null);
        public List<StudentForStatisticsDto> GetStatisticForReport(int examenId);
        public IQueryable<ExamenDto> GetExamensByEmployeeId(Guid employeeId);
        public IQueryable<ExamenDto> GetExamensByAuditoriumId(Guid auditoriumId);
        public IQueryable<ExamenDto> GetExamensFromArchiveByAuditoriumId(Guid employeeId);
        public IQueryable<ExamenDto> GetExamensFromArchiveByFilter(int? facultyId = null, int? departmentId = null, DateTime? startDate = null, DateTime? endDate = null);
        public List<ExamenStudentDto> GetExamensByStudentId(int studentId);
        public List<StudentsDto> GetStudentsByExamenId(int examenId);
        public List<ForCheckingDto>? GetStudentsByExamenIdForChecking(int examenId);
        public Task<AnswerBlank?> StartExamen(int studentId, int examId);
        public Task<Examen?> CopyExamen(int examenId, DateTime newExamDate);
        public Task DeleteExamen(int id);
    }
}
