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
        public int? IdStudent { get; set; }
        /// <summary>
        /// Id вопроса
        /// </summary>
        public int? IdQuestion { get; set; }
        /// <summary>
        /// Id бланка с ответами студента
        /// </summary>
        public int? IdAnswerBlank { get; set; }
        /// <summary>
        /// Текст ответа
        /// </summary>
        public string? TextAnswer { get; set; }
        /// <summary>
        /// Баллы за ответ
        /// </summary>
        public int? Scores { get; set; } 
        /// <summary>
        /// Время создания ответа
        /// </summary>
        public DateTime CreateAnswer { get; set; } = DateTime.Now;
        /// <summary>
        /// Время последнего обновления ответа
        /// </summary>
        public DateTime UpdateAnswer { get; set; }
    }
}
