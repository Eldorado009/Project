using AllupProjectMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AllupProjectMVC.Areas.admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DashboardController : MainController
    {
        
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddRole()
        {
            AppUser admin = await _userManager.FindByNameAsync("Admin");


            var result = await _userManager.AddToRoleAsync(admin, "Admin");

            return Ok(result);
        }
    }
}
