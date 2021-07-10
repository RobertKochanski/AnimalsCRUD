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
    }
}
