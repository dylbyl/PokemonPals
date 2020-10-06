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
        public async Task<IActionResult> UserSearch(string searchString)
        {
            List<ApplicationUser> usersInSearch = await _userManager.Users
                                                    .Include(user => user.Avatar)
                                                    .OrderBy(user => user.UserName)
                                                    .ToListAsync();

            ViewBag.SearchString = searchString;

            if(searchString != null && searchString != "")
            {
                usersInSearch = usersInSearch
                                        .Where(user => user.UserName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                        .ToList();
            }

            return View(usersInSearch);
        }

        [Authorize]
        public async Task<IActionResult> Profile(string id)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            ApplicationUser currentUser = await GetCurrentUserAsync();

            if (id == currentUser.UserName)
            {
                model.isOwnProfile = true;
            }
            else
            {
                model.isOwnProfile = false;
            }


            model.ViewedUser = await _userManager.Users
                                .Include(user => user.Game)
                                .Include(user => user.Avatar)
                                .FirstOrDefaultAsync(user => user.UserName == id);

            if (model.ViewedUser == null)
            {
                return NotFound();
            }

            List<Pokemon> AllPokemon = await _context.Pokemon.ToListAsync();
            model.AllPokemonCount = AllPokemon.Count();

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
