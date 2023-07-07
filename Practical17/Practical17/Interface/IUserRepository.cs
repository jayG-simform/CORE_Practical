using Practical17.Enum;
using Practical17.Models;
using Practical17.ViewModel;

namespace Practical17.Interface
{
    public interface IUserRepository
    {
        Users GetUser(int id);
        Users Add(Users user);
        Task<UserLoginStatus> LoginUserAsync(LoginUser model);
        List<string> GetUserRoles(int id);
        Task<Users> GetUserByEmailAsync(string email);
    }
}
