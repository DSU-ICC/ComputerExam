using DomainService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DtoModels
{
    public class StartExamenDto
    {
        public AnswerBlank? AnswerBlank { get; set; }
        public ExamTicket? ExamTicket { get; set; }
        public int? ExamenDuration { get; set; }
    }
}
