﻿using DomainService.Entity;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExamStudentController : Controller
    {
        private readonly IExamStudentRepository _examStudentRepository;

        public ExamStudentController (IExamStudentRepository examStudentRepository)
        {
            _examStudentRepository = examStudentRepository;
        }


        [HttpGet]
        [Route("GetExamStudents")]
        public async Task<ActionResult<ExamStudent>> GetExamStudents()
        {
            return Ok(await _examStudentRepository.Get().ToListAsync());
        }

        [HttpGet]
        [Route("GetExamStudentById")]
        public ActionResult<ExamStudent> GetExamSudent(int id)
        {
            return Ok(_examStudentRepository.FindById(id));
        }

        [HttpPost]
        [Route("CreateExamStudent")]
        public async Task<ActionResult<ExamStudent>> CreateExamStudent(ExamStudent examStudent)
        {
            await _examStudentRepository.Create(examStudent);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateExamStudent")]
        public async Task<ActionResult<ExamStudent>> UpdateExamStudent(ExamStudent examStudent)
        {
            await _examStudentRepository.Update(examStudent);
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteExamStudent")]
        public async Task<ActionResult<ExamStudent>> DeleteExamStudent(int id)
        {
            await _examStudentRepository.Remove(id);
            return Ok();
        }
    }
}
