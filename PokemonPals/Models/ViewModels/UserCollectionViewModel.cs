using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class UserCollectionViewModel
    {
        public ApplicationUser ViewedUser { get; set; } = new ApplicationUser();
        public List<CaughtPokemon> ViewedCollection { get; set; } = new List<CaughtPokemon>();
        public List<int> CurrentUserCollection { get; set; } = new List<int>();
        public Boolean isOwnProfile { get; set; } = false;
    }
}
