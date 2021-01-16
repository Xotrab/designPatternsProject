namespace FileForgeDP.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using FileForgeDP.Database.Repositories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Administrator")]
    [Route("api/admin/")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly KeycloakFacade mKeycloakFacade;
        private readonly WorkspaceRepository mWorkspaceRepository;

        public AdministratorController(KeycloakFacade keycloakFacade, WorkspaceRepository workspaceRepository)
        {
            mKeycloakFacade = keycloakFacade;
            mWorkspaceRepository = workspaceRepository;
        }

        [HttpGet]
        [Route("synchronizeGroups")]
        public async Task<IActionResult> SynchronizeGroups()
        {
            var keycloakGroups = await mKeycloakFacade.GetAvailableGroups();

            if (keycloakGroups.Count() > 0)
            {
                mWorkspaceRepository.SynchronizeWorkspaces(keycloakGroups.Select(x => x.Name));
            }

            return Ok();
        }
    }
}
