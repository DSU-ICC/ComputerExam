using DomainService.DtoModels;
using DSUContextDBService.DBService;
using DSUContextDBService.DtoModels;
using DSUContextDBService.Models;

namespace DSUContextDBService.Interface
{
    public interface IDsuDbService
    {
        public IQueryable<CaseCFilial> GetFilials();
        public CaseCFilial? GetFilialsById(int id);
        public IQueryable<CaseCFaculty> GetFaculties();
        public CaseCFaculty? GetFacultyById(int id);
        public IQueryable<CaseSDepartment> GetCaseSDepartments();
        public CaseSDepartment? GetCaseSDepartmentById(int id);
        public IQueryable<CaseSDepartment>? GetCaseSDepartmentByFacultyId(int id);
        public IQueryable<CaseSSpecialization> GetCaseSSpecializations();
        public CaseSSpecialization? GetCaseSSpecializationById(int? id);
        public List<int>? GetCoursesByDepartmentId(int filialId, int departmentId);
        public List<string?>? GetGroupsByDepartmentId(int departmentId, int course, int? filialId);
        public IQueryable<CaseSStudent> GetCaseSStudents();
        public IQueryable<StudentDtoForListOutput> GetStudentDtoForListOutput(int filId = 1);
        public CaseSStudent? GetCaseSStudentById(int id);
        public IQueryable<Discipline>? GetDisciplinesWithFilter(int deptId, int course, string nGroup, int edukindId, int filId);
        public IQueryable<CaseSTeacher> GetCaseSTeachers();
        public CaseSTeacher? GetCaseSTeacherById(int id);
        public string? GetFioCaseSTeacherById(int? teacherId);
        public IQueryable<CaseUkoModule?>? GetCaseUkoModules();
        public List<CaseCEdukind>? GetEdukinds();
        public CaseCEdukind GetEdukindById(int edukindId);
    }
}
