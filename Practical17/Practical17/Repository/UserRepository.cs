using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Practical17.Data;
using Practical17.Enum;
using Practical17.Interface;
using Practical17.Models;
using Practical17.ViewModel;
using System.Security.Claims;

namespace Practical17.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly StudentDbContext context;

        public UserRepository(StudentDbContext context)
        {
            this.context = context;
        }
        public Users Add(Users user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public Users GetUser(int id)
        {
            return context.Users.Find(id);
        }
        public async Task<UserLoginStatus> LoginUserAsync(LoginUser model)
        {
            if (model == null)
            {
                return UserLoginStatus.UserNull;
            }
            Users user = await context.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
            if(user == null) 
            {
                return UserLoginStatus.UserNotFound;
            }
            if(user.Password != model.Password)
            {
                return UserLoginStatus.InvalidPassword;
            }
            else
            {
                return UserLoginStatus.LoginSuccess;
            }
        }
        public List<string> GetUserRoles(int id)
        {
            var roles = context.UserRoles
                .Where(x => x.UserId == id)
                .Join(context.Roles, x => x.RoleId, x => x.Id, (user, role) => new { user.UserId, role.UserRole })
                .Select(x => x.UserRole);

            return roles.ToList();
        }
        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
