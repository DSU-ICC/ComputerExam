using DomainService.DtoModels;
using DomainService.Entity;
using DSUContextDBService.Interface;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamenController : Controller
    {
        private readonly IExamenRepository _examenRepository;

        public ExamenController(IExamenRepository examenRepository)
        {
            _examenRepository = examenRepository;
        }

        /// <summary>
        /// Получение всех активных экзаменов
        /// </summary>
        /// <returns></returns>
        [Route("GetExamens")]
        [HttpGet]
        public IActionResult GetExamens()
        {
            return Ok(_examenRepository.GetExamens());
        }

        /// <summary>
        /// Получение экзаменов по Id сотрудника
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Route("GetExamensByEmployeeId")]
        [HttpGet]
        public IActionResult GetExamensByEmployeeId(Guid employeeId)
        {
            return Ok(_examenRepository.GetExamensByEmployeeId(employeeId));
        }

        /// <summary>
        /// Получение экзаменов по Id студента
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [Route("GetExamensByStudentId")]
        [HttpGet]
        public IActionResult GetExamensByStudentId(int studentId)
        {
            return Ok(_examenRepository.GetExamensByStudentId(studentId));
        }

        /// <summary>
        /// Получение студентов по Id экзамена
        /// </summary>
        /// <param name="examenId"></param>
        /// <returns></returns>
        [Route("GetStudentsByExamenId")]
        [HttpGet]
        public IActionResult GetStudentsByExamenId(int examenId)
        {
            return Ok(_examenRepository.GetStudentsByExamenId(examenId));
        }

        /// <summary>
        /// Получение студентов по Id экзамена для проверки их ответов преподавателем
        /// </summary>
        /// <param name="examenId"></param>
        /// <returns></returns>
        [Route("GetStudentsByExamenIdForChecking")]
        [HttpGet]
        public IActionResult GetStudentsByExamenIdForChecking(int examenId)
        {
            return Ok(_examenRepository.GetStudentsByExamenIdForChecking(examenId));
        }

        /// <summary>
        /// Функция начала экзамена
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="examId"></param>
        /// <returns></returns>
        [Route("StartExamen")]
        [HttpGet]
        public async Task<IActionResult> StartExamen(int studentId, int examId)
        {
            var examen = await _examenRepository.StartExamen(studentId, examId);
            if (examen == null)
                return BadRequest("Ошибка при попытке начать экзамен. Проверьте дату начала экзамена");

            return Ok(examen);
        }

        /// <summary>
        /// Создание экзамена
        /// </summary>
        /// <param name="examen"></param>
        /// <returns></returns>
        [Route("CreateExamen")]
        [HttpPost]
        public async Task<IActionResult> CreateExamen(Examen examen)
        {
            await _examenRepository.Create(examen);
            return Ok();
        }

        /// <summary>
        /// Обновление экзамена
        /// </summary>
        /// <param name="examen"></param>
        /// <returns></returns>
        [Route("UpdateExamen")]
        [HttpPut]
        public async Task<IActionResult> UpdateExamen(Examen examen)
        {
            await _examenRepository.Update(examen);
            return Ok();
        }

        /// <summary>
        /// Удаление экзамена
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("DeleteExamen")]
        [HttpDelete]
        public IActionResult DeleteExamen(int id)
        {
            _examenRepository.DeleteExamen(id);
            return Ok();
        }
    }
}