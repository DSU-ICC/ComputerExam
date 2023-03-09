using ComputerExam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSUContextDBService.Interface
{
    public interface IDsuDbService
    {
        public IQueryable<CaseSDepartment> GetCaseSDepartments();
        public CaseSDepartment GetCaseSDepartmentById(int? id);
        public IQueryable<CaseSDepartment> GetCaseSDepartmentByFacultyId(int? id);
        public IQueryable<CaseSSpecialization> GetCaseSSpecializations();
        public CaseSSpecialization GetCaseSSpecializationById(int? id);
        public IQueryable<CaseSStudent> GetCaseSStudents();
        public CaseSStudent GetCaseSStudentById(int? id);
        public IQueryable<CaseSTeacher> GetCaseSTeachers();
        public CaseSTeacher GetCaseSTeacherById(int? id);
    }
}
