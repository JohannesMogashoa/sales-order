using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesOrder.Data;
using SalesOrder.Models;

namespace SalesOrder.Services.Implementation
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UsersService> _logger;
        private readonly IConfiguration _configuration;

        public UsersService(UserManager<IdentityUser> userManager, ApplicationDbContext context, ILogger<UsersService> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<bool> CreateUserAsync(NewUserModel user)
        {
            var newUser = new IdentityUser
            {
                UserName = user.Email,
                Email = user.Email,
            };

            var result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                return true;
            } else
            {
                return false;
            }
        }

        public async Task<List<IdentityUser>> GetUsers()
        {
            List<IdentityUser> users = await _context.Users.ToListAsync();

            return users;
        }
    }
}
