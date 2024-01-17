using DomainService.Entity;
using DSUContextDBService.Models;

namespace DomainService.DtoModels
{
    public class StudentsDto
    {
        public int? StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patr { get; set; }
        public string? FioTeacher { get; set; }
        public AnswerBlank? AnswerBlank { get; set; }
        public CaseCFaculty? Faculty { get; set; }
    }
}
