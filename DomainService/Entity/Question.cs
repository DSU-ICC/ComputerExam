namespace DomainService.Entity
{
    public class Question
    {
        public int Id { get; set; }
        public int IdExamen { get; set; }
        public int? Number { get; set; }
        public string? Text { get; set; }
    }
}
