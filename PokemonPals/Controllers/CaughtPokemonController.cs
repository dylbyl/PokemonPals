using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: CaughtPokemons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CaughtPokemon.Include(c => c.Gender).Include(c => c.Pokemon).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CaughtPokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caughtPokemon = await _context.CaughtPokemon
                .Include(c => c.Gender)
                .Include(c => c.Pokemon)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caughtPokemon == null)
            {
                return NotFound();
            }

            return View(caughtPokemon);
        }

        // GET: CaughtPokemons/Create
        public async Task<IActionResult> Create(int id)
        {
            CaughtPokemonCreateViewModel model = new CaughtPokemonCreateViewModel();
            model.SelectedPokemon = await _context.Pokemon
                                    .Where(p => p.Id == id)
                                    .FirstOrDefaultAsync();

            List<Gender> gendersToSelectFrom = await _context.Gender.ToListAsync();

            model.Genders = new SelectList(gendersToSelectFrom, "Id", "Name").ToList();

            return View(model);
        }

        // POST: CaughtPokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,isOwned,PokemonId,UserId,Nickname,Level,CP,GenderId,isHidden,isFavorite,isTradeOpen")] CaughtPokemon caughtPokemon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caughtPokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Name", caughtPokemon.GenderId);
            ViewData["PokemonId"] = new SelectList(_context.Pokemon, "Id", "DefaultSpriteURL", caughtPokemon.PokemonId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", caughtPokemon.UserId);
            return View(caughtPokemon);
        }

        // GET: CaughtPokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caughtPokemon = await _context.CaughtPokemon.FindAsync(id);
            if (caughtPokemon == null)
            {
                return NotFound();
            }
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Name", caughtPokemon.GenderId);
            ViewData["PokemonId"] = new SelectList(_context.Pokemon, "Id", "DefaultSpriteURL", caughtPokemon.PokemonId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", caughtPokemon.UserId);
            return View(caughtPokemon);
        }

        // POST: CaughtPokemons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,isOwned,PokemonId,UserId,Nickname,Level,CP,GenderId,isHidden,isFavorite,isTradeOpen")] CaughtPokemon caughtPokemon)
        {
            if (id != caughtPokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caughtPokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaughtPokemonExists(caughtPokemon.Id))
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
            ViewData["GenderId"] = new SelectList(_context.Gender, "Id", "Name", caughtPokemon.GenderId);
            ViewData["PokemonId"] = new SelectList(_context.Pokemon, "Id", "DefaultSpriteURL", caughtPokemon.PokemonId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", caughtPokemon.UserId);
            return View(caughtPokemon);
        }

        // GET: CaughtPokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caughtPokemon = await _context.CaughtPokemon
                .Include(c => c.Gender)
                .Include(c => c.Pokemon)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caughtPokemon == null)
            {
                return NotFound();
            }

            return View(caughtPokemon);
        }

        // POST: CaughtPokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caughtPokemon = await _context.CaughtPokemon.FindAsync(id);
            _context.CaughtPokemon.Remove(caughtPokemon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaughtPokemonExists(int id)
        {
            return _context.CaughtPokemon.Any(e => e.Id == id);
        }
    }
}
