using Quartz.Impl;
using Quartz;

namespace ComputerExam.Tasks
{
    public class ComputerExamSheduler
    {
        public static async Task Start(IServiceProvider serviceProvider)
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService<JobFactory>() ?? throw new InvalidOperationException();
            await scheduler.Start();

            // Создание задачи и триггера для UpdateTestingStatusJob
            IJobDetail endAnswerBlankJob = JobBuilder.Create<EndAnswerBlankJob>().Build();

            ITrigger endAnswerBlankTrigger = TriggerBuilder.Create()  // создаем триггер
                .WithIdentity("endAnswerBlankTrigger", "default")     // идентифицируем триггер с именем и группой
                .StartNow()                             // запуск сразу после начала выполнения
                .WithSimpleSchedule(x => x              // настраиваем выполнение действия
                    .WithIntervalInMinutes(1)           // через 1 минуты
                    .RepeatForever())                   // бесконечное повторение
                .Build();
            await scheduler.ScheduleJob(endAnswerBlankJob, endAnswerBlankTrigger);

        }
    }
}
