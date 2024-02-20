using ComputerExam.Services.Interfaces;
using DSUContextDBService.Interface;
using Infrastructure.Repositories.Interfaces;
using Excel = Microsoft.Office.Interop.Excel;

namespace ComputerExam.Services
{
    public class GeneratedExcelFile : IGeneratedExcelFile
    {
        private readonly IExamenRepository _examenRepository;
        private readonly IDsuDbService _dsuDbService;
        private readonly IWebHostEnvironment _appEnvironment;
        private string Path { get; set; }
        private string ExcelDefaultSavePath { get; set; }
        public GeneratedExcelFile(IWebHostEnvironment appEnvironment, IExamenRepository examenRepository, IDsuDbService dsuDbService, IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _examenRepository = examenRepository;
            _dsuDbService = dsuDbService;
            _appEnvironment = appEnvironment;

            Path = hostEnvironment.ContentRootPath + configuration["FileFolder"];
            ExcelDefaultSavePath = hostEnvironment.ContentRootPath + configuration["ExcelDefaultSavePath"];
        }

        public string GenerateExcelFile(int examenId)
        {
            var examen = _examenRepository.FindById(examenId);
            var studentForStatisticsDtos = _examenRepository.GetStatisticForReport(examenId);
            var fileName = examenId.ToString() + DateTime.Now.ToString("dd-MM-yyyy") + "_" + DateTime.Now.ToString("ss-mm-HH") + ".xlsx";
            
            Excel.Application ObjWorkExcel = new(); //открыть эксель
            var workBooks = ObjWorkExcel.Workbooks;
            var workBook = workBooks.Add();
            var workSheet = (Excel.Worksheet)ObjWorkExcel.ActiveSheet;

            workSheet.Cells[1, "A"] = "Учебный год";

            var lastYear = examen.ExamDate.Value.AddYears(-1);

            workSheet.Cells[2, "A"] = lastYear.ToString("yyyy") + "-" + examen.ExamDate.Value.ToString("yyyy");

            if (studentForStatisticsDtos.Any(x => x.SessId % 2 == 0))
                workSheet.Cells[3, "A"] = "Летняя сессия";
            else
                workSheet.Cells[3, "A"] = "Зимняя сессия";

            workSheet.Cells[1, "C"] = "МИНИСТЕРСТВО НАУКИ И ВЫСШЕГО ОБРАЗОВАНИЯ";
            workSheet.Cells[2, "C"] = "Дагестанский государственный университет";

            var department = _dsuDbService.GetCaseSDepartmentById((int)examen.DepartmentId);
            workSheet.Cells[5, "A"] = "Факультет/институт: "; workSheet.Cells[5, "C"] = _dsuDbService.GetFacultyById(department.FacId).FacName;
            Excel.Range _excelCells1 = workSheet.get_Range("A5", "B5").Cells;
            _excelCells1.Merge(Type.Missing);
            workSheet.Cells[6, "A"] = "Направление/специальность: "; workSheet.Cells[6, "C"] = department.DeptName;
            Excel.Range _excelCells2 = workSheet.get_Range("A6", "B6").Cells;
            _excelCells2.Merge(Type.Missing);
            workSheet.Cells[7, "A"] = "Курс: "; workSheet.Cells[7, "C"] = examen.Course;
            Excel.Range _excelCells3 = workSheet.get_Range("A7", "B7").Cells;
            _excelCells3.Merge(Type.Missing);
            workSheet.Cells[8, "A"] = "Группа: "; workSheet.Cells[8, "C"] = examen.NGroup;
            Excel.Range _excelCells4 = workSheet.get_Range("A8", "B8").Cells;
            _excelCells4.Merge(Type.Missing);
            workSheet.Cells[9, "A"] = "Дисциплина: "; workSheet.Cells[9, "C"] = examen.Discipline;
            Excel.Range _excelCells5 = workSheet.get_Range("A9", "B9").Cells;
            _excelCells5.Merge(Type.Missing);

            for (int i = 0; i < studentForStatisticsDtos.Count; i++)
            {
                workSheet.Cells[11, "A"] = "№";
                workSheet.Cells[11, "B"] = "ФИО ";
                workSheet.Cells[11, "C"] = "Средний балл успеваемости (из ИС деканат)";
                workSheet.Cells[11, "D"] = "Балл, полученный на комп экзамене";

                workSheet.Cells[i + 11, "A"] = i;
                workSheet.Cells[i + 11, "B"] = studentForStatisticsDtos[i].LastName + " " + studentForStatisticsDtos[i].FirstName + " " + studentForStatisticsDtos[i].Patr;
                workSheet.Cells[i + 11, "C"] = studentForStatisticsDtos[i].AverageAcademicScore;
                workSheet.Cells[i + 11, "D"] = studentForStatisticsDtos[i].ExamenScore;
            }

            workBook.SaveAs(fileName, Type.Missing,
              Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange,
              Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            ObjWorkExcel.Quit(); // выйти из экселя
            GC.Collect(); // убрать за собой
            GC.WaitForPendingFinalizers(); // Подождать окончания выполняемых операций

            File.Move(ExcelDefaultSavePath + fileName, _appEnvironment.ContentRootPath + Path + fileName, true);
            return Path + fileName;
        }
    }
}
