using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FileForgeDP.Database.Repositories;
using FileForgeDP.Loggers;
using Microsoft.AspNetCore.SignalR;

namespace FileForgeDP
{
    public class AdminNotifyHub : Hub<IAdminHubClient>
    {
        private readonly KeycloakFacade mKeycloakFacade;
        private readonly WorkspaceRepository mWorkspaceRepository;
        private readonly ILogger mAuditLogger;

        public AdminNotifyHub(KeycloakFacade keycloakFacade, WorkspaceRepository workspaceRepository, ILogger auditLogger)
        {
            mKeycloakFacade = keycloakFacade;
            mWorkspaceRepository = workspaceRepository;
            mAuditLogger = auditLogger;
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var keycloakGroups = await mKeycloakFacade.GetAvailableGroups();

            if (keycloakGroups.Count() > 0)
            {
                var (addedGroups, deletedGroups) = mWorkspaceRepository.SynchronizeWorkspaces(keycloakGroups.Select(x => x.Name));

                foreach (var group in addedGroups)
                {
                    mAuditLogger.Debug("admin admin", ActionEnum.CREATE, "Synchronize Workspaces", HttpStatusCode.OK, group);
                }

                foreach (var group in deletedGroups)
                {
                    mAuditLogger.Debug("admin admin", ActionEnum.DELETE, "Synchronize Workspaces", HttpStatusCode.OK, group);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
