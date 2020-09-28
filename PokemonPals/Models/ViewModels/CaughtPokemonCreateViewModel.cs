using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class CaughtPokemonCreateViewModel
    {
        public Pokemon SelectedPokemon { get; set; }
        public CaughtPokemon PokemonToAdd { get; set; }
        public List<SelectListItem> Genders { get; set; }
    }
}
