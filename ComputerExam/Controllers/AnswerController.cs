using DomainService.Entity;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [Route("GetAnswers")]
        [HttpGet]
        public async Task<IActionResult> GetAnswers()
        {
            return Ok(await _answerRepository.Get().ToListAsync());
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
            await _answerRepository.Create(answer);
            return Ok(answer);
        }

        [Route("UpdateAnswer")]
        [HttpPut]
        public async Task<IActionResult> UpdateAnswer(Answer answer)
        {
            await _answerRepository.Update(answer);
            return Ok();
        }

        [Route("DeleteAnswer")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAnswer(Answer answer)
        {
            await _answerRepository.Remove(answer);
            return Ok();
        }
    }
}
