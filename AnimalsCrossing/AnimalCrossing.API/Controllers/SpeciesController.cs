using AnimalCrossing.Services.RestModels.Species;
using AnimalCrossing.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimalCrossing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesService _speciesService;

        public SpeciesController(ISpeciesService speciesService)
        {
            _speciesService = speciesService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeciesRequest request)
        {
            await _speciesService.AddAsync(request);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _speciesService.GetAllsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _speciesService.GetByIdAsync(id));
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UpdateSpeciesRequest request)
        {
            await _speciesService.EditAsync(request);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _speciesService.Remove(id);

            return Ok();
        }
    }
}
