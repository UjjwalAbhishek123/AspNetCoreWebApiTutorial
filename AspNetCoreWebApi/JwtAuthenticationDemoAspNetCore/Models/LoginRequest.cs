namespace JwtAuthenticationDemoAspNetCore.Models
{
    public class LoginRequest
    {
        //It will handle username and password after login request
        public string Username { get; set; }
        public string Passsword { get; set; }
    }
}
