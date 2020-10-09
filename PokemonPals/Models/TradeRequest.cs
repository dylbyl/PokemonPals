using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models
{
    public class TradeRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int DesiredPokemonId { get; set; }
        public CaughtPokemon DesiredPokemon { get; set; } = new CaughtPokemon();
        [Required]
        public int OfferedPokemonId { get; set; }
        public CaughtPokemon OfferedPokemon { get; set; } = new CaughtPokemon();
        [StringLength(120)]
        public string Comment { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime? DateCompleted { get; set; }
        public Boolean isOpen { get; set; }
    }
}
