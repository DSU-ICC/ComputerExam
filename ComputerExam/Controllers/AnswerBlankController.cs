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

        public AnswerBlankController(IAnswerBlankRepository answerBlankRepository)
        {
            _answerBlankRepository = answerBlankRepository;
        }

        [HttpGet]
        [Route("GetAnswerBlanks")]
        public async Task<IActionResult> GetAnswerBlanks()
        {
            return Ok(await _answerBlankRepository.Get().ToListAsync());
        }

        [HttpGet]
        [Route("GetAnswerBlanksByExamenIdAndStudentId")]
        public IActionResult GetAnswerBlanksByExamenIdAndStudentId(int examId, int studentId)
        {
            return Ok(_answerBlankRepository.Get().Include(x => x.Answers).FirstOrDefault(x => x.StudentId == studentId && x.ExamTicket.ExamenId == examId));
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
            var answerBlankAndTicketDto = answerBlank.Select(x => new AnswerBlankAndTicketDto()
            {
                AnswerBlank = x,
                Ticket = x.ExamTicket
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
        [Route("EndExamenForStudent")]
        public async Task<IActionResult> EndExamenForStudent(AnswerBlank answerBlank)
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