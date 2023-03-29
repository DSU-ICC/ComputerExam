using DomainService.Entity;
using Infrastructure.Repositories.Interfaces;
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
        private readonly IAnswerRepository _answerRepository;

        public AnswerController(IAnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        [Route("GetAnswers")]
        [HttpGet]
        public async Task<ActionResult<Answer>> GetAnswers()
        {
            return Ok(await _answerRepository.Get().ToListAsync());
        }

        [Route("GetAnswerById")]
        [HttpGet]
        public ActionResult<Answer> GetAnswerById(int id)
        {
            return Ok(_answerRepository.FindById(id));
        }

        [Route("CreateAnswer")]
        [HttpPost]
        public async Task<ActionResult<Answer>> CreateAnswer(Answer answer)
        {
            await _answerRepository.Create(answer);
            return Ok();
        }

        [Route("UpdateAnswer")]
        [HttpPut]
        public async Task<ActionResult<Answer>> UpdateAnswer(Answer answer)
        {
            await _answerRepository.Update(answer);
            return Ok();
        }

        [Route("DeleteAnswer")]
        [HttpDelete]
        public async Task<ActionResult<Answer>> DeleteAnswer(Answer answer)
        {
            await _answerRepository.Remove(answer);
            return Ok();
        }
    }
}
