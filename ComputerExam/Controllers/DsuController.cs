using DSUContextDBService.Interface;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [Route("GetFaculties")]
        [HttpGet]
        public async Task<IActionResult> GetFaculties()
        {
            return Ok(await _dsuDbService.GetFaculties().ToListAsync());
        }

        [Route("GetCaseSDepartmentByFacultyId")]
        [HttpGet]
        public async Task<IActionResult> GetCaseSDepartmentByFacultyId(int facultyId)
        {
            return Ok(await _dsuDbService.GetCaseSDepartmentByFacultyId(facultyId).ToListAsync());
        }

        [Route("GetCourseByDepartmentId")]
        [HttpGet]
        public async Task<IActionResult> GetCourseByDepartmentId(int departmentId)
        {
            return Ok(await _dsuDbService.GetCoursesByDepartmentId(departmentId).ToListAsync());
        }

        [Route("GetStudentsByDepartmentAndCourse")]
        [HttpGet]
        public async Task<IActionResult> GetStudentsByDepartmentAndCourse(int departmentId, int course)
        {
            var students = await _dsuDbService.GetCaseSStudents().Where(x => x.DepartmentId == departmentId && x.Course == course).ToListAsync();
            return Ok(students);
        }

        [Route("SignInStudent")]
        [HttpGet]
        public IActionResult SignInStudent(int studentId, string nzachkn)
        {
            var student = _dsuDbService.GetCaseSStudents().FirstOrDefault(x => x.Id == studentId && x.Nzachkn == nzachkn);
            if (student != null)
                return Ok();
            return BadRequest();
        }

        [Route("GetTeachers")]
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            return Ok(await _dsuDbService.GetCaseSTeachers().ToListAsync());
        }
    }
}
