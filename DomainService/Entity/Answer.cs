namespace DomainService.Entity
{
    /// <summary>
    /// Ответ
    /// </summary>
    public class Answer
    {
        public int Id { get; set; }
        /// <summary>
        /// Id студента
        /// </summary>
        public int? StudentId { get; set; }
        /// <summary>
        /// Id вопроса
        /// </summary>
        public int? QuestionId { get; set; }
        /// <summary>
        /// Id бланка с ответами студента
        /// </summary>
        public int? AnswerBlankId { get; set; }
        /// <summary>
        /// Текст ответа
        /// </summary>
        public string? TextAnswer { get; set; }
        /// <summary>
        /// Время создания ответа
        /// </summary>
        public DateTime? CreateAnswerDate { get; set; } = DateTime.Now;
        /// <summary>
        /// Время последнего обновления ответа
        /// </summary>
        public DateTime? UpdateAnswerDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
