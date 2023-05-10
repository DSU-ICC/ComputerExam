using DomainService.DBService;
using DomainService.Entity;
using Infrastructure.Common;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ExamTicketRepository : GenericRepository<ExamTicket>, IExamTicketRepository
    {
        public ExamTicketRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public ExamTicket GetRandomTicket(int examId)
        {
            return Get().Include(x => x.Questions).Where(x => x.ExamenId == examId).OrderBy(x => new Guid()).First();
        }

        public IQueryable<ExamTicket> GetTickets()
        {
            return Get().Where(x => x.IsDeleted == false);
        }

        public async Task DeleteTicket(int id)
        {
            try
            {
                await Remove(id);
            }
            catch (Exception)
            {
                var ticket = GetWithTracking().Include(x => x.Questions).FirstOrDefault(x => x.Id == id);
                if (ticket != null)
                {
                    ticket.IsDeleted = true;
                    ticket.Questions?.ForEach(c => c.IsDeleted = true);
                    await Update(ticket);
                }
            }
        }
    }
}
