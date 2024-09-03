using ApiDevBP.Database;
using ApiDevBP.Entities;
using ApiDevBP.Models;
using ApiDevBP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SQLite;
using System.Reflection;

namespace ApiDevBP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        // Endpoint para guardar un nuevo usuario
        [HttpPost("create")]
        public async Task<IActionResult> SaveUser([FromBody] UserModel user)
        {
            var result = _userService.SaveUser(new UserEntity()
            {
                Name = user.Name,
                Lastname = user.Lastname
            });
            return Ok(result > 0);
        }

        // Endpoint para obtener todos los usuarios con ID
        [HttpGet("list")]
        public async Task<IActionResult> GetUsers()
        {
            var users = _userService.GetUsers();
            if (users != null && users.Any())
            {
                var userModels = users.Select(x => new UserModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Lastname = x.Lastname
                });
                return Ok(userModels);
            }
            return NotFound();
        }

        // Endpoint para eliminar un usuario por ID
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);
            if (result > 0)
            {
                return Ok();
            }
            return NotFound();
        }

        // Endpoint para actualizar un usuario por ID
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserModel user)
        {
            var userToUpdate = new UserEntity()
            {
                Id = id,
                Name = user.Name,
                Lastname = user.Lastname
            };

            var result = _userService.UpdateUser(userToUpdate);
            if (result > 0)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
