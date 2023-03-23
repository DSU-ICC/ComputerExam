using ComputerExam.Common.Interfaces;
using ComputerExam.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("GetAnswers")]
        [HttpGet]
        public async Task<ActionResult<Answer>> GetAnswers()
        {
            return Ok(await _unitOfWork.AnswerRepository.Get().ToListAsync());
        }

        [Route("GetAnswerById")]
        [HttpGet]
        public ActionResult<Answer> GetAnswerById(int id)
        {
            return Ok(_unitOfWork.AnswerRepository.FindById(id));
        }

        [Route("CreateAnswer")]
        public async Task<ActionResult<Answer>> CreateAnswer(Answer answer)
        {
            await _unitOfWork.AnswerRepository.Create(answer);
            return Ok();
        }

        [Route("UpdateAnswer")]
        [HttpPut]
        public async Task<ActionResult<Answer>> UpdateAnswer(Answer answer)
        {
            await _unitOfWork.AnswerRepository.Update(answer);
            return Ok();
        }

        [Route("DeleteAnswer")]
        [HttpDelete]
        public async Task<ActionResult<Answer>> DeleteAnswer(Answer answer)
        {
            await _unitOfWork.AnswerRepository.Remove(answer);
            return Ok();
        }
    }
}
