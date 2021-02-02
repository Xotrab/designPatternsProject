namespace FileForgeDP.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using FileForgeDP.Database;
    using FileForgeDP.Loggers;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/account/")]
    public class AccountController : ControllerBase
    {
        private readonly KeycloakFacade mKeycloakFacade;
        private readonly ILogger mAuditLogger;
        public AccountController(KeycloakFacade keycloakFacade, ILogger auditLogger)
        {
            mKeycloakFacade = keycloakFacade;
            mAuditLogger = auditLogger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto userRegisterDto)
        {
            var result = await mKeycloakFacade.Register(userRegisterDto);

            if (result)
            {
                mAuditLogger.Debug($"{userRegisterDto.FirstName} {userRegisterDto.LastName}", ActionEnum.REGISTER, ControllerContext.ActionDescriptor.ActionName, HttpStatusCode.OK, userRegisterDto.Username);
                return Ok();
            }

            return BadRequest();
        }
    }
}
