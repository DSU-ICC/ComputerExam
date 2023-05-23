using DomainService.Entity;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        private readonly IAnswerRepository _answerRepository;

        public AnswerController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

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

        [Authorize]
        [Route("CreateAnswer")]
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(Answer answer)
        {
            await _answerRepository.Create(answer);
            return Ok(answer);
        }

        [Authorize]
        [Route("UpdateAnswer")]
        [HttpPut]
        public async Task<IActionResult> UpdateAnswer(Answer answer)
        {
            await _answerRepository.Update(answer);
            return Ok();
        }

        [Authorize]
        [Route("DeleteAnswer")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAnswer(Answer answer)
        {
            await _answerRepository.Remove(answer);
            return Ok();
        }
    }
}
