using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonPals.Data;
using PokemonPals.Models;
using PokemonPals.Models.ViewModels;

namespace PokemonPals.Controllers
{
    public class TradesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public TradesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        //A method for fetching the currently logged in user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> Requests(string searchString)
        {
            TradeRequestViewModel model = new TradeRequestViewModel();
            ApplicationUser currentUser = await GetCurrentUserAsync();

            ViewBag.SearchString = searchString;

            model.SearchResults = await _context.CaughtPokemon
                                            .Include(cp => cp.User)
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.UserId != currentUser.Id)
                                            .OrderByDescending(cp => cp.DateCaught)
                                            .ToListAsync();

            if (searchString != null && searchString != "")
            {
                model.SearchResults = model.SearchResults
                                        .Where(cp => (cp.Nickname ?? "").Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || cp.Pokemon.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                        .ToList();
            }
            else
            {
                model.SearchResults = model.SearchResults
                                        .Take(10)
                                        .ToList();
            }

            return View(model);
        }
    }
}
