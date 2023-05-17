using DomainService.DBService;
using DomainService.DtoModels;
using DomainService.Entity;
using DSUContextDBService.DBService;
using DSUContextDBService.Interface;
using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ExamenRepository : GenericRepository<Examen>, IExamenRepository
    {
        private readonly IDsuDbService _dsuDbService;
        private readonly IExamTicketRepository _examTicketRepository;
        private readonly IAnswerBlankRepository _answerBlankRepository;
        public ExamenRepository(ApplicationContext dbContext, IDsuDbService dsuDbService, IExamTicketRepository examTicketRepository, IAnswerBlankRepository answerBlankRepository) : base(dbContext)
        {
            _dsuDbService = dsuDbService;
            _examTicketRepository = examTicketRepository;
            _answerBlankRepository = answerBlankRepository;
        }

        public IQueryable<Examen> GetExamens()
        {
            return Get().Where(x => x.IsDeleted == false);
        }

        public IQueryable<ExamenDto> GetExamensByEmployeeId(Guid employeeId)
        {
            var examenDto = GetExamens().Where(x => x.EmployeeId == employeeId)
               .Select(i => new ExamenDto()
               {
                   ExamenId = i.Id,
                   Discipline = i.Discipline,
                   Group = i.NGroup,
                   Course = i.Course,
                   Department = _dsuDbService.GetCaseSDepartmentById(i.DepartmentId),
                   ExamDate = i.ExamDate,
                   ExamDurationInMitutes = i.ExamDurationInMitutes,
                   ExamTickets = _examTicketRepository.Get().Include(x => x.Questions).Where(x => x.ExamenId == i.Id).ToList(),
               });
            return examenDto;
        }

        public IQueryable<ExamenStudentDto> GetExamensByStudentId(int studentId)
        {
            var student = _dsuDbService.GetCaseSStudentById(studentId);
            var examens = GetExamens().Where(x => x.DepartmentId == student.DepartmentId && x.Course == student.Course);

            var examenStudentDtos = examens.Select(examen => new ExamenStudentDto()
            {
                ExamenId = examen.Id,
                Discipline = examen.Discipline,
                AnswerBlank = _answerBlankRepository.Get().FirstOrDefault(x => x.StudentId == studentId && x.ExamTicket.ExamenId == examen.Id),
                ExamDate = examen.ExamDate
            });
            return examenStudentDtos;
        }

        public List<StudentsDto> GetStudentsByExamenId(int examenId)
        {
            var examen = GetExamens().FirstOrDefault(x => x.Id == examenId);
            var students = _dsuDbService.GetCaseSStudents().Where(x => x.DepartmentId == examen.DepartmentId && x.Course == examen.Course && x.Ngroup == examen.NGroup);
            var answerBlanks = _answerBlankRepository.Get().Include(x => x.ExamTicket).Where(x => x.ExamTicket.ExamenId == examenId);

            List<StudentsDto> studentsDtos = new();
            foreach (var item in students)
            {
                studentsDtos.Add(new StudentsDto()
                {
                    StudentId = item.Id,
                    FirstName = item.Firstname,
                    LastName = item.Lastname,
                    Patr = item.Patr,
                    TotalScore = answerBlanks.FirstOrDefault(c => c.StudentId == item.Id)?.TotalScore
                });
            }
            return studentsDtos;
        }

        public List<ForCheckingDto> GetStudentsByExamenIdForChecking(int examenId)
        {
            var examen = GetExamens().Include(x => x.Tickets).FirstOrDefault(x => x.Id == examenId);
            if (examen == null)
                return null;
            
            var students = _dsuDbService.GetCaseSStudents().Where(x => x.DepartmentId == examen.DepartmentId && x.Course == examen.Course && x.Ngroup == examen.NGroup);
            var answerBlanks = _answerBlankRepository.Get().Include(x => x.ExamTicket).ThenInclude(x => x.Questions)
                                                           .Include(x => x.Answers).Where(x => x.ExamTicket.ExamenId == examenId);

            List<ForCheckingDto> studentsDtos = new();
            foreach (var item in students)
            {
                studentsDtos.Add(new ForCheckingDto()
                {
                    StudentId = item.Id,
                    TotalScore = answerBlanks.FirstOrDefault(c => c.StudentId == item.Id) == null ? null : answerBlanks.FirstOrDefault(c => c.StudentId == item.Id).TotalScore,
                    AnswerBlank = answerBlanks.FirstOrDefault(c => c.StudentId == item.Id),
                    Examen = examen
                });
            }
            return studentsDtos;
        }

        public async Task<StartExamenDto>? StartExamen(int studentId, int examId)
        {
            if (_answerBlankRepository.Get().Any(x => x.StudentId == studentId && x.ExamTicket.ExamenId == examId))
                return null;

            var examen = GetExamens().Include(x => x.Tickets.Where(c => c.IsDeleted == false))
                                                       .ThenInclude(x => x.Questions.Where(c => c.IsDeleted == false))
                                                       .FirstOrDefault(x => x.Id == examId);
            if (examen == null)
                return null;
            if (examen.ExamDate.Value.Date != DateTime.Now.Date)
                return null;

            var ticket = examen.Tickets?.OrderBy(x => Guid.NewGuid()).First();

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
            return startExamenDto;
        }

        public async Task<Examen> CopyExamen(int examenId, DateTime newExamDate)
        {
            var examen = GetExamens().Include(x => x.Tickets).ThenInclude(x => x.Questions).FirstOrDefault(x => x.Id == examenId);
            if (examen != null)
            {
                examen.Id = 0;
                examen.ExamDate = newExamDate;
                foreach (var item in examen.Tickets)
                {
                    item.Id = 0;
                    foreach (var question in item.Questions)
                    {
                        question.Id = 0;
                    }
                }
                await Create(examen);
            }
            return examen;
        }

        public async Task DeleteExamen(int id)
        {
            try
            {
                await Remove(id);
            }
            catch (Exception)
            {
                var examen = GetWithTracking().Include(x => x.Tickets).ThenInclude(c => c.Questions).FirstOrDefault(x => x.Id == id);
                if (examen != null)
                {
                    examen.IsDeleted = true;

                    examen.Tickets?.ForEach(x =>
                    {
                        x.IsDeleted = true;
                        x.Questions?.ForEach(c => c.IsDeleted = true);
                    });
                    await Update(examen);
                }
            }
        }
    }
}
