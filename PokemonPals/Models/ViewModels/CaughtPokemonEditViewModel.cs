using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class CaughtPokemonEditViewModel
    {
        public CaughtPokemon SelectedCaughtPokemon { get; set; } = new CaughtPokemon();
        public List<SelectListItem> Genders { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> AllPokemon { get; set; } = new List<SelectListItem>();
    }
}
