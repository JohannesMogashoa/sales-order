using Microsoft.AspNetCore.Identity;
using SalesOrder.Models;

namespace SalesOrder.Services
{
    public interface IUsersService
    {
        Task<List<IdentityUser>> GetUsers();
        Task<bool> CreateUserAsync(NewUserModel user);
    }
}
