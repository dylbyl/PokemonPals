using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models
{
    public class CaughtPokemon
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Boolean isOwned { get; set; }
        public int PokemonId { get; set; }
        public Pokemon Pokemon { get; set; }
        public string UserId { get; set; }
        public ApplicationUser Owner { get; set; }
        public string Nickname { get; set; }
        public int Level { get; set; }
        public int CP { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public Boolean isHidden { get; set; }
        public Boolean isFavorite { get; set; }
        public Boolean isTradeOpen { get; set; }
    }
}
