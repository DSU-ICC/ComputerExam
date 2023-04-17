using DomainService.Entity;
using DSUContextDBService.Interface;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace ComputerExam.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : Controller
    {
        private readonly IExamenRepository _examenRepository;
        private readonly IExamTicketRepository _examTicketRepository;
        private readonly IDsuDbService _dsuDbService;

        public TicketController(IExamenRepository examenRepository, IExamTicketRepository examTicketRepository, IDsuDbService dsuDbService)
        {
            _examenRepository = examenRepository;
            _examTicketRepository = examTicketRepository;
            _dsuDbService = dsuDbService;
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
            await _examTicketRepository.Remove(id);
            return Ok();
        }
    }
}
