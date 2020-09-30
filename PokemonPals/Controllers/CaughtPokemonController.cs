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

        // GET: CaughtPokemons
        [Authorize]
        public async Task<IActionResult> Collection(string sortOrder)
        {
            ApplicationUser currentUser = await GetCurrentUserAsync();

            List<CaughtPokemon> FullUserCollection = await _context.CaughtPokemon
                                                            .Include(cp => cp.Pokemon)
                                                            .Include(cp => cp.Gender)
                                                            .Where(cp => cp.UserId == currentUser.Id)
                                                            .Where(cp => cp.isHidden == false)
                                                            .OrderBy(cp => cp.PokemonId)
                                                            .ToListAsync();

            ViewBag.SpeciesSortParm = String.IsNullOrEmpty(sortOrder) ? "species_desc" : "";
            ViewBag.FavoriteSortParm = sortOrder == "Favorite" ? "favorite_false" : "Favorite";
            ViewBag.TradeSortParm = sortOrder == "Open to Trade" ? "trade_false" : "Open to Trade";
            ViewBag.LevelSortParm = sortOrder == "Level" ? "level_asc" : "Level";
            ViewBag.CPSortParm = sortOrder == "CP" ? "CP_asc" : "CP";
            ViewBag.NicknameSortParm = sortOrder == "Nickname" ? "nickname_false" : "Nickname";
            ViewBag.CommentSortParm = sortOrder == "Comment" ? "comment_false" : "Comment";

            switch (sortOrder)
            {
                case "species_desc":
                    FullUserCollection = FullUserCollection.OrderByDescending(cp => cp.PokemonId).ToList();
                    break;
                case "Favorite":
                    FullUserCollection = FullUserCollection.Where(cp => cp.isFavorite).ToList();
                    break;
                case "favorite_false":
                    FullUserCollection = FullUserCollection.Where(cp => !cp.isFavorite).ToList();
                    break;
                case "Open to Trade":
                    FullUserCollection = FullUserCollection.Where(cp => cp.isTradeOpen).ToList();
                    break;
                case "trade_false":
                    FullUserCollection = FullUserCollection.Where(cp => !cp.isTradeOpen).ToList();
                    break;
                case "Level":
                    FullUserCollection = FullUserCollection.OrderByDescending(cp => cp.Level).ToList();
                    break;
                case "level_asc":
                    FullUserCollection = FullUserCollection.OrderBy(cp => cp.Level).ToList();
                    break;
                case "CP":
                    FullUserCollection = FullUserCollection.OrderByDescending(cp => cp.CP).ToList();
                    break;
                case "CP_asc":
                    FullUserCollection = FullUserCollection.OrderBy(cp => cp.CP).ToList();
                    break;
                case "Nickname":
                    FullUserCollection = FullUserCollection.Where(cp => cp.Nickname != null).ToList();
                    break;
                case "nickname_false":
                    FullUserCollection = FullUserCollection.Where(cp => cp.Nickname == null).ToList();
                    break;
                case "Comment":
                    FullUserCollection = FullUserCollection.Where(cp => cp.Comment != null && cp.Comment != "").ToList();
                    break;
                case "comment_false":
                    FullUserCollection = FullUserCollection.Where(cp => cp.Comment == null || cp.Comment == "").ToList();
                    break;
                default:
                    FullUserCollection = FullUserCollection.OrderBy(cp => cp.PokemonId).ToList();
                    break;
            }

            return View(FullUserCollection);
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
        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            CaughtPokemonCreateViewModel model = new CaughtPokemonCreateViewModel();
            model.SelectedPokemon = await _context.Pokemon
                                    .Where(p => p.Id == id)
                                    .FirstOrDefaultAsync();

            model.PokemonToAdd.PokemonId = id;

            List<Gender> gendersToSelectFrom = await _context.Gender.ToListAsync();

            model.Genders = new SelectList(gendersToSelectFrom, "Id", "Name").ToList();

            return View(model);
        }

        // POST: CaughtPokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaughtPokemonCreateViewModel model)
        {
            ModelState.Remove("User");
            ModelState.Remove("UserId");
            ModelState.Remove("SelectedPokemon.Name");
            ModelState.Remove("SelectedPokemon.Type1");
            ModelState.Remove("SelectedPokemon.RBSpriteURL");
            ModelState.Remove("SelectedPokemon.OfficialArtURL");
            ModelState.Remove("SelectedPokemon.DefaultSpriteURL");

            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await GetCurrentUserAsync();
                model.PokemonToAdd.UserId = currentUser.Id;

                _context.Add(model.PokemonToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction("Dex", "Pokemon");
            }

            CaughtPokemonCreateViewModel failedModel = new CaughtPokemonCreateViewModel();
            failedModel.SelectedPokemon = await _context.Pokemon
                                    .Where(p => p.Id == model.SelectedPokemon.Id)
                                    .FirstOrDefaultAsync();

            List<Gender> gendersToSelectFrom = await _context.Gender.ToListAsync();

            failedModel.Genders = new SelectList(gendersToSelectFrom, "Id", "Name").ToList();

            return View(failedModel);
        }

        // GET: CaughtPokemons/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser currentUser = await GetCurrentUserAsync();

            CaughtPokemonEditViewModel model = new CaughtPokemonEditViewModel();

            model.SelectedCaughtPokemon = await _context.CaughtPokemon
                                                .Include(cp => cp.Pokemon)
                                                .Include(cp => cp.Gender)
                                                .Where(cp => cp.UserId == currentUser.Id)
                                                .Where(cp => cp.Id == id)
                                                .FirstOrDefaultAsync();

            if (model.SelectedCaughtPokemon == null)
            {
                return NotFound();
            }

            List<Pokemon> allPokemonToSelectFrom = await _context.Pokemon
                                                        .ToListAsync();

            List<SelectListItem> lowercasePokemonChoices = new SelectList(allPokemonToSelectFrom, "Id", "Name", model.SelectedCaughtPokemon.PokemonId).ToList();

            foreach (SelectListItem lowercaseChoice in lowercasePokemonChoices)
            {
                SelectListItem uppercaseChoice = new SelectListItem()
                {
                    Selected = lowercaseChoice.Selected,
                    Text = char.ToUpper(lowercaseChoice.Text[0]) + lowercaseChoice.Text.Substring(1),
                    Value = lowercaseChoice.Value
                };

                model.AllPokemon.Add(uppercaseChoice);
            }

            List<Gender> gendersToSelectFrom = await _context.Gender.ToListAsync();

            model.Genders = new SelectList(gendersToSelectFrom, "Id", "Name", model.SelectedCaughtPokemon.GenderId).ToList();

            return View(model);
        }

        // POST: CaughtPokemons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CaughtPokemonEditViewModel model)
        {
            if (id != model.SelectedCaughtPokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model.SelectedCaughtPokemon);
                    await _context.SaveChangesAsync();
                }
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
                return RedirectToAction(nameof(Collection));
            }
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

        // GET: CaughtPokemons/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser currentUser = await GetCurrentUserAsync();

            var caughtPokemon = await _context.CaughtPokemon
                .Include(c => c.Gender)
                .Include(c => c.Pokemon)
                .Where(cp => cp.Id == id)
                .Where(cp => cp.UserId == currentUser.Id)
                .FirstOrDefaultAsync();

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
            return RedirectToAction(nameof(Collection));
        }

        private bool CaughtPokemonExists(int id)
        {
            return _context.CaughtPokemon.Any(e => e.Id == id);
        }
    }
}
