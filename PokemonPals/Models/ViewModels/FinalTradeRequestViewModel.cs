using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class FinalTradeRequestViewModel
    {
        public int DesiredPokemonId { get; set; } = 0;
        public Boolean isDesiredOwned { get; set; } = false;
        public int OfferedPokemonId { get; set; } = 0;
    }
}