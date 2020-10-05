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
using PokemonPals.Models.ViewModels;

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
            UserProfileViewModel model = new UserProfileViewModel();
            
            if(id == null)
            {
                ApplicationUser currentUser = await GetCurrentUserAsync();
                id = currentUser.Id;
                model.isOwnProfile = true;
            }
            else
            {
                model.isOwnProfile = false;
            }


            model.ViewedUser = await _userManager.Users
                                .Include(user => user.Game)
                                .Include(user => user.Avatar)
                                .FirstOrDefaultAsync(user => user.Id == id);

            if (model.ViewedUser == null)
            {
                return NotFound();
            }

            model.UserCollection = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.UserId == id)
                                            .OrderBy(cp => cp.PokemonId)
                                            .ToListAsync();

            foreach(CaughtPokemon PokemonInCollection in model.UserCollection)
            {
                if (!model.CaughtPokemonIDs.Contains(PokemonInCollection.PokemonId))
                {
                    model.CaughtPokemonIDs.Add(PokemonInCollection.PokemonId);
                }
            }

            model.FavoritePokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.UserId == id)
                                            .Where(cp => cp.isFavorite == true)
                                            .OrderBy(cp => cp.PokemonId)
                                            .Take(10)
                                            .ToListAsync();

            model.TradePokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.UserId == id)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .OrderBy(cp => cp.PokemonId)
                                            .Take(10)
                                            .ToListAsync();

            return View(model);
        }
    }
}
