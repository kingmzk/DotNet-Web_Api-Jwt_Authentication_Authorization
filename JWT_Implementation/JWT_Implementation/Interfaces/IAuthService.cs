using JWT_Implementation.Models;
using JWT_Implementation.Request_Models;

namespace JWT_Implementation.Interfaces
{
    public interface IAuthService
    {
        User AddUser(User user);

        string Login(LoginRequest loginRequest);

    }
}
