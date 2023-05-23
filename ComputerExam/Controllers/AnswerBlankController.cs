using DomainService.DtoModels;
using DomainService.Entity;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [Route("GetAnswerBlankByExamenIdAndStudentId")]
        public IActionResult GetAnswerBlankByExamenIdAndStudentId(int studentId, int examId)
        {
            return Ok(_answerBlankRepository.GetAnswerBlankByStudentIdAndExamenId(studentId, examId));
        }

        [HttpGet]
        [Route("GetAnswerBlankById")]
        public IActionResult GetAnswerBlankById(int id)
        {
            return Ok(_answerBlankRepository.Get().Include(x => x.Answers).FirstOrDefault(x => x.Id == id));
        }

        [HttpGet]
        [Route("GetAnswerBlanksAndTicketByStudentId")]
        public IActionResult GetAnswerBlanksAndTicketByStudentId(int studentId)
        {
            return Ok(_answerBlankRepository.GetAnswerBlanksAndTicketByStudentId(studentId));
        }

        /// <summary>
        /// Изменение в бланке ответов
        /// </summary>
        /// <param name="answerBlank"></param>
        /// <returns></returns>
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpDelete]
        [Route("DeleteAnswerBlank")]
        public async Task<IActionResult> DeleteAnswerBlank(int id)
        {
            await _answerBlankRepository.Remove(id);
            return Ok();
        }
    }
}