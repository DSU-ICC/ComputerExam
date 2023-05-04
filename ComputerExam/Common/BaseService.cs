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
            services.AddScoped<IExamenRepository, ExamenRepository>();
            services.AddScoped<IExamTicketRepository, ExamTicketRepository>();
            services.AddScoped<IAnswerBlankRepository, AnswerBlankRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            #endregion
        }
    }
}
