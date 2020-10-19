using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class UserProfileViewModel
    {
        //The user whose profile we're viewing
        public ApplicationUser ViewedUser { get; set; } = new ApplicationUser();
        //Tells us whether or no the user is viewing their own profile
        public Boolean isOwnProfile { get; set; } = false;
        //A List of Pokemon caught by the user we're viewing
        public List<CaughtPokemon> UserCollection { get; set; } = new List<CaughtPokemon>();
        //A count of all Pokemon in our database (not CAUGHTPokemon, but plain Pokemon). This will be 151 to start, but if we decide to add more Pokemon later, we won't need to update every single instance of the Pokedex stats
        public int AllPokemonCount { get; set; } = 0;
        //A list of IDs of Pokemon caught by the user that we're looking at. We'll use Count() to create the user's Pokedex stats
        public List<int> CaughtPokemonIDs { get; set; } = new List<int>();

        //A list of IDs of the Pokemon caught by the user viewing the page. Any id in this list will have its Pokemon highlighted when the user views a Profile
        public List<int> CurrentUserCollection { get; set; } = new List<int>();
        //When we're viewing a user's page, this is a list of their favorite Pokemon
        public List<CaughtPokemon> FavoritePokemon { get; set; } = new List<CaughtPokemon>();
        //When we're viewing a user's page, this is a list of the Pokemon they have marked for Trade
        public List<CaughtPokemon> TradePokemon { get; set; } = new List<CaughtPokemon>();
    }
}
