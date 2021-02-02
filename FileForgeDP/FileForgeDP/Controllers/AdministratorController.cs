namespace FileForgeDP.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using FileForgeDP.Database.Repositories;
    using FileForgeDP.Loggers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Administrator")]
    [Route("api/admin/")]
    [ApiController]
    public class AdministratorController : ControllerBase
    {
        private readonly KeycloakFacade mKeycloakFacade;
        private readonly WorkspaceRepository mWorkspaceRepository;
        private readonly ILogger mAuditLogger;
        public AdministratorController(KeycloakFacade keycloakFacade, WorkspaceRepository workspaceRepository, ILogger auditLogger)
        {
            mKeycloakFacade = keycloakFacade;
            mWorkspaceRepository = workspaceRepository;
            mAuditLogger = auditLogger;
        }

        [HttpGet]
        [Route("synchronizeGroups")]
        public async Task<IActionResult> SynchronizeGroups()
        {
            var keycloakGroups = await mKeycloakFacade.GetAvailableGroups();

            if (keycloakGroups.Count() > 0)
            {
                var (addedGroups, deletedGroups) = mWorkspaceRepository.SynchronizeWorkspaces(keycloakGroups.Select(x => x.Name));

                foreach(var group in addedGroups)
                {
                    mAuditLogger.Debug("admin admin", ActionEnum.CREATE, ControllerContext.ActionDescriptor.ActionName, HttpStatusCode.OK, group);
                }

                foreach (var group in deletedGroups)
                {
                    mAuditLogger.Debug("admin admin", ActionEnum.DELETE, ControllerContext.ActionDescriptor.ActionName, HttpStatusCode.OK, group);
                }
            }

            return Ok();
        }
    }
}
