using ComputerExam.Services;
using ComputerExam.Services.Interfaces;
using DSUContextDBService.Interface;
using DSUContextDBService.Services;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Hosting.Internal;

namespace ComputerExam.Common
{
    public static class BaseService
    {
        public static void AddServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IDsuDbService, DsuDbService>();
            services.AddSingleton<AuthOptions>();
            services.AddSingleton<IHostEnvironment>(new HostingEnvironment());

            services.AddScoped<IGeneratedExcelFile, GeneratedExcelFile>();
            #region Repositories            
            services.AddScoped<IExamenRepository, ExamenRepository>();
            services.AddScoped<IExamTicketRepository, ExamTicketRepository>();
            services.AddScoped<IAnswerBlankRepository, AnswerBlankRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            #endregion
        }
    }
}
