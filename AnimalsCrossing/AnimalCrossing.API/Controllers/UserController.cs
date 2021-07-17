using AnimalCrossing.DAL.Entities;
using AnimalCrossing.Services.RestModels.Users;
using AnimalCrossing.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AnimalCrossing.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var loggedUser = await _userService.Authenticate(model.Username, model.Password);

            return Ok(loggedUser);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            await _userService.AddAsync(request);

            return Ok();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id, User);

            return Ok(user);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            var user = await _userService.GetMyInfo(User);

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] UpdateUserRequest request)
        {
            await _userService.EditAsync(request, User);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            await _userService.Remove(id);

            return Ok();
        }
    }
}
