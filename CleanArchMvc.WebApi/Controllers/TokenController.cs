using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebApi.DTOs;
using CleanArchMvc.WebApi.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        public TokenController(IAuthenticate authenticate, IConfiguration configuration)
        {
            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration;
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.Authenticate(userInfo.Email, userInfo.Password);

            if (result)
            {
                Token tokenService = new Token(_configuration);
                return tokenService.GenerateToken(userInfo.Email);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

        //[ApiExplorerSettings(IgnoreApi = true)] utilizado para ignorar a documentação do Swagger

        [HttpPost("CreateUser")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfo)
        {
            var result = await _authenticate.RegisterUser(userInfo.Email, userInfo.Password);

            if (result)
            {
                return Ok($"User {userInfo.Email} was created successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }
    }
}
