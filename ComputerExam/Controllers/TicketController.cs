using DomainService.Entity;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly IExamTicketRepository _examTicketRepository;

        public TicketController(IExamTicketRepository examTicketRepository)
        {
            _examTicketRepository = examTicketRepository;
        }

        [Route("GetTicketsByExamenId")]
        [HttpGet]
        public async Task<IActionResult> GetTicketsByExamenId(int examenId)
            {
            var tickets = _examTicketRepository.GetTickets().Include(x=>x.Questions).Where(x => x.ExamenId == examenId);
            return Ok(tickets);
        }

        [Route("CreateTicket")]
        [HttpPost]
        public async Task<IActionResult> CreateTicket(ExamTicket examen)
        {
            await _examTicketRepository.Create(examen);
            return Ok();
        }

        [Route("UpdateTicket")]
        [HttpPut]
        public async Task<IActionResult> UpdateExamen(ExamTicket examen)
        {
            await _examTicketRepository.Update(examen);
            return Ok();
        }

        [Route("DeleteTicket")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _examTicketRepository.DeleteTicket(id);
            return Ok();
        }
    }
}
