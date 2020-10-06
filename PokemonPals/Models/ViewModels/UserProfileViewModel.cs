using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public ApplicationUser ViewedUser = new ApplicationUser();
        public Boolean isOwnProfile = false;
        public List<CaughtPokemon> UserCollection = new List<CaughtPokemon>();
        public int AllPokemonCount = 0;
        public List<int> CaughtPokemonIDs = new List<int>();
        public List<CaughtPokemon> FavoritePokemon = new List<CaughtPokemon>();
        public List<CaughtPokemon> TradePokemon = new List<CaughtPokemon>();
    }
}
