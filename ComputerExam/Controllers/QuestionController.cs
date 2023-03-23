using ComputerExam.Common.Interfaces;
using ComputerExam.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("GetQuestions")]
        [HttpGet]
        public async Task<ActionResult<Question>> GetQuestions()
        {
            return Ok(await _unitOfWork.QuestionRepository.Get().ToListAsync());
        }

        [Route("GetQuestionById")]
        [HttpGet]
        public ActionResult<Question> GetQuestionById(int id)
        {
            return Ok(_unitOfWork.QuestionRepository.FindById(id));
        }

        [Route("CreateQuestion")]
        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion(Question question)
        {
            await _unitOfWork.QuestionRepository.Create(question);
            return Ok();
        }

        [Route("UpdateQuestion")]
        [HttpPut]
        public async Task<ActionResult<Question>> UpdateQuestion(Question question)
        {
            await _unitOfWork.QuestionRepository.Update(question);
            return Ok();
        }

        [Route("DeleteQuestion")]
        [HttpDelete]
        public async Task<ActionResult<Question>> DeleteQuestion(Question question)
        {
            await _unitOfWork.QuestionRepository.Remove(question);
            return Ok();
        }
    }
}
