using DomainService.Entity;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var tickets = await _examTicketRepository.GetTickets().Include(x=>x.Questions).Where(x => x.ExamenId == examenId).ToListAsync();
            return Ok(tickets);
        }

        [Authorize]
        [Route("CreateTicket")]
        [HttpPost]
        public async Task<IActionResult> CreateTicket(ExamTicket examen)
        {
            await _examTicketRepository.Create(examen);
            return Ok();
        }

        [Authorize]
        [Route("UpdateTicket")]
        [HttpPost]
        public async Task<IActionResult> UpdateExamen(ExamTicket examen)
        {
            await _examTicketRepository.Update(examen);
            return Ok();
        }

        [Authorize]
        [Route("DeleteTicket")]
        [HttpPost]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            await _examTicketRepository.DeleteTicket(id);
            return Ok();
        }
    }
}
