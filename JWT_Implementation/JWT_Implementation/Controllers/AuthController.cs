using JWT_Implementation.Interfaces;
using JWT_Implementation.Models;
using JWT_Implementation.Request_Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_Implementation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }

        // POST api/<AuthController>
        [HttpPost]
        public string Login([FromBody] LoginRequest loginModel)
        {
            var result = _authService.Login(loginModel);
            return result;
        }

        // PUT api/<AuthController>/5
        [HttpPost("addUser")]
        public User Put(int id, [FromBody] User value)
        {
            var user = _authService.AddUser(value);
            return user;
        }

     
    }
}
