using Practical_19.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practical_19.Db.Interface
{
    public interface IUserRepository
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginModel model);
        Task<UserManagerResponse> LogoutUserAsync(LogoutModel model);
        Task<IEnumerable<RegisterUser>> GetUsers();
    }
}
