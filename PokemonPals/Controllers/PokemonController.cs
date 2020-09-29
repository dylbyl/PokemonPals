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
        public async Task<IActionResult> Dex()
        {
            //Gets the currently logged in user
            ApplicationUser currentUser = await GetCurrentUserAsync();

            //Creates a new view model for the Dex view
            PokemonAndCaughtPokemonViewModel model = new PokemonAndCaughtPokemonViewModel();

            //Fetches all Pokemon entries in the database and adds them to a list
            model.AllPokemon = await _context.Pokemon.ToListAsync();

            //Gets a list of all the Pokemon the current user has caught (and have not been soft-deleted)
            List<CaughtPokemon> listOfUserCaughtPokemon = await _context.CaughtPokemon
                                                            .Where(cp => cp.UserId == currentUser.Id)
                                                            .Where(cp => cp.isHidden == false)
                                                            .ToListAsync();

            //Loops through the list of Pokemon the user owns, so that we can get their IDs
            foreach(CaughtPokemon caughtPokemon in listOfUserCaughtPokemon)
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

        // GET: Pokemons/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser currentUser = await GetCurrentUserAsync();

            CaughtPokemonDexViewModel model = new CaughtPokemonDexViewModel();

            model.SelectedPokemon = await _context.Pokemon
                .FirstOrDefaultAsync(m => m.Id == id);

            model.UserCollection = await _context.CaughtPokemon
                                        .Include(cp => cp.Pokemon)
                                        .Include(cp => cp.Gender)
                                        .Where(cp => cp.UserId == currentUser.Id)
                                        .Where(cp => cp.isHidden == false)
                                        .ToListAsync();
            
            if (model.SelectedPokemon == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public async Task AddOnePokemon(int PokedexNumber)
        {
            var url = $"https://pokeapi.co/api/v2/pokemon/{PokedexNumber}/";
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStreamAsync();
                var PokemonData = await JsonSerializer.DeserializeAsync<PokemonResponse>(json);

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

                Pokemon OnePokemon;

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

                _context.Add(OnePokemon);
                await _context.SaveChangesAsync();
            }
        }
    }
}
