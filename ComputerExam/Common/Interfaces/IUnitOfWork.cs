using ComputerExam.Repositories.Interfaces;
using DSUContextDBService.Interface;

namespace ComputerExam.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IExamenRepository ExamenRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IQuestionRepository QuestionRepository { get; }        
        IExamStudentRepository ExamStudentRepository { get; }
        IDsuDbService DsuDbService { get; }
    }
}
