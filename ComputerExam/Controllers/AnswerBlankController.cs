using DomainService.DtoModels;
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
        private readonly IExamTicketRepository _examTicketRepository;

        public AnswerBlankController(IAnswerBlankRepository answerBlankRepository, IExamTicketRepository examTicketRepository)
        {
            _answerBlankRepository = answerBlankRepository;
            _examTicketRepository = examTicketRepository;
        }

        [HttpGet]
        [Route("GetAnswerBlanks")]
        public async Task<IActionResult> GetAnswerBlanks()
        {
            return Ok(await _answerBlankRepository.Get().ToListAsync());
        }

        [HttpGet]
        [Route("GetAnswerBlanksByExamenIdStudentId")]
        public IActionResult GetAnswerBlanksByExamenIdStudentId(int examTicketId, int studentId)
        {
            return Ok(_answerBlankRepository.Get().Include(x => x.Answers).FirstOrDefault(x => x.ExamTicketId == examTicketId && x.StudentId == studentId));
        }

        [HttpGet]
        [Route("GetAnswerBlankById")]
        public IActionResult GetAnswerBlankById(int id)
        {
            return Ok(_answerBlankRepository.FindById(id));
        }

        [HttpGet]
        [Route("GetAnswerBlankAndTicketByStudentId")]
        public IActionResult GetAnswerBlankAndTicketByStudentId(int studentId)
        {
            var answerBlank = _answerBlankRepository.Get().Include(x => x.Answers).Where(x => x.StudentId == studentId);
            var tickets = _examTicketRepository.Get().Include(x => x.Questions);

            var answerBlankAndTicketDto = answerBlank.Select(x => new AnswerBlankAndTicketDto()
            {
                AnswerBlank = x,
                Ticket = tickets.FirstOrDefault(c => c.Id == x.ExamTicketId)
            });

            return Ok(answerBlankAndTicketDto);
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
        /// Конец экзамена
        /// </summary>
        /// <param name="answerBlank"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("EndExamen")]
        public async Task<IActionResult> EndExamen(AnswerBlank answerBlank)
        {
            answerBlank.EndExamenDateTime = DateTime.Now;
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