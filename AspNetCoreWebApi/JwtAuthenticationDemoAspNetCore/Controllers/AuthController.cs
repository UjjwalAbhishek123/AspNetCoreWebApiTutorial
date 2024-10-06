using JwtAuthenticationDemoAspNetCore.Interfaces;
using JwtAuthenticationDemoAspNetCore.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtAuthenticationDemoAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //priavte variable for authService
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/<AuthController>
        [HttpPost]
        public string Login([FromBody] LoginRequest loginRequestModel)
        {
            var result = _authService.Login(loginRequestModel);
            return result;
        }

        // PUT api/<AuthController>/5
        [HttpPost("addUser")]
        public User AddUser([FromBody] User value)
        {
            var user = _authService.AddUser(value);
            return user;
        }
    }
}
