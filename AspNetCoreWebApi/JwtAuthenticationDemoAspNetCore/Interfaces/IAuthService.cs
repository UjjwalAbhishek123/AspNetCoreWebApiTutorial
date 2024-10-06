using JwtAuthenticationDemoAspNetCore.Models;

namespace JwtAuthenticationDemoAspNetCore.Interfaces
{
    public interface IAuthService
    {
        //methods related to login
        User AddUser(User user);

        string Login(LoginRequest loginRequest);
    }
}
