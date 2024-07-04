using Jwt_Authentication_Authorization.Models;

namespace Jwt_Authentication_Authorization.Interfaces
{
    public interface IAuthService
    {
        User AddUser(User user);

        string Login(LoginRequest loginRequest);

        Role AddRole(Role role);

        bool AssignRoleToUser(AddUserRole obj);
    }
}
