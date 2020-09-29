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
        public ApplicationUser User { get; set; }
        [StringLength(12)]
        public string Nickname { get; set; }
        [Range(1, 100)]
        public int Level { get; set; }
        public int CP { get; set; }
        public int GenderId { get; set; }
        public string Comment { get; set; }
        public Gender Gender { get; set; }
        public Boolean isHidden { get; set; }
        public Boolean isFavorite { get; set; }
        public Boolean isTradeOpen { get; set; }
        public DateTime DateCaught { get; set; }
        public CaughtPokemon()
        {
            isOwned = true;
            isHidden = false;
            PokemonId = 0;
            UserId = null;
            Level = 0;
            CP = 0;
            GenderId = 0;
            Nickname = null;
            Comment = null;
            isFavorite = false;
            isTradeOpen = false;
            DateCaught = DateTime.Now;
        }
    }
}
