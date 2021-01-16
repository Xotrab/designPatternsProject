using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileForgeDP.Database;
using Keycloak.Net;
using Keycloak.Net.Models.Groups;
using Keycloak.Net.Models.Users;

namespace FileForgeDP
{
    public class KeycloakFacade
    {
        private readonly KeycloakClient mKeycloakClient;

        private readonly string mRealmName = "master";

        public KeycloakFacade()
        {
            mKeycloakClient = new KeycloakClient("http://localhost:8080/", "admin", "admin");
        }

        public async Task<bool> Register(UserRegisterDto userRegisterDto)
        {
            var result = await mKeycloakClient.CreateUserAsync(mRealmName, new User
            {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                UserName = userRegisterDto.Username,
                Email = userRegisterDto.Email,
                Credentials = new List<Credentials>
                {
                    new Credentials
                    {
                        Type = "password",
                        Value = userRegisterDto.Password,
                        Temporary = false
                    }
                }
            });

            return result;
        }

        public async Task<string> GetUserIdByName(string name)
        {
            var users = await mKeycloakClient.GetUsersAsync(mRealmName);

            return null; // users.FirstOrDefault(x => x.0)
        }

        public async Task<IEnumerable<Group>> GetAvailableGroups()
        {
            var result = await mKeycloakClient.GetGroupHierarchyAsync(mRealmName);

            return result;
        }

        public async Task<IEnumerable<Group>> GetUserGroups(string userId)
        {
            var result = await mKeycloakClient.GetUserGroupsAsync(mRealmName, userId);

            return result;
        }
    }
}
