using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonPals.Data;
using PokemonPals.Models;

namespace PokemonPals.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        //A method for fetching the currently logged in user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Profile(string id)
        {
            ApplicationUser viewUser = new ApplicationUser();
            
            if(id == null)
            {
                ApplicationUser currentUser = await GetCurrentUserAsync();
                id = currentUser.Id;
                ViewBag.isOwnProfile = true;
            }
            else
            {
                ViewBag.isOwnProfile = false;
            }


            viewUser = await _userManager.Users
                                .Include(user => user.Game)
                                .Include(user => user.Avatar)
                                .FirstOrDefaultAsync(user => user.Id == id);

            if (viewUser == null)
            {
                return NotFound();
            }

            return View(viewUser);
        }
    }
}
