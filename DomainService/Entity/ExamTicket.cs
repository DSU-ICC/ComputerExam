namespace DomainService.Entity
{
    /// <summary>
    /// Экзаменационный билет
    /// </summary>
    public class ExamTicket
    {
        public int Id { get; set; }
        /// <summary>
        /// Номер билета
        /// </summary>
        public int? Number { get; set; }
        /// <summary>
        /// Id экзамена
        /// </summary>
        public int? ExamenId { get; set; }
        public Examen? Examen { get; set; }
        /// <summary>
        /// Список вопросов
        /// </summary>
        public List<Question>? Questions { get; set; }
        /// <summary>
        /// Вес для уменьшения повторений
        /// </summary>
        public int? Weigth { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
