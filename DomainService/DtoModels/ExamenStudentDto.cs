using DSUContextDBService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DtoModels
{
    public class ExamenStudentDto
    {
        public int ExamenId { get; set; }
        public string Discipline { get; set; }
        public DateTime ExamDate { get; set; }
    }
}
