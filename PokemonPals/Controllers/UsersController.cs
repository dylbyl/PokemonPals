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

        //A method for fetchning a single user, their favorite Pokemon, and the Pokemon they have up for trade. Important to note: for the method to work properly, we need this parameter to be named "id" BUT we're not actually getting the ID. We're getting a user's username, which is also unique to that user. This is done so that the URL for each profile contains the user's username, not their ID. This makes them much more readable.
        [Authorize]
        public async Task<IActionResult> Profile(string id)
        {
            UserProfileViewModel model = new UserProfileViewModel();
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Like I said above, "id" is the username provided in the URL. Here, we're checking to see if the user is viewing their own profile. If so, we're setting a boolean to true on our ViewModel
            //If a user is viewing their own profile, we want them to see an "Edit Your Profile" button. If they're looking at someone else's profile, we want them to see "Request Trade" buttons on certain Pokemon
            if (id == currentUser.UserName)
            {
                model.isOwnProfile = true;
            }
            else
            {
                model.isOwnProfile = false;
            }

            //Fetches the user with the parameter username
            model.ViewedUser = await _userManager.Users
                                .Include(user => user.Game)
                                .Include(user => user.Avatar)
                                .FirstOrDefaultAsync(user => user.UserName == id);

            //Returns a NotFound page if the user doesn't exist
            if (model.ViewedUser == null)
            {
                return NotFound();
            }

            //Gets a list of all the Pokemon the currently logged in user has caught (and have not been soft-deleted)
            List<CaughtPokemon> listOfUserCaughtPokemon = await _context.CaughtPokemon
                                                            .Where(cp => cp.UserId == currentUser.Id)
                                                            .Where(cp => cp.isOwned == true)
                                                            .Where(cp => cp.isHidden == false)
                                                            .ToListAsync();

            //Loops through the list of Pokemon the user owns, so that we can get their IDs. This list will be the record of CaughtPokemon for the VIEWING user, so that when they look at another user's profile, they can see the Pokemon they own/don't own as highlighted/un-highlighted.
            foreach (CaughtPokemon caughtPokemon in listOfUserCaughtPokemon)
            {
                //If this list does not already contain the species of Pokemon we're currently looking at...
                if (!model.CurrentUserCollection.Contains(caughtPokemon.PokemonId))
                {
                    //...add it to the list of IDs
                    model.CurrentUserCollection.Add(caughtPokemon.PokemonId);
                }
            }

            //Gets a count of every Pokemon in the database so that we can have Pokedex stats on our profiles
            List<Pokemon> AllPokemon = await _context.Pokemon.ToListAsync();
            model.AllPokemonCount = AllPokemon.Count();

            //The CaughtPokemon list we fetched above is for the user VIEWING the page. It will be used to highlight/un-highlight Pokemon the user has/hasn't caught. THIS piece of code is for fetching the CaughtPokemon of the VIEWED user. This will be used to show how many Pokemon the user has caught, giving us their Dex stats.
            model.UserCollection = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.User.UserName == id)
                                            .OrderBy(cp => cp.PokemonId)
                                            .ToListAsync();

            //Just like when looking at the example for the viewing user, we want a list of ids of all the Pokemon the viewed user has caught. This will be used for their Pokedex stats.
            foreach(CaughtPokemon PokemonInCollection in model.UserCollection)
            {
                if (!model.CaughtPokemonIDs.Contains(PokemonInCollection.PokemonId))
                {
                    model.CaughtPokemonIDs.Add(PokemonInCollection.PokemonId);
                }
            }

            //Gets a list of all the Pokemon the viewed user has Favorited
            model.FavoritePokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.User.UserName == id)
                                            .Where(cp => cp.isFavorite == true)
                                            .OrderBy(cp => cp.PokemonId)
                                            .Take(6)
                                            .ToListAsync();

            //Gets a list of all the Pokemon the viewed user has put up for Trade
            model.TradePokemon = await _context.CaughtPokemon
                                            .Include(cp => cp.Pokemon)
                                            .Include(cp => cp.Gender)
                                            .Include(cp => cp.User)
                                            .Where(cp => cp.isHidden == false)
                                            .Where(cp => cp.isOwned == true)
                                            .Where(cp => cp.User.UserName == id)
                                            .Where(cp => cp.isTradeOpen == true)
                                            .OrderBy(cp => cp.PokemonId)
                                            .Take(6)
                                            .ToListAsync();

            //Sends our completed ViewModel to our view
            return View(model);
        }

        //A method to load in information if we are viewing a full page of Pokemon a user has put up for trade
        [Authorize]
        public async Task<IActionResult> Trades(string id)
        {
            UserCollectionViewModel model = new UserCollectionViewModel();
            //Gets the current user
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Finds the user we're looking at, using their username instead of an id (see the Profile method for an explanation why)
            model.ViewedUser = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == id);

            //If we can't find the desired user, throw a NotFound page
            if(model.ViewedUser == null)
            {
                return NotFound();
            }

            //Sets a boolean to true if the user is viewing their own profile
            if(currentUser.UserName == id)
            {
                model.isOwnProfile = true;
            }

            //Fetches all the Pokemon we'll be looking at. In this case, it's Pokemon from a specific user that have been marked as "Open to Trade"
            model.ViewedCollection = await _context.CaughtPokemon
                                                    .Include(cp => cp.User)
                                                    .Include(cp => cp.Gender)
                                                    .Include(cp => cp.Pokemon)
                                                    .Where(cp => cp.User.UserName == id)
                                                    .Where(cp => cp.isHidden == false)
                                                    .Where(cp => cp.isOwned == true)
                                                    .Where(cp => cp.isTradeOpen == true)
                                                    .OrderBy(cp => cp.PokemonId)
                                                    .ToListAsync();

            //Gets a list of all the Pokemon the currently logged in user has caught (and have not been soft-deleted)
            List<CaughtPokemon> listOfUserCaughtPokemon = await _context.CaughtPokemon
                                                            .Where(cp => cp.UserId == currentUser.Id)
                                                            .Where(cp => cp.isOwned == true)
                                                            .Where(cp => cp.isHidden == false)
                                                            .ToListAsync();

            //Loops through the list of Pokemon the user owns, so that we can get their IDs. Again, this is to highlight Pokemon the user owns
            foreach (CaughtPokemon caughtPokemon in listOfUserCaughtPokemon)
            {
                //If this list does not already contain the species of Pokemon we're currently looking at...
                if (!model.CurrentUserCollection.Contains(caughtPokemon.PokemonId))
                {
                    //...add it to the list of IDs
                    model.CurrentUserCollection.Add(caughtPokemon.PokemonId);
                }
            }

            //Returns our completed ViewModel to our View
            return View(model);
        }

        //Works almost exactly like our Trade method, except this will show us a page of Pokemon a specific user has marked Favorite
        [Authorize]
        public async Task<IActionResult> Favorites(string id)
        {
            UserCollectionViewModel model = new UserCollectionViewModel();
            //Gets the current user
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Finds the user we're viewing, using a username instead of an id (for details on this, view my Profile method above)
            model.ViewedUser = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == id);

            if (model.ViewedUser == null)
            {
                return NotFound();
            }

            //Fetches all Pokemon owned by the user we're looking at, that are also marked as a favorite
            model.ViewedCollection = await _context.CaughtPokemon
                                                    .Include(cp => cp.User)
                                                    .Include(cp => cp.Gender)
                                                    .Include(cp => cp.Pokemon)
                                                    .Where(cp => cp.User.UserName == id)
                                                    .Where(cp => cp.isHidden == false)
                                                    .Where(cp => cp.isOwned == true)
                                                    .Where(cp => cp.isFavorite == true)
                                                    .OrderBy(cp => cp.PokemonId)
                                                    .ToListAsync();

            //Gets a list of all the Pokemon the currently logged in user has caught (and have not been soft-deleted)
            List<CaughtPokemon> listOfUserCaughtPokemon = await _context.CaughtPokemon
                                                            .Where(cp => cp.UserId == currentUser.Id)
                                                            .Where(cp => cp.isOwned == true)
                                                            .Where(cp => cp.isHidden == false)
                                                            .ToListAsync();

            //Loops through the list of Pokemon the user owns, so that we can get their IDs. Again, this is to highlight Pokemon the user owns
            foreach (CaughtPokemon caughtPokemon in listOfUserCaughtPokemon)
            {
                //If this list does not already contain the species of Pokemon we're currently looking at...
                if (!model.CurrentUserCollection.Contains(caughtPokemon.PokemonId))
                {
                    //...add it to the list of IDs
                    model.CurrentUserCollection.Add(caughtPokemon.PokemonId);
                }
            }

            //Sends the completed ViewModel to our View
            return View(model);
        }
    }
}
