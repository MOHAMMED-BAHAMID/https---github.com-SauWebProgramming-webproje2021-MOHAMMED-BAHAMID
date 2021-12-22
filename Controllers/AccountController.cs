using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebProje.Models;

namespace WebProje.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;  
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)  
        {  
            _userManager = userManager;  
            _signInManager = signInManager;  
        }  

        public IActionResult Index()
        {
            return Redirect("~/Identity/Account/Manage/Index");
        }

        private async Task<List<string>> GetUserRoles(User user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        // GET: Account/Details/{id}
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            ViewBag.user = user;
            
            return View();
        }
        
        private bool AccountExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }
    }
}
