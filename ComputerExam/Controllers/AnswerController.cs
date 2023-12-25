using DomainService.Entity;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerController : Controller
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        [Authorize]
        [Route("GetAnswers")]
        [HttpGet]
        public IActionResult GetAnswers()
        {
            return Ok(_answerRepository.Get());
        }

        [Route("GetAnswerById")]
        [HttpGet]
        public IActionResult GetAnswerById(int id)
        {
            return Ok(_answerRepository.FindById(id));
        }

        [Route("CreateAnswer")]
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(Answer answer)
        {
            answer.CreateAnswerDate = DateTime.Now;
            await _answerRepository.Create(answer);
            return Ok(answer);
        }

        [Route("UpdateAnswer")]
        [HttpPost]
        public async Task<IActionResult> UpdateAnswer(Answer answer)
        {
            answer.UpdateAnswerDate = DateTime.Now;
            await _answerRepository.Update(answer);
            return Ok();
        }

        [Authorize]
        [Route("DeleteAnswer")]
        [HttpPost]
        public async Task<IActionResult> DeleteAnswer(Answer answer)
        {
            await _answerRepository.Remove(answer);
            return Ok();
        }
    }
}
