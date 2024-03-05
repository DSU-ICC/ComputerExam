using ComputerExam.Services.Interfaces;
using DSUContextDBService.Interface;
using Infrastructure.Repositories.Interfaces;
using ClosedXML.Excel;

namespace ComputerExam.Services
{
    public class GeneratedExcelFile : IGeneratedExcelFile
    {
        private readonly IExamenRepository _examenRepository;
        private readonly IDsuDbService _dsuDbService;
        private string Path { get; set; }
        public GeneratedExcelFile(IExamenRepository examenRepository, IDsuDbService dsuDbService, IConfiguration configuration)
        {
            _examenRepository = examenRepository;
            _dsuDbService = dsuDbService;

            Path = configuration["FileFolder"];
        }

        public string GenerateExcelFile(int examenId)
        {
            var fileName = examenId.ToString() + DateTime.Now.ToString("dd-MM-yyyy") + "_" + DateTime.Now.ToString("ss-mm-HH") + ".xlsx";

            var examen = _examenRepository.FindById(examenId);
            var studentForStatisticsDtos = _examenRepository.GetStatisticForReport(examenId);

            var workbook = new XLWorkbook();
            var workSheet = workbook.Worksheets.Add("Лист1");

            workSheet.Cell(1, "A").Value = "Учебный год";

            var lastYear = examen.ExamDate.Value.AddYears(-1);

            workSheet.Cell(2, "A").Value = lastYear.ToString("yyyy") + "-" + examen.ExamDate.Value.ToString("yyyy");

            if (studentForStatisticsDtos.Any(x => x.SessId % 2 == 0))
                workSheet.Cell(3, "A").Value = "Летняя сессия";
            else
                workSheet.Cell(3, "A").Value = "Зимняя сессия";
            
            AddBorder(workSheet.Range("A1:A3"));

            workSheet.Cell(1, "C").Value = "МИНИСТЕРСТВО НАУКИ И ВЫСШЕГО ОБРАЗОВАНИЯ";
            workSheet.Cell(2, "C").Value = "Дагестанский государственный университет";

            var department = _dsuDbService.GetCaseSDepartmentById((int)examen.DepartmentId);
            workSheet.Cell(5, "A").Value = "Факультет/институт: "; workSheet.Cell(5, "B").Value = _dsuDbService.GetFacultyById(department.FacId).FacName;
             workSheet.Cell(6, "A").Value = "Направление/специальность: "; workSheet.Cell(6, "B").Value = department.DeptName;
            workSheet.Cell(7, "A").Value = "Курс: "; workSheet.Cell(7, "B").Value = examen.Course;
            workSheet.Cell(8, "A").Value = "Группа: "; workSheet.Cell(8, "B").Value = examen.NGroup;
            workSheet.Cell(9, "A").Value = "Дисциплина: "; workSheet.Cell(9, "B").Value = examen.Discipline;
                        
            AddBorder(workSheet.Range("A5:B9"));

            workSheet.Cell(12, "A").Value = "№";
            workSheet.Cell(12, "B").Value = "ФИО ";
            workSheet.Cell(12, "C").Value = "Средний балл успеваемости (из ИС деканат)";
            workSheet.Cell(12, "D").Value = "Балл, полученный на комп экзамене";
            
            for (int i = 0; i < studentForStatisticsDtos.Count; i++)
            {
                workSheet.Cell(i + 13, "A").Value = i + 1;
                workSheet.Cell(i + 13, "B").Value = studentForStatisticsDtos[i].LastName + " " + studentForStatisticsDtos[i].FirstName + " " + studentForStatisticsDtos[i].Patr;
                workSheet.Cell(i + 13, "C").Value = studentForStatisticsDtos[i].AverageAcademicScore;
                workSheet.Cell(i + 13, "D").Value = studentForStatisticsDtos[i].ExamenScore;
            }

            //// создание сетки в диапазоне            
            AddBorder(workSheet.Range("A12:D" + (studentForStatisticsDtos.Count + 12).ToString()));

            workSheet.Columns().AdjustToContents(); //ширина столбца по содержимому

            // вернем пользователю файл без сохранения его на сервере
            workbook.SaveAs(Path + fileName);
            return fileName;
        }

        private void AddBorder(IXLRange table)
        {
            table.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            table.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            table.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            table.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        }
    }
}
