using ComputerExam.Common.Interfaces;
using ComputerExam.DBService;
using ComputerExam.Repositories.Interfaces;
using ComputerExam.Repositories;
using DSUContextDBService.Interface;
using DSUContextDBService.DbServices;

namespace ComputerExam.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly DSUContext _dSUContext;

        public UnitOfWork(ApplicationContext context, DSUContext dSUContext)
        {
            _context = context;
            _dSUContext = dSUContext;
        }

        public IAnswerRepository AnswerRepository
        {
            get
            {
                IAnswerRepository answerRepository = new AnswerRepository(_context);
                return answerRepository;
            }
        }

        public IQuestionRepository QuestionRepository
        {
            get
            {
                IQuestionRepository questionRepository = new QuestionRepository(_context);
                return questionRepository;
            }
        }

        public IExamenRepository ExamenRepository
        {
            get
            {
                IExamenRepository examenRepository = new ExamenRepository(_context);
                return examenRepository;
            }
        }

        public IExamStudentRepository ExamStudentRepository
        {
            get
            {
                IExamStudentRepository examStudentRepository = new ExamStudentRepository(_context);
                return examStudentRepository;
            }
        }

        public IDsuDbService DsuDbService
        {
            get
            {
                IDsuDbService dsuDbService = new DsuDbService(_dSUContext);
                return dsuDbService;
            }
        }
    }
}
