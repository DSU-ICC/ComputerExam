namespace DomainService.Entity
{
    /// <summary>
    /// Экзамен
    /// </summary>
    public class Examen
    {
        public int Id { get; set; }
        /// <summary>
        /// Id преподавателя создавшего экзамен
        /// </summary>
        public int? IdTeacher { get; set; }
        /// <summary>
        /// Id направления
        /// </summary>
        public int? DepartmentId { get; set; }
        /// <summary>
        /// Курс
        /// </summary>
        public int? Course { get; set; }
        /// <summary>
        /// Номер группы
        /// </summary>
        public string? NGroup { get; set; }
        /// <summary>
        /// Название дисциплины
        /// </summary>
        public string? Discipline { get; set; }
        /// <summary>
        /// Список билетов
        /// </summary>
        public List<ExamTicket>? Tickets { get; set; }
        /// <summary>
        /// Дата экзамена
        /// </summary>
        public DateOnly? ExamDate { get; set; }
        /// <summary>
        /// Продолжительность экзамена в минутах 
        /// </summary>
        public int? ExamDurationInMitutes { get; set; }
    }
}
