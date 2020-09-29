using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class CaughtPokemonDexViewModel
    {
        public Pokemon SelectedPokemon { get; set; } = new Pokemon();

        public List<CaughtPokemon> UserCollection { get; set; } = new List<CaughtPokemon>();
        
    }
}
