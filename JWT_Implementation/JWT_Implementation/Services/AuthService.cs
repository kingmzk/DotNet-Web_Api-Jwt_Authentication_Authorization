using JWT_Implementation.Context;
using JWT_Implementation.Interfaces;
using JWT_Implementation.Models;
using JWT_Implementation.Request_Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWT_Implementation.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtContext _jwtService;
        private readonly IConfiguration _configuration;

        public AuthService(JwtContext jwtContext,IConfiguration configuration)
        {
            this._jwtService = jwtContext;
            this._configuration = configuration;
        }

        public User AddUser(User user)
        {
           var addUser = _jwtService.Users.Add(user);
           _jwtService.SaveChanges();
           return addUser.Entity;
        }

        public string Login(LoginRequest loginRequest)
        {
           if(loginRequest.UserName != null && loginRequest.Password != null)
           {
                var user = _jwtService.Users.FirstOrDefault(x => x.Email == loginRequest.UserName && x.Password == loginRequest.Password);
                if(user != null)
                {
                    var claims = new[]
{
                       new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                       new Claim("Id", user.Id.ToString()),
                       new Claim("UserName", user.Name),
                       new Claim("Email", user.Email),
                 
                   };
                    var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return tokenString;
                }
                else
                {
                    throw new Exception("User Not Found");
                }
           }
           else
           {
               throw new Exception("Please enter valid username and password");
           }
        }
    }
}
