using System;
using FileForgeDP.Database;
using Keycloak.Net;

namespace FileForgeDP
{
    public class KeycloakFacade
    {
        private readonly KeycloakClient mKeycloakClient;

        public KeycloakFacade()
        {
            mKeycloakClient = new KeycloakClient("https://localhost:8080/", "admin", "admin");
        }

        public void Login(UserLoginDto userLoginDto)
        {

        }

        public void Logout(string userId)
        {

        }

        public void Register(UserRegisterDto userRegisterDto)
        {

        }
    }
}
