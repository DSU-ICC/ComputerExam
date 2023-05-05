using DomainService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DtoModels
{
    public class AnswerBlankAndTicketDto
    {
        public AnswerBlank? AnswerBlank { get; set; }
        public ExamTicket? Ticket { get; set; }
    }
}
