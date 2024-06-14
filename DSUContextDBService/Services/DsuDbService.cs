using DomainService.DtoModels;
using DSUContextDBService.DBService;
using DSUContextDBService.DtoModels;
using DSUContextDBService.Interface;
using DSUContextDBService.Models;

namespace DSUContextDBService.Services
{
    public class DsuDbService : IDsuDbService
    {
        private readonly DSUContext _dSUContext;

        public DsuDbService(DSUContext dSUContext)
        {
            _dSUContext = dSUContext;
        }

        #region Filial
        public IQueryable<CaseCFilial> GetFilials()
        {
            return _dSUContext.CaseCFilials.Where(x => x.Deleted == false).OrderBy(x => x.Filial);
        }

        public CaseCFilial? GetFilialsById(int id)
        {
            return _dSUContext.CaseCFilials.FirstOrDefault(x => x.FilId == id);
        }
        #endregion

        #region Faculties
        public IQueryable<CaseCFaculty> GetFaculties()
        {
            return _dSUContext.CaseCFaculties.Where(x => x.Deleted == false).OrderBy(x => x.FacName);
        }

        public CaseCFaculty? GetFacultyById(int id)
        {
            return _dSUContext.CaseCFaculties.FirstOrDefault(x => x.FacId == id);
        }
        #endregion

        #region Departments
        public CaseSDepartment? GetCaseSDepartmentById(int id)
        {
            return _dSUContext.CaseSDepartments.FirstOrDefault(x => x.DepartmentId == id);
        }

        public IQueryable<CaseSDepartment>? GetCaseSDepartmentByFacultyId(int id)
        {
            var students = GetCaseSStudents();
            return _dSUContext.CaseSDepartments.Where(x => x.Deleted == false && x.FacId == id && students.Any(c => c.DepartmentId == x.DepartmentId)).OrderBy(x => x.DeptName);
        }

        public IQueryable<CaseSDepartment> GetCaseSDepartments()
        {
            return _dSUContext.CaseSDepartments.Where(x => x.Deleted == false).OrderBy(x => x.DeptName);
        }
        #endregion

        #region Specializations
        public IQueryable<CaseSSpecialization> GetCaseSSpecializations()
        {
            return _dSUContext.CaseSSpecializations.Where(x => x.Deleted == false).OrderBy(x => x.SpecName);
        }

        public CaseSSpecialization? GetCaseSSpecializationById(int? id)
        {
            return _dSUContext.CaseSSpecializations.FirstOrDefault(x => x.SpecId == id);
        }
        #endregion

        #region Students
        public IQueryable<StudentDtoForListOutput> GetStudentDtoForListOutput(int filId)
        {
            return _dSUContext.CaseSStudents.Where(x => x.Status == 0 && x.FilId == filId)
                            .OrderBy(x => x.Lastname)
                            .ThenBy(x => x.Firstname)
                            .ThenBy(x => x.Patr).Select(x => new StudentDtoForListOutput()
                            {
                                Id = x.Id,
                                Firstname = x.Firstname,
                                Lastname = x.Lastname,
                                Patr = x.Patr,
                                Nzachkn = x.Nzachkn
                            });

        }

        public IQueryable<CaseSStudent> GetCaseSStudents()
        {
            return _dSUContext.CaseSStudents.Where(x => x.Status == 0)
                            .OrderBy(x => x.Lastname)
                            .ThenBy(x => x.Firstname)
                            .ThenBy(x => x.Patr);

        }

        public CaseSStudent? GetCaseSStudentById(int id)
        {
            return _dSUContext.CaseSStudents.FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<CaseSSubject>? GetPredmets()
        {
            return _dSUContext.CaseSSubjects.Where(x => x.Deleted == false);
        }

        public IQueryable<Discipline>? GetDisciplinesWithFilter(int deptId, int course, string nGroup, int edukindId, int filId)
        {
            var yearStartEdu = DateTime.Now.Year - course;
            var tplans = _dSUContext.CaseSTplans.Where(x => x.FilId == filId && x.DeptId == deptId && x.EdukindId == edukindId && x.Y == yearStartEdu);
            if (tplans.Any())
            {
                var tplanDetails = _dSUContext.CaseSTplandetails.Where(x => x.Exam == 1 && x.PId == tplans.First().PId);
                if (tplanDetails.Any())
                {
                    var modules = _dSUContext.CaseUkoModules.Where(x => tplanDetails.Any(c => c.SessId == x.SessId && c.SId == x.SId) &&
                        x.StudentStatus == 0 && x.Nmod == 1 && x.DeptId == deptId && x.EdukindId == edukindId && x.Ngroup == nGroup);
                    int maxSemestr = 1;
                    IQueryable<CaseSSubject> predmets;
                    if (modules.Any())
                    {
                        maxSemestr = modules.Max(x => x.SessId);
                        predmets = GetPredmets().Where(x => tplanDetails.Any(c => c.SId == x.SId && c.SessId == maxSemestr));
                    }
                    else
                        predmets = GetPredmets().Where(x => tplanDetails.Any(c => c.SId == x.SId));

                    var disciplines = predmets.Select(x => new Discipline()
                    {
                        DisciplineId = x.SId,
                        Predmet = x.SName,
                    });

                    return disciplines.Distinct();
                }
            }
            return null;
        }

        #endregion

        #region Teachers
        public IQueryable<CaseSTeacher> GetCaseSTeachers()
        {
            return _dSUContext.CaseSTeachers.Where(x => x.TeachId > 0);
        }

        public CaseSTeacher? GetCaseSTeacherById(int id)
        {
            return _dSUContext.CaseSTeachers.FirstOrDefault(x => x.TeachId == id);
        }

        public string? GetFioCaseSTeacherById(int? teacherId)
        {
            var teacher = _dSUContext.CaseSTeachers.FirstOrDefault(x => x.TeachId == teacherId);
            return teacher?.Lastname + " " + teacher?.Firstname + " " + teacher?.Patr;
        }
        #endregion

        public List<CaseCEdukind>? GetEdukinds()
        {
            return _dSUContext.CaseCEdukinds.ToList();
        }

        public CaseCEdukind GetEdukindById(int edukindId)
        {
            return _dSUContext.CaseCEdukinds.FirstOrDefault(x => x.EdukindId == edukindId);
        }

        public List<int>? GetCoursesByDepartmentId(int filialId, int departmentId)
        {
            return GetCaseSStudents()
                .Where(x => x.FilId == filialId && x.DepartmentId == departmentId)
                .Select(c => c.Course)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public List<string?>? GetGroupsByDepartmentId(int departmentId, int course, int filialId)
        {
            return GetCaseSStudents()
                .Where(x => x.FilId == filialId &&
                            x.DepartmentId == departmentId &&
                            x.Course == course)
                .Select(c => c.Ngroup)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public IQueryable<CaseUkoModule?>? GetCaseUkoModules()
        {
            return _dSUContext.CaseUkoModules.Where(x => x.Closed);
        }
    }
}
