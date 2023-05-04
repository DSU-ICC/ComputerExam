using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.DtoModels
{
    public class StudentsDto
    {
        public int? StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patr { get; set; }
        public int? TotalScore { get; set; }
    }
}
