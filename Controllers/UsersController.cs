using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.Models;
using SalesOrder.Services;

namespace SalesOrder.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            List<IdentityUser> users = await _usersService.GetUsers();

            return View(users);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(NewUserModel model)
        {
            if(ModelState.IsValid)
            {
                bool isCreated = await _usersService.CreateUserAsync(model);
                if (isCreated)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
