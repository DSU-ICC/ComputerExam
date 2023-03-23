using ComputerExam.Common.Interfaces;
using ComputerExam.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamStudentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExamStudentController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Route("GetExamStudents")]
        public async Task<ActionResult<ExamStudent>> GetExamStudents()
        {
            return Ok(await _unitOfWork.ExamenRepository.Get().ToListAsync());
        }

        [HttpGet]
        [Route("GetExamStudentById")]
        public ActionResult<ExamStudent> GetExamSudent(int id)
        {
            return Ok(_unitOfWork.ExamStudentRepository.FindById(id));
        }

        [HttpPost]
        [Route("CreateExamStudent")]
        public async Task<ActionResult<ExamStudent>> CreateExamStudent(ExamStudent examStudent)
        {
            await _unitOfWork.ExamStudentRepository.Create(examStudent);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateExamStudent")]
        public async Task<ActionResult<ExamStudent>> UpdateExamStudent(ExamStudent examStudent)
        {
            await _unitOfWork.ExamStudentRepository.Update(examStudent);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteExamStudent")]
        public async Task<ActionResult<ExamStudent>> DeleteExamStudent(int id)
        {
            await _unitOfWork.ExamStudentRepository.Remove(id);
            return Ok();
        }
    }
}
