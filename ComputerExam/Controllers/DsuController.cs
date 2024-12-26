using DSUContextDBService.DtoModels;
using DSUContextDBService.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DsuController : Controller
    {
        private readonly IDsuDbService _dsuDbService;

        public DsuController(IDsuDbService dsuDbService)
        {
            _dsuDbService = dsuDbService;
        }

        [Route("GetEdukinds")]
        [HttpGet]
        public IActionResult GetEdukinds()
        {
            return Ok(_dsuDbService.GetEdukinds());
        }

        [Route("GetFilials")]
        [HttpGet]
        public IActionResult GetFilials()
        {
            return Ok(_dsuDbService.GetFilials());
        }

        [Route("GetFaculties")]
        [HttpGet]
        public IActionResult GetFaculties()
        {
            return Ok(_dsuDbService.GetFaculties());
        }

        [Route("GetCaseSDepartmentByFacultyId")]
        [HttpGet]
        public IActionResult GetCaseSDepartmentByFacultyId(int facultyId)
        {
            return Ok(_dsuDbService.GetCaseSDepartmentByFacultyId(facultyId));
        }

        [Route("GetCourseByDepartmentId")]
        [HttpGet]
        public IActionResult GetCourseByDepartmentId(int departmentId, int filialId)
        {
            return Ok(_dsuDbService.GetCoursesByDepartmentId(filialId, departmentId));
        }

        [Route("GetGroupsByDepartmentIdAndCourse")]
        [HttpGet]
        public IActionResult GetGroupsByDepartmentIdAndCourse(int departmentId, int course, int filialId)
        {
            return Ok(_dsuDbService.GetGroupsByDepartmentId(departmentId, course, filialId));
        }

        [Route("GetStudentsByCourse")]
        [HttpGet]
        public IActionResult GetStudentsByCourse(int departmentId, int course, int filialId)
        {
            return Ok(_dsuDbService.GetCaseSStudents().Where(x => x.FilId == filialId && 
                                                                  x.DepartmentId == departmentId && 
                                                                  x.Course == course));
        }

        [Route("GetStudentsByCourseAndGroup")]
        [HttpGet]
        public IActionResult GetStudentsByCourseAndGroup(int departmentId, int course, string ngroup, int filialId)
        {
            return Ok(_dsuDbService.GetCaseSStudents()
                    .Where(x => x.FilId == filialId &&
                                x.DepartmentId == departmentId &&
                                x.Course == course && 
                                x.Ngroup == ngroup)
                    .Select(x => new StudentDtoForListOutput()
                    {
                        Id = x.Id,
                        Firstname = x.Firstname,
                        Lastname = x.Lastname,
                        Patr = x.Patr,
                        Nzachkn = x.Nzachkn
                    }));
        }

        [Route("GetDisciplinesWithFilter")]
        [HttpGet]
        public IActionResult GetDisciplinesByStudentInfo(int departmentId, int course, string ngroup, int edukindId, int filId)
        {
            return Ok(_dsuDbService.GetDisciplinesWithFilter(departmentId, course, ngroup, edukindId, filId));
        }

        //[Authorize]
        [Route("GetTeachers")]
        [HttpGet]
        public IActionResult GetTeachers()
        {
            return Ok(_dsuDbService.GetCaseSTeachers());
        }

        [Route("SignInStudent")]
        [HttpPost]
        public IActionResult SignInStudent(int studentId, string nzachkn)
        {
            var student = _dsuDbService.GetCaseSStudents().FirstOrDefault(x => x.Id == studentId && x.Nzachkn == nzachkn);
            if (student != null)
                return Ok();
            return BadRequest();
        }
    }
}
