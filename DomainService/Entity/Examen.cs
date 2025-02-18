﻿namespace DomainService.Entity
{
    /// <summary>
    /// Экзамен
    /// </summary>
    public class Examen
    {
        public int Id { get; set; }
        /// <summary>
        /// Id создателя экзамена
        /// </summary>
        public Guid? EmployeeId { get; set; }
        /// <summary>
        /// Id кабинета в котором проходит экзамен
        /// </summary>
        public Guid? AuditoriumId { get; set; }
        /// <summary>
        /// Id филиала
        /// </summary>
        public int? FilialId { get; set; }
        /// <summary>
        /// Id направления
        /// </summary>
        public int? DepartmentId { get; set; }        
        /// <summary>
        /// Курс
        /// </summary>
        public int? Course { get; set; }
        /// <summary>
        /// Форма обучения
        /// </summary>
        public int? EdukindId { get; set; }
        /// <summary>
        /// Id дисциплины. Связь с полем S_ID из базы деканат
        /// </summary>
        public int? DisciplineId { get; set; }
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
        public DateTime? ExamDate { get; set; }
        /// <summary>
        /// Дата завершения экзамена
        /// </summary>
        public DateTime? EndExamDate { get; set; }
        /// <summary>
        /// Продолжительность экзамена в минутах 
        /// </summary>
        public int? ExamDurationInMitutes { get; set; }
        /// <summary>
        /// Преподаватель проверяющий экзамен
        /// </summary>
        public int? TeacherId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsInArchive { get; set; }
    }
}
