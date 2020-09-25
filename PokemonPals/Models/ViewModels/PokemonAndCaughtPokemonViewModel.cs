using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class PokemonAndCaughtPokemonViewModel
    {
        //A list of all Pokemon in the database
        public List<Pokemon> AllPokemon { get; set; }

        //IDs of the Pokemon this user has caught
        public List<int> PokemonCaught { get; set; }
    }
}
