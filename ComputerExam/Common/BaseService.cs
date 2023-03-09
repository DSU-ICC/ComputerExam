using ComputerExam.Common.Interfaces;

namespace ComputerExam.Common
{
    public static class BaseService
    {
        public static void AddServiceCollection(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
