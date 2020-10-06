using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokemonPals.Data;
using PokemonPals.Models;

namespace PokemonPals.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Username")]
            public string UserName { get; set; }
            [Display(Name = "About me")]
            public string Description { get; set; }
            [Display(Name = "Switch Friend Code")]
            public string SwitchCode { get; set; }
            [Display(Name = "Discord username")]
            public string DiscordUsername { get; set; }
            [Display(Name = "Favorite Pokemon game")]
            public int? GameId { get; set; }
            public List<SelectListItem> GameItems { get; set; }
            [Display(Name = "Favorite starter")]
            public int? AvatarId { get; set; }
            public List<SelectListItem> AvatarItems { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            List<Game> GameList = await _context.Game.ToListAsync();
            List<Avatar> AvatarList = await _context.Avatar.ToListAsync();

            Input = new InputModel
            {
                UserName = user.UserName,
                Description = user.Description,
                SwitchCode = user.SwitchCode,
                DiscordUsername = user.DiscordUsername,
                GameId = user.GameId,
                AvatarId = user.AvatarId,
                GameItems = new SelectList(GameList, "Id", "Name").ToList(),
                AvatarItems = new SelectList(AvatarList, "Id", "Name").ToList()
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //Input.UserName = Input.UserName.Replace(" ", "");

            user.UserName = Input.UserName;
            user.Description = Input.Description;
            user.SwitchCode = Input.SwitchCode;
            user.DiscordUsername = Input.DiscordUsername;
            user.GameId = Input.GameId;
            user.AvatarId = Input.AvatarId;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated";
                return RedirectToPage();
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            user = await _userManager.GetUserAsync(User);
            await LoadAsync(user);

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
