using AnimalCrossing.DAL.Entities;
using AnimalCrossing.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalCrossing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalsController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await _animalRepository.Add(new Animal()
            {
                Name = "Lucky",
                Age = 4

            });
            return Ok();
        }
    }
}
