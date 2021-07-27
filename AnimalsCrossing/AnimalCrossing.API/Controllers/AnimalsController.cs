using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.RestModels.Animals;
using AnimalCrossing.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimalCrossing.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalsController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnimalRequest request)
        {
            await _animalService.AddAsync(request, User);

            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _animalService.GetAllAsync());
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet("Populated")]
        public async Task<IActionResult> GetAllPopulated()
        {
            return Ok(await _animalService.GetAllPopulatedAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _animalService.GetById(id, User));
        }

        [HttpGet("Populated/{id}")]
        public async Task<IActionResult> GetPopulatedById(int id)
        {
            return Ok(await _animalService.GetPopulatedByIdAsync(id, User));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _animalService.Remove(id, User);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UpdateAnimalRequest request)
        {
            await _animalService.EditAsync(request, User);

            return Ok();
        }
    }
}
