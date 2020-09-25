using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class PokemonAndCaughtPokemonViewModel
    {
        public List<Pokemon> AllPokemon { get; set; }
        public List<int> PokemonCaught { get; set; }
    }
}
