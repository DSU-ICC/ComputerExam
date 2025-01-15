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
        /// Количество минут добавленных к времени прохождения
        /// </summary>
        public int? AdditionalTimeInMinutes { get; set; }
        /// <summary>
        /// Авторизован ли студент
        /// </summary>
        public bool? IsAuthorized { get; set; }
        public bool? IsDeleted { get; set; }

        public void EndAnswerBlank()
        {
            this.EndExamenDateTime = DateTime.Now;
            this.IsAuthorized = false;
        }

        public int? GetTimeToEndInSeconds()
        {
            if (this.ExamTicket != null && this.ExamTicket.Examen != null)
                return (int?)(this.CreateDateTime!.Value.AddMinutes((double)(this.ExamTicket.Examen.ExamDurationInMitutes
                    + (this.AdditionalTimeInMinutes ?? 0)))
                    - DateTime.Now).TotalSeconds;
            return null;
        }
    }
}
