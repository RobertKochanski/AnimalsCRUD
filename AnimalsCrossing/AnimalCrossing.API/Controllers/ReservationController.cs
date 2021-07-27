using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.RestModels.Reservations;
using AnimalCrossing.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimalCrossing.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationRequest request)
        {
            await _reservationService.AddAsync(request);

            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _reservationService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _reservationService.GetById(id, User));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _reservationService.Remove(id, User);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UpdateReservationRequest request)
        {
            await _reservationService.EditAsync(request, User);

            return Ok();
        }
    }
}
