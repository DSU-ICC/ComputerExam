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
        private readonly IDsuDbService _dsuDbService;
        private readonly IExamenRepository _examenRepository;
        private readonly IAnswerBlankRepository _answerBlankRepository;
        private readonly IExamTicketRepository _examTicketRepository;

        public ExamenController(IDsuDbService dsuDbService, IExamenRepository examenRepository, IAnswerBlankRepository answerBlankRepository, IExamTicketRepository examTicketRepository)
        {
            _dsuDbService = dsuDbService;
            _examenRepository = examenRepository;
            _answerBlankRepository = answerBlankRepository;
            _examTicketRepository = examTicketRepository;
        }

        [Route("GetExamens")]
        [HttpGet]
        public IActionResult GetExamens()
        {
            return Ok(_examenRepository.Get());
        }

        [Route("GetExamensByEmployeeId")]
        [HttpGet]
        public IActionResult GetExamensByEmployeeId(Guid employeeId)
        {
            var examenDto = _examenRepository.Get()
               .Where(x => x.EmployeeId == employeeId)
               .Select(i => new ExamenDto()
               {
                   ExamenId = i.Id,
                   Discipline = i.Discipline,
                   Group = i.NGroup,
                   Course = (int)i.Course,
                   Department = _dsuDbService.GetCaseSDepartmentById(i.DepartmentId),
                   ExamDate = (DateTime)i.ExamDate,
                   ExamDurationInMitutes = i.ExamDurationInMitutes,
                   ExamTickets = _examTicketRepository.Get().Include(x => x.Questions).Where(x => x.ExamenId == i.Id).ToList(),
               });
            return Ok(examenDto);
        }

        [Route("GetExamensByStudentId")]
        [HttpGet]
        public IActionResult GetExamensByStudentId(int studentId)
        {
            var student = _dsuDbService.GetCaseSStudentById(studentId);
            var examens = _examenRepository.Get().Include(x => x.Tickets).Where(x => x.DepartmentId == student.DepartmentId && x.Course == student.Course);
            List<ExamenStudentDto> examenStudentDtos = new();

            foreach (var item in _answerBlankRepository.Get())
            {
                foreach (var examen in examens)
                {
                    if (examen.Tickets.Any(x => x.Id == item.Id))
                    {
                        examenStudentDtos.Add(new ExamenStudentDto
                        {
                            ExamenId = examen.Id,
                            Discipline = examen.Discipline,
                            AnswerBlank = item,
                            ExamDate = (DateTime)examen.ExamDate
                        });
                    }
                }
            }
            return Ok(examenStudentDtos);
        }

        [Route("GetStudentsByExamenId")]
        [HttpGet]
        public async Task<IActionResult> GetStudentsByExamenId(int examenId)
        {
            var examen = _examenRepository.Get().Include(x => x.Tickets).FirstOrDefault(x => x.Id == examenId);
            var students = _dsuDbService.GetCaseSStudents().Where(x => x.DepartmentId == examen.DepartmentId && x.Course == examen.Course && x.Ngroup == examen.NGroup);
            var answerBlanks = await _answerBlankRepository.Get().Include(x => x.ExamTicket).Where(x => x.ExamTicket.ExamenId == examenId).ToListAsync();

            List<StudentsDto> studentsDtos = new();

            foreach (var student in students)
            {
                studentsDtos.Add(new StudentsDto
                {
                    StudentId = student.Id,
                    FirstName = student.Firstname,
                    LastName = student.Lastname,
                    Patr = student.Patr,
                    TotalScore = answerBlanks.FirstOrDefault(c => c.StudentId == student.Id)?.TotalScore
                });
            }
            return Ok(studentsDtos);
        }

        [Route("GetStudentsByExamenIdForChecking")]
        [HttpGet]
        public async Task<IActionResult> GetStudentsByExamenIdForChecking(int examenId)
        {
            var examen = _examenRepository.Get().Include(x => x.Tickets).FirstOrDefault(x => x.Id == examenId);
            var students = _dsuDbService.GetCaseSStudents().Where(x => x.DepartmentId == examen.DepartmentId && x.Course == examen.Course && x.Ngroup == examen.NGroup);
            var answerBlanks = await _answerBlankRepository.Get().Include(x => x.ExamTicket)
                                                                 .ThenInclude(x => x.Questions)
                                                                 .Where(x => x.ExamTicket.ExamenId == examenId).ToListAsync();

            List<ForCheckingDto> studentsDtos = new();
            foreach (var student in students)
            {
                var answerBlank = answerBlanks.FirstOrDefault(c => c.StudentId == student.Id);
                studentsDtos.Add(new ForCheckingDto
                {
                    StudentId = student.Id,
                    TotalScore = answerBlank?.TotalScore,
                    AnswerBlank = answerBlank,
                    Examen = examen
                });
            }
            return Ok(studentsDtos);
        }

        [Route("StartExamen")]
        [HttpGet]
        public async Task<IActionResult> StartExamen(int studentId, int examId)
        {
            var examen = _examenRepository.Get().Include(x => x.Tickets).ThenInclude(x => x.Questions).FirstOrDefault(x => x.Id == examId);
            if (examen == null)
                return BadRequest("Экзамен не найден");

            if (examen.ExamDate.Value.Date != DateTime.Now.Date)
                return BadRequest($"Экзамен проводится {examen.ExamDate.Value.Date}");

            var ticket = examen.Tickets.OrderBy(x => new Guid()).First();

            var answerBlank = new AnswerBlank()
            {
                StudentId = studentId,
                ExamTicketId = ticket.Id
            };
            await _answerBlankRepository.Create(answerBlank);

            StartExamenDto startExamenDto = new()
            {
                AnswerBlank = answerBlank,
                ExamTicket = ticket,
                ExamenDuration = examen.ExamDurationInMitutes
            };
            return Ok(startExamenDto);
        }

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