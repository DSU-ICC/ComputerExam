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
        private readonly IDsuDbService _dsuDbService;
        private readonly IExamTicketRepository _examTicketRepository;
        private readonly IAnswerBlankRepository _answerBlankRepository;

        public ExamenController(IExamenRepository examenRepository, IDsuDbService dsuDbService, IExamTicketRepository examTicketRepository, IAnswerBlankRepository answerBlankRepository)
        {
            _examenRepository = examenRepository;
            _dsuDbService = dsuDbService;
            _examTicketRepository = examTicketRepository;
            _answerBlankRepository = answerBlankRepository;
        }

        [Route("GetExamens")]
        [HttpGet]
        public async Task<IActionResult> GetExamensAsync()
        {
            return Ok(await _examenRepository.Get().ToListAsync());
        }

        [Route("GetExamensByTeacherId")]
        [HttpGet]
        public async Task<IActionResult> GetExamensByTeacherId(int teacherId)
        {
            var examenDto = await _examenRepository.Get()
               .Where(x => x.IdTeacher == teacherId)
               .Select(i => new ExamenDto()
               {
                   ExamenId = i.Id,
                   Discipline = i.Discipline,
                   Group = i.NGroup,
                   Course = (int)i.Course,
                   Department = _dsuDbService.GetCaseSDepartmentById(i.DepartmentId),
               }).ToListAsync();
            return Ok(examenDto);
        }

        [Route("GetExamensByStudentId")]
        [HttpGet]
        public async Task<IActionResult> GetExamensByStudentId(int studentId)
        {
            var student = _dsuDbService.GetCaseSStudentById(studentId);
            var examens = _examenRepository.Get().Where(x => x.DepartmentId == student.DepartmentId && x.Course == student.Course);

            List<ExamenStudentDto> examenStudentDtos = await examens.Select(x => new ExamenStudentDto
            {
                ExamenId = x.Id,
                Discipline = x.Discipline
            }).ToListAsync();
            return Ok(examenStudentDtos);
        }

        //[Route("GetExamenById")]
        //[HttpGet]
        //public IActionResult GetExamenById(int id)
        //{
        //    return Ok(_examenRepository.FindById(id));
        //}

        [Route("StartExamen")]
        [HttpGet]
        public async Task<IActionResult> StartExamen(int studentId, int examId)
        {
            var examDate = _examenRepository.FindById(examId).ExamDate;
            if (examDate != DateTime.Now.Date)
                return BadRequest($"Экзамен проводится {examDate}");
           
            await _answerBlankRepository.Create(new AnswerBlank()
            {
                StudentId = studentId,
                ExamTicketId = examId
            });
            return Ok(_examTicketRepository.Get().OrderBy(x => new Guid()).First());
        }

        //[Route("GetExamenByStudentId")]
        //[HttpGet]
        //public IActionResult GetExamenByStudentId(int studentId)
        //{
        //    var student = _dsuDbService.GetCaseSStudentById(studentId);
        //    var examens = _examenRepository.Get().Where(x => x.DepartmentId == student.DepartmentId && x.Course == student.Course && x.NGroup == student.Ngroup);
        //    return Ok(examens);
        //}

        //[Route("GetExamensByStudentId")]
        //[HttpGet]
        //public async Task<IActionResult> GetExamensByStudentId(int studentId)
        //{
        //    var examens = _examenRepository.GetExamensByStudentId(studentId);
        //    if (examens == null)
        //        return BadRequest("Не найден данный студент");
        //    return Ok(examens);
        //}

        [Route("CreateExamen")]
        [HttpPost]
        public async Task<IActionResult> CreateExamen(Examen examen)
        {
            await _examenRepository.Create(examen);
            return Ok();
        }

        [Route("UpdateExamen")]
        [HttpPut]
        public async Task<IActionResult> UpdateExamen(Examen examen)
        {
            await _examenRepository.Update(examen);
            return Ok();
        }

        [Route("DeleteExamen")]
        [HttpDelete]
        public async Task<IActionResult> DeleteExamen(int id)
        {
            await _examenRepository.Remove(id);
            return Ok();
        }
    }
}