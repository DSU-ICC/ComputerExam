using DomainService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DtoModels
{
    public class AnswerBlankDto
    {
        public AnswerBlank? AnswerBlank { get; set; }
        public int? TimeToEndInSeconds { get; set; }
    }
}
