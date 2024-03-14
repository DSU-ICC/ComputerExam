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
            return Get().Where(x => x.IsDeleted != true && x.IsInArchive != true);
        }

        public IQueryable<Examen> GetExamensWithFilter(int? departmentId = null, int? course = null, string? ngroup = null)
        {
            var examens = Get();
            if (departmentId != null)
                examens = examens.Where(x => x.DepartmentId == departmentId);

            if (course != null)
                examens = examens.Where(x => x.Course == course);

            if (ngroup != null)
                examens = examens.Where(x => x.NGroup == ngroup);

            return examens;
        }

        public List<StudentForStatisticsDto> GetStatisticForReport(int examenId)
        {
            Examen examen = FindById(examenId);
            var caseUkoModules = _dsuDbService.GetCaseUkoModules().Where(x => x.DeptId == examen.DepartmentId &&
                                                                              x.Ngroup == examen.NGroup && 
                                                                              x.EdukindId == examen.EdukindId).AsEnumerable();
            List<StudentForStatisticsDto> studentForStatisticsDtos = new();
            var modulesByDiscipline = caseUkoModules.Where(x => examen.ExamDate - x.Veddate < TimeSpan.FromDays(180) && examen.ExamDate > x.Veddate && examen.Discipline == x.Predmet)
                                              .OrderBy(x => x.Lastname)
                                              .ThenBy(x => x.Firstname)
                                              .ThenBy(x => x.Patr)
                                              .GroupBy(x => x.Id).Select(x => new
                                              {
                                                  Id = x.Key,
                                                  SessId = x.Max(c => c.SessId),
                                                  Average = x.Average(c => c.Rb)
                                              }).ToList();
            foreach (var item in modulesByDiscipline)
            {
                var student = caseUkoModules.FirstOrDefault(x => x.Id == item.Id);
                var answerBlank = _answerBlankRepository.GetAnswerBlankByStudentIdAndExamenId(item.Id, examenId);
                studentForStatisticsDtos.Add(new StudentForStatisticsDto()
                {
                    FirstName = student.Firstname,
                    LastName = student.Lastname,
                    Patr = student.Patr,
                    SessId = item.SessId,
                    AverageAcademicScore = Math.Round(item.Average, 2),
                    ExamenScore = answerBlank?.TotalScore
                });
            };
            return studentForStatisticsDtos;
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
                   Department = _dsuDbService.GetCaseSDepartmentById((int)i.DepartmentId!),
                   Edukind = _dsuDbService.GetEdukindById((int)i.EdukindId!),
                   ExamDate = i.ExamDate,
                   ExamDurationInMitutes = i.ExamDurationInMitutes,
                   ExamTickets = _examTicketRepository.GetTickets().Include(x => x.Questions).Where(x => x.ExamenId == i.Id).ToList(),
                   EndExamDate = i.EndExamDate,
                   AuditoriumId = i.AuditoriumId,
                   TeacherId = i.TeacherId
               });
            return examenDto;
        }

        public IQueryable<ExamenDto> GetExamensByAuditoriumId(Guid auditoriumId)
        {
            var examenDto = GetExamens().Where(x => x.AuditoriumId == auditoriumId)
               .Select(i => new ExamenDto()
               {
                   ExamenId = i.Id,
                   Discipline = i.Discipline,
                   Group = i.NGroup,
                   Course = i.Course,
                   Department = _dsuDbService.GetCaseSDepartmentById((int)i.DepartmentId!),
                   ExamDate = i.ExamDate,
                   ExamDurationInMitutes = i.ExamDurationInMitutes,
                   ExamTickets = _examTicketRepository.GetTickets().Include(x => x.Questions).Where(x => x.ExamenId == i.Id).ToList(),
                   EndExamDate = i.EndExamDate,
                   AuditoriumId = i.AuditoriumId,
                   TeacherId = i.TeacherId
               });
            return examenDto;
        }

        public List<ExamenStudentDto> GetExamensByStudentId(int studentId)
        {
            var student = _dsuDbService.GetCaseSStudentById(studentId)
                ?? throw new Exception("Student not found. " + studentId.ToString());
            var examens = GetExamens().Where(x => x.DepartmentId == student.DepartmentId && 
                                                  x.Course == student.Course && 
                                                  x.NGroup == student.Ngroup && 
                                                  x.EdukindId == student.EdukindId);

            List<ExamenStudentDto> examenStudentDtos = new();

            foreach (var examen in examens)
            {
                examenStudentDtos.Add(new ExamenStudentDto()
                {
                    ExamenId = examen.Id,
                    Discipline = examen.Discipline,
                    AnswerBlank = _answerBlankRepository.GetAnswerBlanks().FirstOrDefault(x => x.StudentId == studentId && x.ExamTicket.ExamenId == examen.Id),
                    ExamDate = examen.ExamDate,
                    IsActiveNow = examen.EndExamDate == null && (DateTime.Now - examen.ExamDate) > TimeSpan.FromMinutes(0)
                });
            }
            return examenStudentDtos;
        }

        public List<StudentsDto> GetStudentsByExamenId(int examenId)
        {
            var examen = GetExamens().FirstOrDefault(x => x.Id == examenId)
                ?? throw new Exception("Exam not found.");
            var students = _dsuDbService.GetCaseSStudents().Where(x => x.DepartmentId == examen.DepartmentId && 
                                                                       x.Course == examen.Course && 
                                                                       x.Ngroup == examen.NGroup && 
                                                                       x.EdukindId == examen.EdukindId);
            var answerBlanks = _answerBlankRepository.GetAnswerBlanks().Include(x => x.ExamTicket).ThenInclude(x => x.Questions)
                                                           .Include(x => x.ExamTicket).ThenInclude(x => x.Examen)
                                                           .Where(x => x.ExamTicket.ExamenId == examenId);

            List<StudentsDto> studentsDtos = new();
            foreach (var item in students)
            {
                studentsDtos.Add(new StudentsDto()
                {
                    StudentId = item.Id,
                    FirstName = item.Firstname,
                    LastName = item.Lastname,
                    Patr = item.Patr,
                    FioTeacher = _dsuDbService.GetFioCaseSTeacherById(examen.TeacherId),
                    AnswerBlank = answerBlanks.FirstOrDefault(c => c.StudentId == item.Id),
                    Faculty = _dsuDbService.GetFacultyById(_dsuDbService.GetCaseSDepartmentById((int)examen.DepartmentId).FacId)
                });
            }
            return studentsDtos;
        }

        public List<ForCheckingDto>? GetStudentsByExamenIdForChecking(int examenId)
        {
            var examen = GetExamens().Include(x => x.Tickets).FirstOrDefault(x => x.Id == examenId);
            if (examen == null)
                return null;

            var students = _dsuDbService.GetCaseSStudents().Where(x => x.DepartmentId == examen.DepartmentId && 
                                                                       x.Course == examen.Course && 
                                                                       x.Ngroup == examen.NGroup &&
                                                                       x.EdukindId == examen.EdukindId);
            var answerBlanks = _answerBlankRepository.GetAnswerBlanks().Include(x => x.ExamTicket).ThenInclude(x => x.Questions)
                                                           .Include(x => x.ExamTicket).ThenInclude(x => x.Examen)
                                                           .Where(x => x.ExamTicket.ExamenId == examenId);

            List<ForCheckingDto> studentsDtos = new();
            foreach (var item in students)
            {
                studentsDtos.Add(new ForCheckingDto()
                {
                    StudentId = item.Id,
                    AnswerBlank = answerBlanks.FirstOrDefault(c => c.StudentId == item.Id),
                    FioTeacher = _dsuDbService.GetFioCaseSTeacherById(examen.TeacherId)
                });
            }
            return studentsDtos;
        }

        public async Task<AnswerBlank?> StartExamen(int studentId, int examId)
        {
            var answerBlank = _answerBlankRepository.GetAnswerBlanks().Include(x => x.ExamTicket).ThenInclude(x => x.Questions)
                                                                      .FirstOrDefault(x => x.StudentId == studentId && x.ExamTicket.ExamenId == examId);
            if (answerBlank != null)
            {
                if (answerBlank.EndExamenDateTime == null && answerBlank.IsAuthorized != true)
                {
                    answerBlank.IsAuthorized = true;
                    return answerBlank;
                }
                return null;
            }

            var examen = GetExamens().Include(x => x.Tickets.Where(c => c.IsDeleted == false))
                                     .ThenInclude(x => x.Questions.Where(c => c.IsDeleted == false))
                                     .FirstOrDefault(x => x.Id == examId);
            if (examen == null)
                return null;
            if (examen.ExamDate?.Date != DateTime.Now.Date)
                return null;

            foreach (var item in examen.Tickets)
                item.Weight ??= 0;

            var ticket = examen.Tickets?.OrderBy(x => Guid.NewGuid()).OrderBy(x => x.Weight).First();

            answerBlank = new AnswerBlank()
            {
                StudentId = studentId,
                ExamTicketId = ticket.Id,
                IsAuthorized = true
            };
            await _answerBlankRepository.Create(answerBlank);
            answerBlank.ExamTicket = ticket;
            ticket.Weight++;
            await _examTicketRepository.Update(ticket);
            return answerBlank;
        }

        public async Task<Examen?> CopyExamen(int examenId, DateTime newExamDate)
        {
            var examen = GetExamens().Include(x => x.Tickets).ThenInclude(x => x.Questions).FirstOrDefault(x => x.Id == examenId);
            if (examen != null)
            {
                examen.Id = 0;
                examen.ExamDate = newExamDate.AddHours(3);
                examen.Discipline += " (Пересдача)";
                examen.EndExamDate = null;
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
            var examen = GetWithTracking().Include(x => x.Tickets).ThenInclude(c => c.Questions).FirstOrDefault(x => x.Id == id);
            if (examen != null)
            {
                if (examen.Tickets.Any())
                {
                    examen.IsDeleted = true;

                    examen.Tickets?.ForEach(x =>
                    {
                        x.IsDeleted = true;
                        x.Questions?.ForEach(c => c.IsDeleted = true);
                    });
                    await Update(examen);
                }
                else
                {
                    await Remove(id);
                }
            }
            else
                throw new Exception();
        }
    }
}