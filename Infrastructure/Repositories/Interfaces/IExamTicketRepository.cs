using DomainService.Entity;
using Infrastructure.Common.Interfaces;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IExamTicketRepository : IGenericRepository<ExamTicket>
    {
        public ExamTicket GetRandomTicket(int examId);
    }
}
