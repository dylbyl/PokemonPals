using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class CaughtPokemonEditViewModel
    {
        //The Pokemon we're trying to edit
        public CaughtPokemon SelectedCaughtPokemon { get; set; } = new CaughtPokemon();
        //A list of Genders the Pokemon can be
        public List<SelectListItem> Genders { get; set; } = new List<SelectListItem>();
        //A list of every Pokemon species, so that we can edit the species of our CaughtPokemon
        public List<SelectListItem> AllPokemon { get; set; } = new List<SelectListItem>();
    }
}
