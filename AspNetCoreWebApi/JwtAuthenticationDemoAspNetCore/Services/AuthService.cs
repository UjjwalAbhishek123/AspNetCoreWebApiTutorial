using JwtAuthenticationDemoAspNetCore.Context;
using JwtAuthenticationDemoAspNetCore.Interfaces;
using JwtAuthenticationDemoAspNetCore.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationDemoAspNetCore.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtDbContext _jwtContext;
        private readonly IConfiguration _configuration;

        public AuthService(JwtDbContext jwtContext, IConfiguration configuration)
        {
            _jwtContext = jwtContext;
            _configuration = configuration;
        }

        public User AddUser(User user)
        {
            //functionality to add user
            //adding user in Users Table
            var addeduser = _jwtContext.Users.Add(user);

            _jwtContext.SaveChanges();

            return addeduser.Entity;
        }

        public string Login(LoginRequest loginRequest)
        {
            //check if login request having username and password is not null
            if(loginRequest.Username!=null && loginRequest.Passsword!=null)
            {
                var user = _jwtContext.Users.SingleOrDefault(s => s.Email == loginRequest.Username && s.Password == loginRequest.Passsword);

                if (user != null)
                {
                    //defining claims
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.Name),
                        new Claim("Email", user.Email)
                    };

                    //creating SymmetricSecurityKey
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    //creating Signing credentials
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //creating JwtSecurityToken
                    var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn
                    );

                    // Use JwtSecurityTokenHandler to convert the JWT token into a string format.
                    // The 'token' variable is a previously created JwtSecurityToken object.
                    // The WriteToken method serializes the token so it can be sent to the client.
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    throw new Exception("User is not valid");
                }
            }
            else
            {
                throw new Exception("Credentials are not valid");
            }
        }
    }
}
