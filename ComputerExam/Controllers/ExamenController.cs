using DomainService.Entity;
using DSUContextDBService.Interface;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamenController : Controller
    {
        private readonly IExamenRepository _examenRepository;
        private readonly IDsuDbService _dsuDbService;

        public ExamenController(IExamenRepository examenRepository, IDsuDbService dsuDbService)
        {
            _examenRepository = examenRepository;
            _dsuDbService = dsuDbService;
        }

        [Route("GetExamens")]
        [HttpGet]
        public async Task<IActionResult> GetExamensAsync()
        {
            return Ok(await _examenRepository.Get().ToListAsync());
        }

        [Route("GetExamenById")]
        [HttpGet]
        public IActionResult GetExamenById(int id)
        {
            return Ok(_examenRepository.FindById(id));
        }

        [Route("GetExamenByStudentId")]
        [HttpGet]
        public IActionResult GetExamenByStudentId(int studentId)
        {
            var student = _dsuDbService.GetCaseSStudentById(studentId);
            var examens = _examenRepository.Get().Where(x => x.DepartmentId == student.DepartmentId && x.Course == student.Course && x.NGroup == student.Ngroup);
            return Ok(examens);
        }

        [Route("GetExamensByStudentId")]
        [HttpGet]
        public async Task<IActionResult> GetExamensByStudentId(int studentId)
        {
            var examens = _examenRepository.GetExamensByStudentId(studentId);
            if (examens == null)
                return BadRequest("Не найден данный студент");
            return Ok(examens);
        }

        [Route("CreateExamen")]
        [HttpPost]
        public async Task<IActionResult> CreateExamen(Examen examen)
        {
            await _examenRepository.Create(examen);
            return Ok();
        }

        [Route("UpdateExamen")]
        [HttpPut]
        public async Task<IActionResult> UpdateExamen(Examen examen)
        {
            await _examenRepository.Update(examen);
            return Ok();
        }

        [Route("DeleteExamen")]
        [HttpDelete]
        public async Task<IActionResult> DeleteExamen(int id)
        {
            await _examenRepository.Remove(id);
            return Ok();
        }
    }
}
