namespace FileForgeDP.Controllers
{
    using System.Threading.Tasks;
    using FileForgeDP.Database;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/account/")]
    public class AccountController : ControllerBase
    {
        private readonly KeycloakFacade mKeycloakFacade;

        public AccountController(KeycloakFacade keycloakFacade)
        {
            mKeycloakFacade = keycloakFacade;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm]UserLoginDto userLoginDto)
        {
            return Ok();
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout(string userId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto userRegisterDto)
        {
            var result = await mKeycloakFacade.Register(userRegisterDto);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
