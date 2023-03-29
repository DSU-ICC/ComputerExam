
using DomainService.Entity;
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

        public ExamenController(IExamenRepository examenRepository)
        {
            _examenRepository = examenRepository;
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
