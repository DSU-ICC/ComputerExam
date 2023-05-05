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
    }
}
