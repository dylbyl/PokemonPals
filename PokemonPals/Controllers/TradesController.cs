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

        public async Task<IActionResult> Index(string searchString)
        {
            TradeIndexViewModel model = new TradeIndexViewModel();
            ApplicationUser currentUser = await GetCurrentUserAsync();

            ViewBag.SearchString = searchString;

            model.IncomingRequests = await _context.TradeRequest
                                            .Include(tr => tr.DesiredPokemon)
                                                .ThenInclude(tr => tr.User)
                                            .Include(tr => tr.DesiredPokemon)
                                                .ThenInclude(tr => tr.Gender)
                                            .Include(tr => tr.DesiredPokemon)
                                                .ThenInclude(tr => tr.Pokemon)
                                            .Include(tr => tr.OfferedPokemon)
                                                .ThenInclude(tr => tr.User)
                                            .Include(tr => tr.OfferedPokemon)
                                                .ThenInclude(tr => tr.Gender)
                                            .Include(tr => tr.OfferedPokemon)
                                                .ThenInclude(tr => tr.Pokemon)
                                            .Take(3)
                                            .Where(tr => tr.DesiredPokemon.UserId == currentUser.Id)
                                            .ToListAsync();

            model.OutgoingRequests = await _context.TradeRequest
                                            .Include(tr => tr.DesiredPokemon)
                                                .ThenInclude(tr => tr.User)
                                            .Include(tr => tr.DesiredPokemon)
                                                .ThenInclude(tr => tr.Gender)
                                            .Include(tr => tr.DesiredPokemon)
                                                .ThenInclude(tr => tr.Pokemon)
                                            .Include(tr => tr.OfferedPokemon)
                                                .ThenInclude(tr => tr.User)
                                            .Include(tr => tr.OfferedPokemon)
                                                .ThenInclude(tr => tr.Gender)
                                            .Include(tr => tr.OfferedPokemon)
                                                .ThenInclude(tr => tr.Pokemon)
                                            .Take(3)
                                            .Where(tr => tr.OfferedPokemon.UserId == currentUser.Id)
                                            .ToListAsync();

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

            //Gets a list of all the Pokemon the current user has caught (and have not been soft-deleted)
            List<CaughtPokemon> listOfUserCaughtPokemon = await _context.CaughtPokemon
                                                            .Where(cp => cp.UserId == currentUser.Id)
                                                            .Where(cp => cp.isOwned == true)
                                                            .Where(cp => cp.isHidden == false)
                                                            .ToListAsync();

            //Loops through the list of Pokemon the user owns, so that we can get their IDs
            foreach (CaughtPokemon caughtPokemon in listOfUserCaughtPokemon)
            {
                //If this list does not already contain the species of Pokemon we're currently looking at...
                if (!model.CurrentUserCollection.Contains(caughtPokemon.PokemonId))
                {
                    //...add it to the list of IDs
                    model.CurrentUserCollection.Add(caughtPokemon.PokemonId);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> StartRequest(int id)
        {
            TradeRequestViewModel model = new TradeRequestViewModel();
            ApplicationUser currentUser = await GetCurrentUserAsync();

            model.DesiredPokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.User.Id != currentUser.Id)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .FirstOrDefaultAsync(cp => cp.Id == id);

            if (model.DesiredPokemon == null)
            {
                return NotFound();
            }

            model.DesiredPokemonId = id;

            CaughtPokemon DesiredPokemonInUserCollection = await _context.CaughtPokemon
                                                                    .Include(cp => cp.Pokemon)
                                                                    .Include(cp => cp.Gender)
                                                                    .Include(cp => cp.User)
                                                                    .Where(cp => cp.User.Id == currentUser.Id)
                                                                    .Where(cp => cp.isOwned == true)
                                                                    .Where(cp => cp.isHidden == false)
                                                                    .FirstOrDefaultAsync(cp => cp.PokemonId == model.DesiredPokemon.PokemonId);

            if (DesiredPokemonInUserCollection != null)
            {
                model.isDesiredOwned = true;
            }

            model.TradeOpenPokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.User.Id == currentUser.Id)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .ToListAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartRequest(TradeRequestViewModel model)
        {
            FinalTradeRequestViewModel passedModel = new FinalTradeRequestViewModel()
            {
                DesiredPokemonId = model.DesiredPokemonId,
                OfferedPokemonId = model.OfferedPokemonId,
                isDesiredOwned = model.isDesiredOwned
            };

            return RedirectToAction("FinalizeRequest", passedModel);
        }

        public async Task<IActionResult> FinalizeRequest(FinalTradeRequestViewModel passedModel)
        {
            TradeRequestViewModel model = new TradeRequestViewModel()
            {
                DesiredPokemonId = passedModel.DesiredPokemonId,
                OfferedPokemonId = passedModel.OfferedPokemonId,
                isDesiredOwned = passedModel.isDesiredOwned
            };

            ApplicationUser currentUser = await GetCurrentUserAsync();



            model.DesiredPokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.User.Id != currentUser.Id)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .FirstOrDefaultAsync(cp => cp.Id == passedModel.DesiredPokemonId);

            if (model.DesiredPokemon == null)
            {
                return NotFound();
            }

            model.OfferedPokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.User.Id == currentUser.Id)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .FirstOrDefaultAsync(cp => cp.Id == passedModel.OfferedPokemonId);

            if (model.OfferedPokemon == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizeRequest(TradeRequestViewModel passedModel)
        {
            ModelState.Remove("DesiredPokemon");
            ModelState.Remove("DesiredPokemon.Level");
            ModelState.Remove("OfferedPokemon");
            ModelState.Remove("TradeOpenPokemon");

            if (ModelState.IsValid)
            {
                TradeRequest newTradeRequest = new TradeRequest();

                newTradeRequest.DesiredPokemonId = passedModel.DesiredPokemonId;
                newTradeRequest.OfferedPokemonId = passedModel.OfferedPokemonId;
                newTradeRequest.Comment = passedModel.Comment;

                _context.Database.ExecuteSqlCommand(
        $"INSERT INTO TradeRequest (DesiredPokemonId, OfferedPokemonId, Comment) VALUES ({passedModel.DesiredPokemonId}, {passedModel.OfferedPokemonId}, {passedModel.Comment})");

                return RedirectToAction("Index");
            }

            TradeRequestViewModel failedModel = new TradeRequestViewModel()
            {
                DesiredPokemonId = passedModel.DesiredPokemonId,
                OfferedPokemonId = passedModel.OfferedPokemonId,
                isDesiredOwned = passedModel.isDesiredOwned
            };

            ApplicationUser currentUser = await GetCurrentUserAsync();



            failedModel.DesiredPokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.User.Id != currentUser.Id)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .FirstOrDefaultAsync(cp => cp.Id == passedModel.DesiredPokemonId);

            if (failedModel.DesiredPokemon == null)
            {
                return NotFound();
            }

            failedModel.OfferedPokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.User.Id == currentUser.Id)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .FirstOrDefaultAsync(cp => cp.Id == passedModel.OfferedPokemonId);

            if (failedModel.OfferedPokemon == null)
            {
                return NotFound();
            }

            return View(failedModel);
        }
    }
}
