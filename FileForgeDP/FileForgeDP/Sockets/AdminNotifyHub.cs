using System;
using System.Linq;
using System.Threading.Tasks;
using FileForgeDP.Database.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace FileForgeDP
{
    public class AdminNotifyHub : Hub<IAdminHubClient>
    {
        public readonly KeycloakFacade mKeycloakFacade;
        public readonly WorkspaceRepository mWorkspaceRepository;

        public AdminNotifyHub(KeycloakFacade keycloakFacade, WorkspaceRepository workspaceRepository)
        {
            mKeycloakFacade = keycloakFacade;
            mWorkspaceRepository = workspaceRepository;
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
                mWorkspaceRepository.SynchronizeWorkspaces(keycloakGroups.Select(x => x.Name));
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
