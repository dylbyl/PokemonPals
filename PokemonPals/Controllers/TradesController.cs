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

        //An Index view for our TradeRequest resource. Will show all incoming and outgoing TradeRequests, as well as allow users to search for CaughtPokemon to create a TradeRequest for
        public async Task<IActionResult> Index(string searchString)
        {
            TradeIndexViewModel model = new TradeIndexViewModel();
            //Gets the currently logged in user
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Sets a variable in our ViewBag to the search string that was passed in as a parameter
            ViewBag.SearchString = searchString;

            //Loads the 3 most recent TradeRequests sent TO the current user
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
                                            .OrderByDescending(tr => tr.DateSent)
                                            .ToListAsync();

            //Loads the 3 most recent TradeRequests sent BY the current user
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
                                            .OrderByDescending(tr => tr.DateSent)
                                            .ToListAsync();

            //Loads all CaughtPokemon that are open for trade
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
                //If a search string parameter was passed in, we'll filter our results to only have CaughtPokemon whose nickname or species name contain that search query
                model.SearchResults = model.SearchResults
                                        .Where(cp => (cp.Nickname ?? "").Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || cp.Pokemon.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                        .ToList();
            }
            else
            {
                //If there's no search string parameter, we'll just show the 10 most recently caught Pokemon that are open for trade
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

            //Loops through the list of Pokemon the user owns, so that we can get their IDs. Pokemon whose IDs are listed here will be highlighted as "caught" when viewing search results
            foreach (CaughtPokemon caughtPokemon in listOfUserCaughtPokemon)
            {
                //If this list does not already contain the species of Pokemon we're currently looking at...
                if (!model.CurrentUserCollection.Contains(caughtPokemon.PokemonId))
                {
                    //...add it to the list of IDs
                    model.CurrentUserCollection.Add(caughtPokemon.PokemonId);
                }
            }

            //Sends our completed ViewModel to our view
            return View(model);
        }

        //A method to start a TradeRequest, called when a user first clicks a "Request Trade" button
        public async Task<IActionResult> StartRequest(int id)
        {
            //Creates a ViewModel so our data can persists when we switch to a new page
            TradeRequestViewModel model = new TradeRequestViewModel();
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Gets all the info of the Pokemon the user wishes to request
            model.DesiredPokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.User.Id != currentUser.Id)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .FirstOrDefaultAsync(cp => cp.Id == id);

            //If that Pokemon doesn't exist, return null
            if (model.DesiredPokemon == null)
            {
                return NotFound();
            }

            //Sets the ID of this Pokemon to our ViewModel
            model.DesiredPokemonId = id;

            //Checks to see if the user owns a Pokemon of the same species as the desired Pokemon
            CaughtPokemon DesiredPokemonInUserCollection = await _context.CaughtPokemon
                                                                    .Include(cp => cp.Pokemon)
                                                                    .Include(cp => cp.Gender)
                                                                    .Include(cp => cp.User)
                                                                    .Where(cp => cp.User.Id == currentUser.Id)
                                                                    .Where(cp => cp.isOwned == true)
                                                                    .Where(cp => cp.isHidden == false)
                                                                    .FirstOrDefaultAsync(cp => cp.PokemonId == model.DesiredPokemon.PokemonId);

            //If they do, we'll set a boolean to true, so that the Pokemon is highlighted as Caught
            if (DesiredPokemonInUserCollection != null)
            {
                model.isDesiredOwned = true;
            }

            //Fetches a list of all Pokemon the currently logged in user owns AND is marked for Trade. Users will be able to select a Pokemon to offer up in exchange for the Pokemon they're requesting
            model.TradeOpenPokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.User.Id == currentUser.Id)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .ToListAsync();

            //Sends our completed ViewModel to our View
            return View(model);
        }


        //A method that runs when the user decides which Pokemon they're requesting AND which Pokemon they're offering
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartRequest(TradeRequestViewModel model)
        {
            //Creates a new ViewModel for the two Pokemon...
            FinalTradeRequestViewModel passedModel = new FinalTradeRequestViewModel()
            {
                DesiredPokemonId = model.DesiredPokemonId,
                OfferedPokemonId = model.OfferedPokemonId,
                isDesiredOwned = model.isDesiredOwned
            };

            //...and sends it to our FinalizeRequest method, which will open a new page ofr the user to finalize their request
            return RedirectToAction("FinalizeRequest", passedModel);
        }


        //Runs when the user is bout to finalize their TradeRequest. Fetches both Pokemon associated, and asks the user to add a comment to their request
        public async Task<IActionResult> FinalizeRequest(FinalTradeRequestViewModel passedModel)
        {
            TradeRequestViewModel model = new TradeRequestViewModel()
            {
                DesiredPokemonId = passedModel.DesiredPokemonId,
                OfferedPokemonId = passedModel.OfferedPokemonId,
                isDesiredOwned = passedModel.isDesiredOwned
            };

            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Fetches the Pokemon the user is requesting
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

            //Fetches the Pokemon the user is offering in return
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

            //Returns these two Pokemon to our View
            return View(model);
        }

        //Runs when the user has attached a comment and confirmed that they will be finalizing their trade request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizeRequest(TradeRequestViewModel passedModel)
        {
            //Removes some unneccesary fields from our ModelState
            ModelState.Remove("DesiredPokemon");
            ModelState.Remove("DesiredPokemon.Level");
            ModelState.Remove("OfferedPokemon");
            ModelState.Remove("TradeOpenPokemon");

            //If our ModelState is valid, we'll add this TradeRequest to our database
            if (ModelState.IsValid)
            {
                TradeRequest newTradeRequest = new TradeRequest();

                //Adds all of our IDs and comments to a new TradeRequest
                newTradeRequest.DesiredPokemonId = passedModel.DesiredPokemonId;
                newTradeRequest.OfferedPokemonId = passedModel.OfferedPokemonId;
                newTradeRequest.Comment = passedModel.Comment.Replace("'", "''");
                newTradeRequest.isOpen = true;

                //Checks to see if a request already exists that holds these two exact Pokemon
                TradeRequest potentialDuplicateRequest = await _context.TradeRequest
                                                            .Where(tr => (tr.DesiredPokemonId == newTradeRequest.DesiredPokemonId && tr.OfferedPokemonId == newTradeRequest.OfferedPokemonId) || (tr.DesiredPokemonId == newTradeRequest.OfferedPokemonId && tr.OfferedPokemonId == newTradeRequest.DesiredPokemonId))
                                                            .Where(tr => tr.isOpen == true)
                                                            .FirstOrDefaultAsync();

                //If this isn't a duplicate request, add this TradeRequest to our database
                //Important to note, this MUST be done as a SQL query for now. Using ApplicationDbContext simply doesn't work for some unknown reason, so I had to do a manual workaround for this feature of the app.
                if (potentialDuplicateRequest == null)
                {
                    string sqlCommand = $"INSERT INTO TradeRequest (DesiredPokemonId, OfferedPokemonId, Comment, isOpen, DateSent, DateCompleted) VALUES ({newTradeRequest.DesiredPokemonId}, {newTradeRequest.OfferedPokemonId}, '{newTradeRequest.Comment}', 1, '{DateTime.Now}', NULL)";
                    await _context.Database.ExecuteSqlCommandAsync(sqlCommand);
                }
                
                return RedirectToAction("Index");
            }

            //If the ModelState is invalid, we'll reload the page with the original information

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
