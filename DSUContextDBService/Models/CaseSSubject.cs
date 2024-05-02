namespace DSUContextDBService.Models
{
    public class CaseSSubject
    {
        public int SId { get; set; }
        public string? SName { get; set; }
        public int CathId { get; set; }
        public int? CycleId { get; set; }
        public bool Deleted { get; set; }
        public string? SAbr { get; set; }
    }
}
