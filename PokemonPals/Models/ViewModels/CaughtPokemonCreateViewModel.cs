using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class CaughtPokemonCreateViewModel
    {
        //The species of the Pokemon we're entering into our collection
        public Pokemon SelectedPokemon { get; set; } = new Pokemon();
        //A list of genders that this Pokemon can be
        public List<SelectListItem> Genders { get; set; } = new List<SelectListItem>();
        //The final CaughtPokemon resource that will be added to our database
        public CaughtPokemon PokemonToAdd { get; set; } = new CaughtPokemon();
    }
}
