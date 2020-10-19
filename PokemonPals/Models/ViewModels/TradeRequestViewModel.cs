using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class TradeRequestViewModel
    {
        //The CaughtPokemon resource that you're requesting a trade for
        public CaughtPokemon DesiredPokemon { get; set; } = new CaughtPokemon();
        //The ID of the DesiredPokemon
        public int DesiredPokemonId { get; set; } = 0;
        //Whether or not the Pokemon you're requesting is from a species you already own
        public Boolean isDesiredOwned { get; set; } = false;
        //The Pokemon you're offering up in return for the desired Pokemon
        public CaughtPokemon OfferedPokemon = new CaughtPokemon();
        //The ID of the OfferedPokemon
        public int OfferedPokemonId { get; set; } = 0;
        //A list of Pokemon you own that are marked for Trade
        public List<CaughtPokemon> TradeOpenPokemon { get; set; } = new List<CaughtPokemon>();
        //A comment you can attach to the Trade Request for the other user to see
        [StringLength(120)]
        public string Comment { get; set; }
    }
}
