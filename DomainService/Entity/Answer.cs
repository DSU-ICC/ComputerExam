namespace DomainService.Entity
{
    public class Answer
    {
        public int Id { get; set; }
        public int? IdStudent { get; set; }
        public int? IdQuestion { get; set; }
        public string? TextAnswer { get; set; }
        public int? Scores { get; set; } 
    }
}
