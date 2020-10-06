using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class UserCollectionViewModel
    {
        public ApplicationUser ViewedUser = new ApplicationUser();
        public List<CaughtPokemon> ViewedCollection = new List<CaughtPokemon>();
        public List<int> CurrentUserCollection = new List<int>();
    }
}
