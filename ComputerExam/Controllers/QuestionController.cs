using DomainService.Entity;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        [Route("GetQuestions")]
        [HttpGet]
        public async Task<ActionResult<Question>> GetQuestions()
        {
            return Ok(await _questionRepository.Get().ToListAsync());
        }

        [Route("GetQuestionById")]
        [HttpGet]
        public ActionResult<Question> GetQuestionById(int id)
        {
            return Ok(_questionRepository.FindById(id));
        }

        [Route("CreateQuestion")]
        [HttpPost]
        public async Task<ActionResult<Question>> CreateQuestion(Question question)
        {
            await _questionRepository.Create(question);
            return Ok();
        }

        [Route("UpdateQuestion")]
        [HttpPut]
        public async Task<ActionResult<Question>> UpdateQuestion(Question question)
        {
            await _questionRepository.Update(question);
            return Ok();
        }

        [Route("DeleteQuestion")]
        [HttpDelete]
        public async Task<ActionResult<Question>> DeleteQuestion(Question question)
        {
            await _questionRepository.Remove(question);
            return Ok();
        }
    }
}
