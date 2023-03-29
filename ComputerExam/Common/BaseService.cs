using DSUContextDBService.Interface;
using DSUContextDBService.Services;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;

namespace ComputerExam.Common
{
    public static class BaseService
    {
        public static void AddServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IDsuDbService, DsuDbService>();

            #region Repositories
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IExamenRepository, ExamenRepository>();
            services.AddScoped<IExamStudentRepository, ExamStudentRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            #endregion
        }
    }
}
