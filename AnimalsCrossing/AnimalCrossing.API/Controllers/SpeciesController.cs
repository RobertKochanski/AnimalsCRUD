using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.RestModels.Species;
using AnimalCrossing.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimalCrossing.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesService _speciesService;

        public SpeciesController(ISpeciesService speciesService)
        {
            _speciesService = speciesService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSpeciesRequest request)
        {
            await _speciesService.AddAsync(request);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _speciesService.GetAllsync());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _speciesService.GetByIdAsync(id));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UpdateSpeciesRequest request)
        {
            await _speciesService.EditAsync(request);

            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _speciesService.Remove(id);

            return Ok();
        }
    }
}
