using DSUContextDBService.DBService;
using DSUContextDBService.Models;

namespace DSUContextDBService.Interface
{
    public interface IDsuDbService
    {
        public IQueryable<CaseCFaculty> GetFaculties();
        public CaseCFaculty GetFacultyById(int? id);
        public IQueryable<CaseSDepartment> GetCaseSDepartments();
        public CaseSDepartment GetCaseSDepartmentById(int? id);
        public IQueryable<CaseSDepartment> GetCaseSDepartmentByFacultyId(int? id);
        public IQueryable<CaseSSpecialization> GetCaseSSpecializations();
        public CaseSSpecialization GetCaseSSpecializationById(int? id);
        public IQueryable<int?> GetCoursesByDepartmentId(int departmentId);
        public IQueryable<string?> GetGroupsByDepartmentId(int departmentId, int course);
        public IQueryable<CaseSStudent> GetCaseSStudents();
        public CaseSStudent GetCaseSStudentById(int? id);
        public IQueryable<CaseSTeacher> GetCaseSTeachers();
        public CaseSTeacher GetCaseSTeacherById(int? id);
    }
}
