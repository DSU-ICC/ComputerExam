using DomainService.Entity;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnswerBlankController : Controller
    {
        private readonly IAnswerBlankRepository _answerBlankRepository;
        private readonly IExamenRepository _examenRepository;

        public AnswerBlankController(IAnswerBlankRepository answerBlankRepository, IExamenRepository examenRepository)
        {
            _answerBlankRepository = answerBlankRepository;
            _examenRepository = examenRepository;
        }

        [HttpGet]
        [Route("GetAnswerBlanks")]
        public async Task<IActionResult> GetAnswerBlanks()
        {
            return Ok(await _answerBlankRepository.Get().ToListAsync());
        }

        [HttpGet]
        [Route("GetAnswerBlanks")]
        public async Task<IActionResult> GetAnswerBlanksByExamenId(int examTicketId)
        {
            return Ok(await _answerBlankRepository.Get().Where(x => x.ExamTicketId == examTicketId).ToListAsync());
        }

        [HttpGet]
        [Route("GetAnswerBlankById")]
        public IActionResult GetAnswerBlankById(int id)
        {
            return Ok(_answerBlankRepository.FindById(id));
        }

        /// <summary>
        /// Старт экзамена
        /// </summary>
        /// <param name="answerBlank"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateAnswerBlank")]
        public async Task<IActionResult> CreateAnswerBlank(AnswerBlank answerBlank)
        {
            var examen = _examenRepository.Get().Include(x => x.Tickets).FirstOrDefault(c => c.Id == answerBlank.ExamTicketId);
            if (examen == null)
                return BadRequest("Экзамен не найден");
            var ticket = examen.Tickets.FirstOrDefault(x => x.Id == answerBlank.ExamTicketId);

            await _answerBlankRepository.Create(answerBlank);
            return Ok();
        }

        /// <summary>
        /// Изменение в бланке ответов
        /// </summary>
        /// <param name="answerBlank"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateAnswerBlank")]
        public async Task<IActionResult> UpdateAnswerBlank(AnswerBlank answerBlank)
        {
            await _answerBlankRepository.Update(answerBlank);
            return Ok();
        }

        /// <summary>
        /// Удаление бланка ответов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteAnswerBlank")]
        public async Task<IActionResult> DeleteAnswerBlank(int id)
        {
            await _answerBlankRepository.Remove(id);
            return Ok();
        }
    }
}
