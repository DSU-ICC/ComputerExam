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
            try
            {
                await _examTicketRepository.Remove(id);
            }
            catch (Exception)
            {
                var ticket = _examTicketRepository.Get().Include(x => x.Questions).FirstOrDefault(x => x.Id == id);
                if (ticket == null)
                    return BadRequest("Билет не найден");

                ticket.IsDeleted = true;
                ticket.Questions?.ForEach(c => c.IsDeleted = true);
                await _examTicketRepository.Update(ticket);
                throw;
            }
            return Ok();
        }
    }
}
