namespace DomainService.DtoModels
{
    public class StudentForStatisticsDto
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? Patr { get; set; }
        public double? AverageAcademicScore { get; set; }
        public int? ExamenScore { get; set; }
        public int? SessId { get; set; }
    }
}
