using Quartz.Spi;
using Quartz;

namespace ComputerExam.Tasks
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _provider;

        public JobFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return new JobWrapper(_provider, bundle.JobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();
        }
    }

    public class JobWrapper : IJob, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        private readonly IJob _job;

        public JobWrapper(IServiceProvider serviceProvider, Type jobType)
        {
            _serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _job = ActivatorUtilities.CreateInstance(_serviceScope.ServiceProvider, jobType) as IJob;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return _job.Execute(context);
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}
