using DSUContextDBService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DtoModels
{
    public class ExamenDto
    {
        public int ExamenId { get; set; }
        public string Discipline { get; set; }
        public CaseSDepartment Department { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }
    }
}
