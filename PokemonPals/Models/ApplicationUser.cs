using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Description { get; set; }
        public string SwitchCode { get; set; }
        public string DiscordUsername { get; set; }
        public int? GameId { get; set; }
        public Game Game { get; set; }
        public int? AvatarId { get; set; }
        public Avatar Avatar { get; set; }
    }
}
