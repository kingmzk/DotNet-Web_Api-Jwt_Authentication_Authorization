using Jwt_Authentication_Authorization.Context;
using Jwt_Authentication_Authorization.Interfaces;
using Jwt_Authentication_Authorization.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Jwt_Authentication_Authorization.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(JwtContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }

        public Role AddRole(Role role)
        {
            var addedRole = _context.Roles.Add(role);
            _context.SaveChanges();
            return addedRole.Entity;
        }

        public User AddUser(User user)
        {
            var addUser = _context.Users.Add(user);
            _context.SaveChanges();
            return addUser.Entity;
        }

        public bool AssignRoleToUser(AddUserRole obj)
        {
            try
            {
                var addRoles = new List<UserRole>();
                var user = _context.Users.FirstOrDefault(x => x.Id == obj.UserId); // _context.Users.Find(obj.UserId); 
                if (user == null)
                {
                    throw new Exception(" User is not Valid "); //return false;
                }

                foreach (int role in obj.RoleIds)
                {
                    var userRole = new UserRole();

                    userRole.RoleId = role;
                    userRole.UserId = obj.UserId;
                   addRoles.Add(userRole);

                }
                _context.UserRoles.AddRange(addRoles);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
               // throw new Exception(ex.Message);
            }
        
        }

        public string Login(LoginRequest loginRequest)
        {
           if(loginRequest.Username != null && loginRequest.Password != null)
            {
                var user = _context.Users.SingleOrDefault(x => x.UserName == loginRequest.Username && x.Password == loginRequest.Password);
                if(user != null)
                {
                    var claims = new List<Claim> { 
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.UserName),
                    };

                    var userRoles =  _context.UserRoles.Where(x => x.UserId == user.Id).ToList();
                    var roleIds =  userRoles.Select(s => s.RoleId).ToList();
                    var roles = _context.Roles.Where(x => roleIds.Contains(x.Id)).ToList();

                    foreach(var role in roles)
                    {
                        // claims.Add(new Claim("Role", role.Name));
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }

                    var Key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                   throw new Exception("User not found");
                }
            }
            else
            {
                throw new Exception("UserName or Password did not match");
            }
        }
    }
}
