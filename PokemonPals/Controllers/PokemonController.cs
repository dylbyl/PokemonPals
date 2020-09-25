using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
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

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Pokemons
        public async Task<IActionResult> Dex()
        {
            ApplicationUser currentUser = await GetCurrentUserAsync();

            PokemonAndCaughtPokemonViewModel model = new PokemonAndCaughtPokemonViewModel();
            List<Pokemon> allPokemon = await _context.Pokemon.ToListAsync();
            List<CaughtPokemon> userCaughtPokemon = await _context.CaughtPokemon
                                                            .Where(cp => cp.UserId == currentUser.Id)
                                                            .ToListAsync();

            model.AllPokemon = allPokemon;

            return View(allPokemon);
        }

        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // GET: Pokemons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PokedexNumber,Height,Weight,Type1,Type2,DefaultSpriteURL,RBSpriteURL,OfficialArtURL,HP,Attack,Defense,SpecialAttack,SpecialDefense,Speed")] Pokemon pokemon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokemon);
        }

        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }

        // POST: Pokemons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PokedexNumber,Height,Weight,Type1,Type2,DefaultSpriteURL,RBSpriteURL,OfficialArtURL,HP,Attack,Defense,SpecialAttack,SpecialDefense,Speed")] Pokemon pokemon)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pokemon);
        }

        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemon.FindAsync(id);
            _context.Pokemon.Remove(pokemon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(e => e.Id == id);
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
