﻿using ComputerExam.Services.Interfaces;
using DomainService.Entity;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamenController : Controller
    {
        private readonly IExamenRepository _examenRepository;
        private readonly IGeneratedExcelFile _generatedExcelFile;
        public ExamenController(IExamenRepository examenRepository, IGeneratedExcelFile generatedExcelFile)
        {
            _examenRepository = examenRepository;
            _generatedExcelFile = generatedExcelFile;
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
        /// Получение всех экзаменов по заданным фильтрам
        /// </summary>
        /// <returns></returns>
        [Route("GetExamensWithFilter")]
        [HttpGet]
        public IActionResult GetExamensWithFilter(int? filialId = null, int? departmentId = null, int? course = null, string? ngroup = null)
        {
            return Ok(_examenRepository.GetExamensWithFilter(filialId, departmentId, course, ngroup));
        }

        /// <summary>
        /// Запрос на получение данных об успеваемости студентов по examenId
        /// </summary>
        /// <param name="examenId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("GetStatisticForReport")]
        [HttpGet]
        public IActionResult GetStatisticForReport(int examenId)
        {
            return Ok(_examenRepository.GetStatisticForReport(examenId));
        }

        /// <summary>
        /// Запрос на генерацию эксель файла об успеваемости студентов по examenId
        /// </summary>
        /// <param name="examenId"></param>
        /// <returns>Строка с именем файла</returns>
        [Authorize]
        [Route("GenerateExcelFile")]
        [HttpGet]
        public IActionResult GenerateExcelFile(int examenId)
        {            
            return Ok(_generatedExcelFile.GenerateExcelFile(examenId));
        }

        /// <summary>
        /// Получение экзаменов по Id сотрудника
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("GetExamensByEmployeeId")]
        [HttpGet]
        public IActionResult GetExamensByEmployeeId(Guid employeeId)
        {
            return Ok(_examenRepository.GetExamensByEmployeeId(employeeId));
        }

        [Authorize]
        [Route("GetExamensFromArchiveByAuditoriumId")]
        [HttpGet]
        public IActionResult GetExamensFromArchiveByAuditoriumId(Guid auditoriumId)
        {
            return Ok(_examenRepository.GetExamensFromArchiveByAuditoriumId(auditoriumId));
        }

        [Authorize]
        [Route("GetExamensFromArchiveByFilter")]
        [HttpGet]
        public IActionResult GetExamensFromArchiveByFilter(int? filialId = null, int? facultyId = null, int? departmentId = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            return Ok(_examenRepository.GetExamensFromArchiveByFilter(filialId, facultyId, departmentId, startDate, endDate));
        }

        /// <summary>
        /// Получение экзаменов по Id аудитории
        /// </summary>
        /// <param name="auditoriumId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("GetExamensByAuditoriumId")]
        [HttpGet]
        public IActionResult GetExamensByAuditoriumId(Guid auditoriumId)
        {
            return Ok(_examenRepository.GetExamensByAuditoriumId(auditoriumId));
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
        [Authorize]
        [Route("GetStudentsByExamenIdForChecking")]
        [HttpGet]
        public IActionResult GetStudentsByExamenIdForChecking(int examenId)
        {
            var students = _examenRepository.GetStudentsByExamenIdForChecking(examenId);
            if (students == null)
                return BadRequest("Экзамен не найден");
            return Ok(students);
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
        /// Конец экзамена для сотрудника
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        [Authorize]
        [Route("EndExamenForEmployee")]
        [HttpGet]
        public async Task<IActionResult> EndExamenForEmployee(int examId)
        {
            var examen = _examenRepository.Get().FirstOrDefault(x=>x.Id == examId);
            if (examen == null)
                return BadRequest("Экзамен не найден");

            examen.EndExamDate = DateTime.Now;
            await _examenRepository.Update(examen);
            return Ok(_examenRepository.GetStudentsByExamenIdForChecking(examId));
        }

        /// <summary>
        /// Копирование экзамена
        /// </summary>
        /// <param name="examenId"></param>
        /// <param name="newExamDate"></param>
        /// <returns></returns>
        [Authorize(Roles = "testingDepartment, admin, uko")]
        [Route("CopyExamen")]
        [HttpPost]
        public async Task<IActionResult> CopyExamen(int examenId, DateTime newExamDate)
        {
            return Ok(await _examenRepository.CopyExamen(examenId, newExamDate));
        }

        /// <summary>
        /// Сброс экзамена
        /// </summary>
        /// <param name="examenId"></param>
        /// <returns></returns>
        [Authorize(Roles = "admin")]
        [Route("ResetExamen")]
        [HttpPost]
        public async Task<IActionResult> ResetExamen(int examenId)
        {
            var examen = _examenRepository.GetExamens().FirstOrDefault(x=>x.Id == examenId);
            if (examen == null)
                return BadRequest("Экзамен не найден");
            examen.EndExamDate = null;
            await _examenRepository.UpdateEntity(examen);
            return Ok();
        }

        /// <summary>
        /// Создание экзамена
        /// </summary>
        /// <param name="examen"></param>
        /// <returns></returns>
        [Authorize(Roles = "testingDepartment, admin, uko")]
        [Route("CreateExamen")]
        [HttpPost]
        public async Task<IActionResult> CreateExamen(Examen examen)
        {
            examen.ExamDate = examen.ExamDate.Value.AddHours(3);
            await _examenRepository.Create(examen);
            return Ok();
        }

        /// <summary>
        /// Обновление экзамена
        /// </summary>
        /// <param name="examen"></param>
        /// <returns></returns>
        [Authorize(Roles = "testingDepartment, admin, uko")]
        [Route("UpdateExamen")]
        [HttpPost]
        public async Task<IActionResult> UpdateExamen(Examen examen)
        {
            examen.ExamDate = examen.ExamDate.Value.AddHours(3);
            await _examenRepository.Update(examen);
            return Ok();
        }

        /// <summary>
        /// Удаление экзамена
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "testingDepartment, admin, uko")]
        [Route("DeleteExamen")]
        [HttpPost]
        public async Task<IActionResult> DeleteExamen(int id)
        {
            await _examenRepository.DeleteExamen(id);
            return Ok();
        }
    }
}