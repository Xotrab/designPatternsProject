using System.Collections.Generic;
using System.Threading.Tasks;
using FileForgeDP.Database;
using Keycloak.Net;
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

        public void Login(UserLoginDto userLoginDto)
        {

        }

        public void Logout(string userId)
        {

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
    }
}
