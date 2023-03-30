namespace DomainService.Entity
{
    public class ExamStudent
    {
        public int Id { get; set; }
        public int? IdStudent { get; set; }
        public int? IdExamen { get; set; }
        public List<Answer>? Answers { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime FinishDate { get; set; }
        public int? TotalScore { get; set; }
    }
}
