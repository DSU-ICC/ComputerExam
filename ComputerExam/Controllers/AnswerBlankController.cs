using DomainService.Entity;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        [Route("GetAnswerBlanks")]
        public async Task<IActionResult> GetAnswerBlanks()
        {
            return Ok(await _answerBlankRepository.GetAnswerBlanks().ToListAsync());
        }

        /// <summary>
        /// Получение бланка ответов студента по examId
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAnswerBlankByExamenIdAndStudentId")]
        public IActionResult GetAnswerBlankByExamenIdAndStudentId(int studentId, int examId)
        {
            return Ok(_answerBlankRepository.GetAnswerBlankByStudentIdAndExamenId(studentId, examId));
        }

        /// <summary>
        /// Получение бланка ответов по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAnswerBlankById")]
        public IActionResult GetAnswerBlankById(int id)
        {
            var answerBlank = _answerBlankRepository.GetAnswerBlankById(id);
            return Ok(answerBlank);
        }

        /// <summary>
        /// Получение всех бланков ответов и билетов по studentId
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAnswerBlanksAndTicketByStudentId")]
        public IActionResult GetAnswerBlanksAndTicketByStudentId(int studentId)
        {
            return Ok(_answerBlankRepository.GetAnswerBlanksAndTicketByStudentId(studentId));
        }

        /// <summary>
        /// Сброс бланка ответа студента
        /// </summary>
        /// <param name="answerBlankId"></param>
        /// <param name="isRemoveAnswerBlank"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("ResetAnswerBlank")]
        public async Task<IActionResult> ResetAnswerBlank(int answerBlankId, bool? isRemoveAnswerBlank)
        {
            var answerBlank = _answerBlankRepository.GetAnswerBlanks().FirstOrDefault(x => x.Id == answerBlankId);
            if (answerBlank == null)
                return BadRequest();
            if (isRemoveAnswerBlank == true)
            {
                answerBlank.IsDeleted = true;
                answerBlank.Answers?.ForEach(x => x.IsDeleted = true);
            }
            else
            {
                answerBlank.EndExamenDateTime = null;
                answerBlank.IsAuthorized = null;
            }

            await _answerBlankRepository.Update(answerBlank);
            return Ok();
        }

        /// <summary>
        /// Изменение в бланке ответов
        /// </summary>
        /// <param name="answerBlank"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateAnswerBlank")]
        public async Task<IActionResult> UpdateAnswerBlank(AnswerBlank answerBlank)
        {
            await _answerBlankRepository.Update(answerBlank);
            return Ok();
        }

        /// <summary>
        /// Конец экзамена для студента
        /// </summary>
        /// <param name="answerBlankId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EndExamenForStudent")]
        public async Task<IActionResult> EndExamenForStudent(int answerBlankId)
        {
            var answerBlank = _answerBlankRepository.FindById(answerBlankId);
            answerBlank.EndExamenDateTime = DateTime.Now;
            answerBlank.IsAuthorized = false;
            await _answerBlankRepository.Update(answerBlank);
            return Ok();
        }

        /// <summary>
        /// Удаление бланка ответов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("DeleteAnswerBlank")]
        public async Task<IActionResult> DeleteAnswerBlank(int id)
        {
            await _answerBlankRepository.Remove(id);
            return Ok();
        }
    }
}