namespace DomainService.Entity
{
    /// <summary>
    /// Бланк ответов
    /// </summary>
    public class AnswerBlank
    {
        public int Id { get; set; }
        /// <summary>
        /// Id студента
        /// </summary>
        public int StudentId { get; set; }
        /// <summary>
        /// Id экзаменационного билета
        /// </summary>
        public int ExamTicketId { get; set; }
        public ExamTicket? ExamTicket { get; set; }
        /// <summary>
        /// Суммарный балл за экзамен
        /// </summary>
        public int? TotalScore { get; set; }
        /// <summary>
        /// Список ответов на экзаменационные вопросы по билету
        /// </summary>
        public List<Answer>? Answers { get; set; }
        /// <summary>
        /// Дата создания бланка ответов
        /// </summary>
        public DateTime? CreateDateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Дата окончания экзамена
        /// </summary>
        public DateTime? EndExamenDateTime { get; set; }
        /// <summary>
        /// Время до конца экзамена в минутах
        /// </summary>
        public double? TimeToEndInMinutes { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
