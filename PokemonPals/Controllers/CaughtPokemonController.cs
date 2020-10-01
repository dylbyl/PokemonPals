using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokemonPals.Data;
using PokemonPals.Models;
using PokemonPals.Models.ViewModels;

namespace PokemonPals.Controllers
{
    public class CaughtPokemonController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public CaughtPokemonController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;

            _userManager = userManager;
        }


        //A method for fetching the currently logged in user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: CaughtPokemon
        //A method to show the user's entire collection of caught Pokemon. Accepts two parameters: a string declaring the order the collection should be sorted in, and a string that can be used to search the colleciton.
        [Authorize]
        public async Task<IActionResult> Collection(string sortOrder, string searchString)
        {
            //Gets the current user
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Fetches all Pokemon that belong to the current user, as long as they haven't been soft-deleted with isHidden
            List<CaughtPokemon> FullUserCollection = await _context.CaughtPokemon
                                                            .Include(cp => cp.Pokemon)
                                                            .Include(cp => cp.Gender)
                                                            .Where(cp => cp.UserId == currentUser.Id)
                                                            .Where(cp => cp.isHidden == false)
                                                            .OrderBy(cp => cp.PokemonId)
                                                            .ToListAsync();

            //Runs if a search term has been entered on the Collection view
            if (!String.IsNullOrEmpty(searchString))
            {
                //Checks the caught Pokemon's nickname, species name, and both types for the search string. This is a case insensitive search. Two of the properties (Nickname and Type 2) are nullable, and therfore require a null-conditional operator (ie cp.Nickname ?? ""). In this expression, if the property is NOT null, the property is used. If the property is null, and empty string is used instead.
                FullUserCollection = FullUserCollection.Where(
                    cp => (cp.Nickname ?? "").Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                       || cp.Pokemon.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) || cp.Pokemon.Type1.Equals(searchString, StringComparison.OrdinalIgnoreCase) || (cp.Pokemon.Type2 ?? "").Equals(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            //These lines check to see if a parameter was passed to this method to determine if the collection is sorted. If so, the data stored in the ViewBag is switched to the inverse sortOrder string, so that when the user clicks the sort link a second time, the sort order will be reversed.
            ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewBag.FavoriteSortParm = sortOrder == "favorite_true" ? "favorite_false" : "favorite_true";
            ViewBag.TradeSortParm = sortOrder == "trade_true" ? "trade_false" : "trade_true";
            ViewBag.LevelSortParm = sortOrder == "level_desc" ? "level_asc" : "level_desc";
            ViewBag.CPSortParm = sortOrder == "CP_desc" ? "CP_asc" : "CP_desc";
            ViewBag.NicknameSortParm = sortOrder == "nickname_true" ? "nickname_false" : "nickname_true";
            ViewBag.CommentSortParm = sortOrder == "comment_true" ? "comment_false" : "comment_true";

            ViewBag.SortOrderString = sortOrder;
            ViewBag.SearchString = searchString;

            //A switch-case that checks the sortOrder string, and properly sorts the user's Pokemon collection accordingly
            switch (sortOrder)
            {
                case "number_desc":
                    FullUserCollection = FullUserCollection.OrderByDescending(cp => cp.Pokemon.PokedexNumber).ToList();
                    break;
                case "favorite_true":
                    FullUserCollection = FullUserCollection.Where(cp => cp.isFavorite).ToList();
                    break;
                case "favorite_false":
                    FullUserCollection = FullUserCollection.Where(cp => !cp.isFavorite).ToList();
                    break;
                case "trade_true":
                    FullUserCollection = FullUserCollection.Where(cp => cp.isTradeOpen).ToList();
                    break;
                case "trade_false":
                    FullUserCollection = FullUserCollection.Where(cp => !cp.isTradeOpen).ToList();
                    break;
                case "level_desc":
                    FullUserCollection = FullUserCollection.OrderByDescending(cp => cp.Level).ToList();
                    break;
                case "level_asc":
                    FullUserCollection = FullUserCollection.OrderBy(cp => cp.Level).ToList();
                    break;
                case "CP_desc":
                    FullUserCollection = FullUserCollection.OrderByDescending(cp => cp.CP).ToList();
                    break;
                case "CP_asc":
                    FullUserCollection = FullUserCollection.OrderBy(cp => cp.CP).ToList();
                    break;
                case "nickname_true":
                    FullUserCollection = FullUserCollection.Where(cp => cp.Nickname != null && cp.Nickname != "").ToList();
                    break;
                case "nickname_false":
                    FullUserCollection = FullUserCollection.Where(cp => cp.Nickname == null || cp.Nickname == "").ToList();
                    break;
                case "comment_true":
                    FullUserCollection = FullUserCollection.Where(cp => cp.Comment != null && cp.Comment != "").ToList();
                    break;
                case "comment_false":
                    FullUserCollection = FullUserCollection.Where(cp => cp.Comment == null || cp.Comment == "").ToList();
                    break;
                default:
                    FullUserCollection = FullUserCollection.OrderBy(cp => cp.Pokemon.PokedexNumber).ToList();
                    break;
            }

            //Returns the fully sorted collection to the view
            return View(FullUserCollection);
        }

        // GET: CaughtPokemon/Create
        //A method to "catch" a single Pokemon and add it to the user's collection
        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            //Creates a view model so we can store multiple resources and pass them to our view
            CaughtPokemonCreateViewModel model = new CaughtPokemonCreateViewModel();

            //Fetches the Pokemon that the user has selected to catch
            model.SelectedPokemon = await _context.Pokemon
                                    .Where(p => p.Id == id)
                                    .FirstOrDefaultAsync();

            //A blank CaughtPokemon resource was created with our ViewModel. This resource needs a PokemonId so that it can be properly created when the user clicks Submit
            model.PokemonToAdd.PokemonId = id;

            //Fetches our three gender selections from the database - Male, Female, and Genderless
            List<Gender> gendersToSelectFrom = await _context.Gender.ToListAsync();

            //Turns those fetched Genders into SelectListItems, so we can use them in a dropdown
            model.Genders = new SelectList(gendersToSelectFrom, "Id", "Name").ToList();

            //Passes our completed ViewModel to the View
            return View(model);
        }

        // POST: CaughtPokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Takes all the data the user has entered, and uses it to create a CaughtPokemon resource
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaughtPokemonCreateViewModel model)
        {
            //The use of a ViewModel means that there are certain data values that are left null when this method is called. We still want to check if our ModelState is (mostly) valid, but these null fields will throw off our logic. So, let's remove them from the ModelState. I promise, they're not useful. A lot come from SelectedPokemon (the pre-seeded Pokemon resource in our database), while two are related to User data (which hasn't been added yet)
            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("SelectedPokemon.Name");
            ModelState.Remove("SelectedPokemon.Type1");
            ModelState.Remove("SelectedPokemon.RBSpriteURL");
            ModelState.Remove("SelectedPokemon.OfficialArtURL");
            ModelState.Remove("SelectedPokemon.DefaultSpriteURL");

            //Runs if our ModelState is valid and has no inappropriate null values
            if (ModelState.IsValid)
            {
                //Gets the current user and sets the new resource's user ID to match
                ApplicationUser currentUser = await GetCurrentUserAsync();
                model.PokemonToAdd.UserId = currentUser.Id;


                //Adds the CaughtPokemon resource (which is on our ViewModel) to the database. Then sends us to the Dex view.
                _context.Add(model.PokemonToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dex", "Pokemon");
            }

            //If our CaughtPokemon is not successfully added to the database, we're going to fetch a ViewModel just like in our GET Create method, but call it failedModel instead

            CaughtPokemonCreateViewModel failedModel = new CaughtPokemonCreateViewModel();
            failedModel.SelectedPokemon = await _context.Pokemon
                                    .Where(p => p.Id == model.PokemonToAdd.PokemonId)
                                    .FirstOrDefaultAsync();

            List<Gender> gendersToSelectFrom = await _context.Gender.ToListAsync();

            failedModel.Genders = new SelectList(gendersToSelectFrom, "Id", "Name").ToList();

            //Refreshes the Create view with this failedModel
            return View(failedModel);
        }

        // GET: CaughtPokemon/Edit/5
        //A method to let users Edit any Pokemon in their collection
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            //First off, if this method is called with no ID, return a NotFound page
            if (id == null)
            {
                return NotFound();
            }

            //Gets the current user and creates a ViewModel
            ApplicationUser currentUser = await GetCurrentUserAsync();
            CaughtPokemonEditViewModel model = new CaughtPokemonEditViewModel();

            //Add to our ViewModel: we're fetching the CaughtPokemon that the user has selected to edit. We're including their Gender and Pokemon info, and checking the UserId to make sure users can't edit another user's Pokemon
            model.SelectedCaughtPokemon = await _context.CaughtPokemon
                                                .Include(cp => cp.Pokemon)
                                                .Include(cp => cp.Gender)
                                                .Where(cp => cp.UserId == currentUser.Id)
                                                .Where(cp => cp.Id == id)
                                                .FirstOrDefaultAsync();

            //If the user is trying to access a Pokemon that doesn't exist, return a Not Found page
            if (model.SelectedCaughtPokemon == null)
            {
                return NotFound();
            }

            //We're now grabbing a list of every Pokemon possible, so we can let the user edit their Pokemon's species with a dropdown. In the games, Pokemon can evolve to a new species but retain their nickname, level, etc. so this edit feature is a handy way to handle such a scenario
            List<Pokemon> allPokemonToSelectFrom = await _context.Pokemon
                                                        .ToListAsync();

            //Creates a list of SelectListItems with each Pokemon's name and ID
            List<SelectListItem> lowercasePokemonChoices = new SelectList(allPokemonToSelectFrom, "Id", "Name", model.SelectedCaughtPokemon.PokemonId).ToList();

            //When I scraped PokeAPI to create my database, the Pokemon's names were all lowercase! So for this dropdown, we'd like their names to be uppercase. Let's begin by looping through the list we just made
            foreach (SelectListItem lowercaseChoice in lowercasePokemonChoices)
            {
                //Create a NEW SelectListItem with the same value and selected statuses as the original. BUT! The text will be a concatenated version of the lowercase string, with the first character capitalized
                SelectListItem uppercaseChoice = new SelectListItem()
                {
                    Selected = lowercaseChoice.Selected,
                    Text = char.ToUpper(lowercaseChoice.Text[0]) + lowercaseChoice.Text.Substring(1),
                    Value = lowercaseChoice.Value
                };

                //Adds the uppercase SelectListItem to our list in our ViewModel
                model.AllPokemon.Add(uppercaseChoice);
            }

            //Just like in the Create method, we're going to fetch our genders and make SelectListItems out of them
            List<Gender> gendersToSelectFrom = await _context.Gender.ToListAsync();

            model.Genders = new SelectList(gendersToSelectFrom, "Id", "Name", model.SelectedCaughtPokemon.GenderId).ToList();

            //Sends our ViewModel to our View
            return View(model);
        }

        // POST: CaughtPokemon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //This method takes all the user's input and updates the corresponding CaughtPokemon in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CaughtPokemonEditViewModel model)
        {
            //If the user has somehow mismatched the Pokemon IDs, returns a NotFound page
            if (id != model.SelectedCaughtPokemon.Id)
            {
                return NotFound();
            }

            //Checks if the ModelState is valid
            if (ModelState.IsValid)
            {
                try
                {
                    //Adds the updated CaughtPokemon from out ViewModel and sends it to our database
                    _context.Update(model.SelectedCaughtPokemon);
                    await _context.SaveChangesAsync();
                }
                //Exception handling if the update fails
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaughtPokemonExists(model.SelectedCaughtPokemon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //Once we edit a Pokemon in our Collection, return the user to their Collection view
                return RedirectToAction(nameof(Collection));
            }

            //If the edit fails, we're going to re-update our ViewModel and refresh the page. Much like the POST Create method, but out ViewModel is filled like in our GET Edit method instead.

            ApplicationUser currentUser = await GetCurrentUserAsync();

            CaughtPokemonEditViewModel failedModel = new CaughtPokemonEditViewModel();

            failedModel.SelectedCaughtPokemon = await _context.CaughtPokemon
                                                .Include(cp => cp.Pokemon)
                                                .Include(cp => cp.Gender)
                                                .Where(cp => cp.UserId == currentUser.Id)
                                                .Where(cp => cp.Id == id)
                                                .FirstOrDefaultAsync();

            if (failedModel.SelectedCaughtPokemon == null)
            {
                return NotFound();
            }

            List<Pokemon> allPokemonToSelectFrom = await _context.Pokemon
                                                        .ToListAsync();

            List<SelectListItem> lowercasePokemonChoices = new SelectList(allPokemonToSelectFrom, "Id", "Name", failedModel.SelectedCaughtPokemon.PokemonId).ToList();

            foreach (SelectListItem lowercaseChoice in lowercasePokemonChoices)
            {
                SelectListItem uppercaseChoice = new SelectListItem()
                {
                    Selected = lowercaseChoice.Selected,
                    Text = char.ToUpper(lowercaseChoice.Text[0]) + lowercaseChoice.Text.Substring(1),
                    Value = lowercaseChoice.Value
                };

                failedModel.AllPokemon.Add(uppercaseChoice);
            }

            List<Gender> gendersToSelectFrom = await _context.Gender.ToListAsync();

            failedModel.Genders = new SelectList(gendersToSelectFrom, "Id", "Name", failedModel.SelectedCaughtPokemon.GenderId).ToList();

            return View(failedModel);
        }

        // GET: CaughtPokemon/Delete/5
        //Allows the user to remove a single Pokemon from the collection
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            //Returns a NotFound page if the user tries to delete a CaughtPokemon resource that doesn't exist
            if (id == null)
            {
                return NotFound();
            }

            //Gets the current user
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Finds the CaughtPokemon the user is trying to delete. Also checks the UserID on that resource, so that users can't delete the Pokemon of another user
            var caughtPokemon = await _context.CaughtPokemon
                .Include(c => c.Gender)
                .Include(c => c.Pokemon)
                .Where(cp => cp.Id == id)
                .Where(cp => cp.UserId == currentUser.Id)
                .FirstOrDefaultAsync();

            //If that CaughtPokemon doesn't turn up, throw a NotFound page
            if (caughtPokemon == null)
            {
                return NotFound();
            }

            //Sends our desired Pokemon to the View, so we can show a final deletion confirmation screen
            return View(caughtPokemon);
        }

        // POST: CaughtPokemon/Delete/5
        //The method that actually deletes the CaughtPokemon resource
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Once the user has confirmed that yes, they want to deleted the specified Pokemon, we'll fetch that critter from the database, then delete it
            var caughtPokemon = await _context.CaughtPokemon.FindAsync(id);
            _context.CaughtPokemon.Remove(caughtPokemon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Collection));
        }

        //A simple method for just checking if a CaughtPokemon resource exists. Used for error handling in the Edit methods
        private bool CaughtPokemonExists(int id)
        {
            return _context.CaughtPokemon.Any(e => e.Id == id);
        }
    }
}
