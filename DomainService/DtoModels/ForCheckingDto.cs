using DomainService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DtoModels
{
    public class ForCheckingDto
    {
        public int? StudentId { get; set; }
        public AnswerBlank? AnswerBlank { get; set; }
    }
}
