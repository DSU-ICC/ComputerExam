using DSUContextDBService.DBService;
using DSUContextDBService.Interface;

namespace DSUContextDBService.Services
{
    public class DsuDbService : IDsuDbService
    {
        private readonly DSUContext _dSUContext;

        public DsuDbService(DSUContext dSUContext)
        {
            _dSUContext = dSUContext;
        }

        public CaseSDepartment GetCaseSDepartmentById(int? id)
        {
            return _dSUContext.CaseSDepartments.FirstOrDefault(x => x.DepartmentId == id);
        }

        public IQueryable<CaseSDepartment> GetCaseSDepartmentByFacultyId(int? id)
        {
            return _dSUContext.CaseSDepartments.Where(x => x.Deleted == false && x.FacId == id);
        }

        public IQueryable<CaseSDepartment> GetCaseSDepartments()
        {
            return _dSUContext.CaseSDepartments.Where(x => x.Deleted == false);
        }

        public IQueryable<CaseSSpecialization> GetCaseSSpecializations()
        {
            return _dSUContext.CaseSSpecializations.Where(x => x.Deleted == false);
        }

        public CaseSSpecialization GetCaseSSpecializationById(int? id)
        {
            return _dSUContext.CaseSSpecializations.FirstOrDefault(x => x.SpecId == id);
        }

        public IQueryable<CaseSStudent> GetCaseSStudents()
        {
            return _dSUContext.CaseSStudents.Where(x => x.Status == 0);
        }

        public CaseSStudent GetCaseSStudentById(int? id)
        {
            return _dSUContext.CaseSStudents.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<CaseSTeacher> GetCaseSTeachers()
        {
            return _dSUContext.CaseSTeachers.Where(x => x.TeachId > 0);
        }

        public CaseSTeacher GetCaseSTeacherById(int? id)
        {
            return _dSUContext.CaseSTeachers.FirstOrDefault(x => x.TeachId == id);
        }
    }
}
