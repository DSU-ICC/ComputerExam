using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ComputerExam.Tasks
{
    [DisallowConcurrentExecution]
    public class EndAnswerBlankJob : IJob
    {
        private readonly IAnswerBlankRepository _answerBlankRepository;
        public EndAnswerBlankJob(IAnswerBlankRepository answerBlankRepository)
        {
            _answerBlankRepository = answerBlankRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var answerBlanks = _answerBlankRepository.Get()
                        .Include(x => x.ExamTicket).ThenInclude(x => x.Examen)
                        .Where(x => x.EndExamenDateTime == null &&
                                    x.CreateDateTime > DateTime.Now.AddDays(-1) &&
                                    x.CreateDateTime < DateTime.Now.AddMinutes(-(int)x.ExamTicket.Examen.ExamDurationInMitutes));

                if (answerBlanks != null)
                {
                    foreach (var item in answerBlanks)
                    {
                        if (item.GetTimeToEndInSeconds() < -10 * 60)
                        {
                            item.EndAnswerBlank();
                            await _answerBlankRepository.UpdateEntity(item);
                        }
                    }
                }

                LogHelper.SendInformationLog("Задание обновление тестов выполнено");
            }
            catch (Exception ex)
            {
                LogHelper.SendErrorLog(ex);
            }
        }
    }
}
