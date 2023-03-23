using ComputerExam.Common.Interfaces;
using ComputerExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamenController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamenController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("GetExamens")]
        [HttpGet]
        public async Task<ActionResult> GetExamensAsync()
        {
            return Ok(await _unitOfWork.ExamenRepository.Get().ToListAsync());
        }

        [Route("GetExamenById")]
        [HttpGet]
        public ActionResult<Examen> GetExamenById(int id)
        {
            return Ok(_unitOfWork.ExamenRepository.FindById(id));
        }

        [Route("CreateExamen")]
        [HttpPost]
        public async Task<ActionResult<Examen>> CreateExamen(Examen examen)
        {
            await _unitOfWork.ExamenRepository.Create(examen);
            return Ok();
        }

        [Route("UpdateExamen")]
        [HttpPut]
        public async Task<ActionResult<Examen>> UpdateExamen(Examen examen)
        {
            await _unitOfWork.ExamenRepository.Update(examen);
            return Ok();
        }

        [Route("DeleteExamen")]
        [HttpDelete]
        public async Task<ActionResult<Examen>> DeleteExamen(int id)
        {
            await _unitOfWork.ExamenRepository.Remove(id);
            return Ok();
        }
    }
}
