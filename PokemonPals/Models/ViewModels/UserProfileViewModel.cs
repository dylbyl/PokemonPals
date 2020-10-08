using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class UserProfileViewModel
    {
        public ApplicationUser ViewedUser { get; set; } = new ApplicationUser();
        public Boolean isOwnProfile { get; set; } = false;
        public List<CaughtPokemon> UserCollection { get; set; } = new List<CaughtPokemon>();
        public int AllPokemonCount { get; set; } = 0;
        public List<int> CaughtPokemonIDs { get; set; } = new List<int>();
        public List<int> CurrentUserCollection { get; set; } = new List<int>();
        public List<CaughtPokemon> FavoritePokemon { get; set; } = new List<CaughtPokemon>();
        public List<CaughtPokemon> TradePokemon { get; set; } = new List<CaughtPokemon>();
    }
}
