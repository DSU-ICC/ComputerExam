using DSUContextDBService.Interface;
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
        public IActionResult GetCourseByDepartmentId(int departmentId)
        {
            return Ok(_dsuDbService.GetCoursesByDepartmentId(departmentId));
        }

        [Route("GetGroupsByDepartmentIdAndCourse")]
        [HttpGet]
        public IActionResult GetGroupsByDepartmentIdAndCourse(int departmentId, int course)
        {
            return Ok(_dsuDbService.GetGroupsByDepartmentId(departmentId, course));
        }

        [Route("GetStudentsByCourse")]
        [HttpGet]
        public IActionResult GetStudentsByCourse(int departmentId, int course)
        {
            return Ok(_dsuDbService.GetCaseSStudents().Where(x => x.DepartmentId == departmentId && x.Course == course));
        }

        [Route("GetStudentsByCourseAndGroup")]
        [HttpGet]
        public IActionResult GetStudentsByCourseAndGroup(int departmentId, int course, string ngroup)
        {
            return Ok(_dsuDbService.GetCaseSStudents().Where(x => x.DepartmentId == departmentId && x.Course == course && x.Ngroup == ngroup));
        }

        [Route("GetDisciplinesWithFilter")]
        [HttpGet]
        public IActionResult GetDisciplinesByStudentInfo(int departmentId, int course, string ngroup, int edukindId, int filId = 1)
        {
            return Ok(_dsuDbService.GetDisciplinesWithFilter(departmentId, course, ngroup, edukindId, filId));
        }
        
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
