using AnimalCrossing.API.RestModels.Animals;
using AnimalCrossing.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimalCrossing.API.Controllers
{
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
            await _animalService.Add(request);
            
            return Ok();
        }
    }
}
