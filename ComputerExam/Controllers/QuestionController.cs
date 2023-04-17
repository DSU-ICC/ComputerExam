using DomainService.Entity;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
        public async Task<IActionResult> GetQuestions()
        {
            return Ok(await _questionRepository.Get().ToListAsync());
        }

        [Route("GetQuestionsByExamenId")]
        [HttpGet]
        public async Task<IActionResult> GetQuestionsByExamenId(int examenId)
        {
            return Ok(await _questionRepository.Get().Where(x => x.ExamTicketId == examenId).ToListAsync());
        }

        [Route("GetQuestionById")]
        [HttpGet]
        public IActionResult GetQuestionById(int id)
        {
            return Ok(_questionRepository.FindById(id));
        }

        [Route("CreateQuestion")]
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(Question question)
        {
            await _questionRepository.Create(question);
            return Ok();
        }

        [Route("CreateQuestionByListQuestion")]
        [HttpPost]
        public async Task<IActionResult> CreateQuestionByListQuestion(string questions, int examenId)
        {
            var paragraph = questions.Split("№");
            foreach (var item in paragraph.Skip(1))
            {
                Question question = new()
                {
                    ExamTicketId = examenId,
                    Number = int.Parse(item.Split("- ")[0]),
                    Text = item.Split("- ")[1],
                };
                await _questionRepository.Create(question);
            }
            return Ok();
        }

        [Route("UpdateQuestion")]
        [HttpPut]
        public async Task<IActionResult> UpdateQuestion(Question question)
        {
            await _questionRepository.Update(question);
            return Ok();
        }

        [Route("DeleteQuestion")]
        [HttpDelete]
        public async Task<IActionResult> DeleteQuestion(Question question)
        {
            await _questionRepository.Remove(question);
            return Ok();
        }
    }
}
