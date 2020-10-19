using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    //We need two different ViewModels for the entire process of sending a TradeRequest. This one is passed to the final method, when we know which Pokemon we're requesting and which one we're trading
    public class FinalTradeRequestViewModel
    {
        public int DesiredPokemonId { get; set; } = 0;
        public Boolean isDesiredOwned { get; set; } = false;
        public int OfferedPokemonId { get; set; } = 0;
    }
}