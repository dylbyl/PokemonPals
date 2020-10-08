using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
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
    public class PokemonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PokemonController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //A method for fetching the currently logged in user
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // A method for fetching all Pokemon in the database, and returning a view of them
        [Authorize]
        public async Task<IActionResult> Dex(string sortOrder, string searchString)
        {
            //Gets the currently logged in user
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Creates a new view model for the Dex view
            PokemonAndCaughtPokemonViewModel model = new PokemonAndCaughtPokemonViewModel();

            //Fetches all Pokemon entries in the database and adds them to a list
            model.AllPokemon = await _context.Pokemon.OrderBy(p => p.PokedexNumber).ToListAsync();

            //Runs if a search term has been entered on the Dex view
            if (!String.IsNullOrEmpty(searchString))
            {
                //Uses a LINQ Where statement to find any Pokemon whose name contains the search string
                model.AllPokemon = model.AllPokemon.Where(p => p.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            }

                //These lines check to see if a parameter was passed to this method to determine if the collection is sorted. If so, the data stored in the ViewBag is switched to the inverse sortOrder string, so that when the user clicks the sort link a second time, the sort order will be reversed.
                ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";

            ViewBag.SortOrderString = sortOrder;
            ViewBag.SearchString = searchString;

            //A switch-case that checks the sortOrder string, and properly sorts the Pokedex accordingly
            switch (sortOrder)
            {
                case "number_desc":
                    model.AllPokemon = model.AllPokemon.OrderByDescending(p => p.PokedexNumber).ToList();
                    break;
                case "name_desc":
                    model.AllPokemon = model.AllPokemon.OrderByDescending(p => p.Name).ToList();
                    break;
                case "name_asc":
                    model.AllPokemon = model.AllPokemon.OrderBy(p => p.Name).ToList();
                    break;
                default:
                    model.AllPokemon = model.AllPokemon.OrderBy(p => p.PokedexNumber).ToList();
                    break;
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
                if (!model.PokemonCaught.Contains(caughtPokemon.PokemonId))
                {
                    //...add it to the list of IDs
                    model.PokemonCaught.Add(caughtPokemon.PokemonId);
                }
            }

            //Returns a view with our view model
            return View(model);
        }

        // GET: Pokemon/Details/5
        //A method for showing the Details view of a single Pokemon species
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            //Throws a NotFound page if the user tries to access a Pokemon that doesn't exist
            if (id == null)
            {
                return NotFound();
            }

            //gets the currently logged in user
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //creates a ViewModel for our details page, so we can access a few different objects from our HTML
            CaughtPokemonDexViewModel model = new CaughtPokemonDexViewModel();

            //Fetches the details of the Pokemon species that the user wishes to view
            model.SelectedPokemon = await _context.Pokemon
                .FirstOrDefaultAsync(m => m.Id == id);

            //Fetches all CaughtPokemon resources that belong to this user and match the current Pokemon species. We're going to show the user all Pokemon they own of the species they're viewing.
            model.UserCollection = await _context.CaughtPokemon
                                        .Include(cp => cp.Pokemon)
                                        .Include(cp => cp.Gender)
                                        .Where(cp => cp.PokemonId == id)
                                        .Where(cp => cp.UserId == currentUser.Id)
                                        .Where(cp => cp.isHidden == false)
                                        .ToListAsync();

            //If the desired Pokemon doesn't exist in our database, send a NotFound page
            if (model.SelectedPokemon == null)
            {
                return NotFound();
            }

            //Send us to the Details view with our ViewModel in hand
            return View(model);
        }

        //A task for adding a single Pokemon to our database, using data from the public PokeAPI. Call this in a loop ONLY FOR ONE RUN. Afterwards, some names may still need to be edited (for instance "Mr. Mime" was saved as "mr-mime")
        //Accepts a Pokemon's Pokedex Number as a parameter. For the first run, I put this in a loop and passed in the iterator, i, while i was 1 to 151. You can do this for every generation of Pokemon, but the API only lets you call it 300 times a day. There are 800+ Pokemon. For the sake of simplicity, I scraped data of the original 151 and held them in my own SQL server to cut down on API calls and load times.
        public async Task AddOnePokemon(int PokedexNumber)
        {
            //The url for fetching a single Pokemon from the API, using the parameter as part of the URL
            var url = $"https://pokeapi.co/api/v2/pokemon/{PokedexNumber}/";

            //Opens up an httpClient and passes in our API url
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync(url);

            //Runs if we successfully get a response from the API
            if (response.IsSuccessStatusCode)
            {
                //Stores the API result as JSON...
                var json = await response.Content.ReadAsStreamAsync();
                //...then converts it to a special PokemonData resource that was auto-populated by Visual Studio based on the API's JSON response structure
                var PokemonData = await JsonSerializer.DeserializeAsync<PokemonResponse>(json);

                //A quick few lines of code to turn the Pokemon's Pokedex Number (ie. 5) into a three digit number (ie. 005). This is used in string interpolation to link to the Pokemon's official artwork on the Pokemon site.
                string threeDigitId = "";

                if (PokedexNumber < 10)
                {
                    threeDigitId = $"00{PokedexNumber}";
                }
                else if (PokedexNumber < 100)
                {
                    threeDigitId = $"0{PokedexNumber}";
                }
                else
                {
                    threeDigitId = $"{PokedexNumber}";
                }

                //Instantiates a new Pokemon object to save the API info in. We're not storing ALL the API data in our database, just the relevant info
                Pokemon OnePokemon;

                //Creates a Pokemon object with only a single "type" property, "Type2" will be null
                if (PokemonData.types.Count() == 1)
                {
                    OnePokemon = new Pokemon
                    {
                        Name = PokemonData.name,
                        PokedexNumber = PokemonData.id,
                        Height = PokemonData.height,
                        Weight = PokemonData.weight,
                        Type1 = PokemonData.types[0].type.name,
                        Type2 = null,
                        DefaultSpriteURL = PokemonData.sprites.front_default,
                        RBSpriteURL = PokemonData.sprites.versions.generationi.redblue.front_default,
                        OfficialArtURL = $"https://assets.pokemon.com/assets/cms2/img/pokedex/full/{threeDigitId}.png",
                        HP = PokemonData.stats[0].base_stat,
                        Attack = PokemonData.stats[1].base_stat,
                        Defense = PokemonData.stats[2].base_stat,
                        SpecialAttack = PokemonData.stats[3].base_stat,
                        SpecialDefense = PokemonData.stats[4].base_stat,
                        Speed = PokemonData.stats[5].base_stat
                    };
                }
                //If the Pokemon in the API has two types, create a Pokemon with Type1 and Type2 filled in
                else
                {
                    OnePokemon = new Pokemon
                    {
                        Name = PokemonData.name,
                        PokedexNumber = PokemonData.id,
                        Height = PokemonData.height,
                        Weight = PokemonData.weight,
                        Type1 = PokemonData.types[0].type.name,
                        Type2 = PokemonData.types[1].type.name,
                        DefaultSpriteURL = PokemonData.sprites.front_default,
                        RBSpriteURL = PokemonData.sprites.versions.generationi.redblue.front_default,
                        OfficialArtURL = $"https://assets.pokemon.com/assets/cms2/img/pokedex/full/{threeDigitId}.png",
                        HP = PokemonData.stats[0].base_stat,
                        Attack = PokemonData.stats[1].base_stat,
                        Defense = PokemonData.stats[2].base_stat,
                        SpecialAttack = PokemonData.stats[3].base_stat,
                        SpecialDefense = PokemonData.stats[4].base_stat,
                        Speed = PokemonData.stats[5].base_stat
                    };
                }

                //If you look at OfficialArtURL above, you'll see how we used the threeDigitID variable

                //Adds this newly created Pokemon object to our database
                _context.Add(OnePokemon);
                await _context.SaveChangesAsync();
            }
        }

        //When I scraped the data from the API, I realized AFTER the fact that the Pokemon names and types were all lowercase. So I devised a method (which, again, ran in only one loop) to fetch a Pokemon, capitalize its name and types, then update its entry in the database. Some Pokemon were stored *even weirder* and needed further adjusting (Mr. Mime and the two types of Nidoran were stored as "mr-mime," "nidoran-m," and "nidoran-f"). I adjusted these three with a SQL query after running this method. My plan is to export my entire database as a giant SQL query so that anyone who needs the data for testing can run a single script instead of having to manually scrape the API and use these methods. I'm keeping them just for future reference!
        public async Task CapitalizeOnePokemon(int id)
        {
            //Grabs a single Pokemon
            Pokemon pokemonToChange = await _context.Pokemon.FirstOrDefaultAsync(p => p.Id == id);

            //Capitalizes its name
            pokemonToChange.Name = char.ToUpper(pokemonToChange.Name[0]) + pokemonToChange.Name.Substring(1);

            //Capitalizes its first type
            pokemonToChange.Type1 = char.ToUpper(pokemonToChange.Type1[0]) + pokemonToChange.Type1.Substring(1);

            //If it has a second type, capitalizes it too
            if (pokemonToChange.Type2 != null)
            {
                pokemonToChange.Type2 = char.ToUpper(pokemonToChange.Type2[0]) + pokemonToChange.Type2.Substring(1);
            }

            //Saves the update Pokemon to our database
            _context.Update(pokemonToChange);
            await _context.SaveChangesAsync();
        }
    }
}
