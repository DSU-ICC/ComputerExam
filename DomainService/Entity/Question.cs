namespace DomainService.Entity
{
    /// <summary>
    /// Вопрос
    /// </summary>
    public class Question
    {
        public int Id { get; set; }
        /// <summary>
        /// Id билета
        /// </summary>
        public int ExamTicketId { get; set; }
        /// <summary>
        /// Билет
        /// </summary>
        public ExamTicket? Ticket { get; set; }
        /// <summary>
        /// Номер вопроса
        /// </summary>
        public int? Number { get; set; }
        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string? Text { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
