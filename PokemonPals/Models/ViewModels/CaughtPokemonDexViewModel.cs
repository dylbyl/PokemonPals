using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    //A ViewModel for viewing a single Pokemon species' Details
    public class CaughtPokemonDexViewModel
    {
        //Holds the Pokemon species we're currently looking at
        public Pokemon SelectedPokemon { get; set; } = new Pokemon();
        //A list of every Pokemon the user owns of this species
        public List<CaughtPokemon> UserCollection { get; set; } = new List<CaughtPokemon>();
        
    }
}
