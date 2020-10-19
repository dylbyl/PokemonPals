using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class UserCollectionViewModel
    {
        //The user whose profile we're viewing
        public ApplicationUser ViewedUser { get; set; } = new ApplicationUser();
        //The specified collection we're viewing (this ViewModel is used when we view full pages for FAvorite and Open for Trade Pokemon on someone's profile. This ViewModel is used for both pages)
        public List<CaughtPokemon> ViewedCollection { get; set; } = new List<CaughtPokemon>();
        //A list of IDs of the Pokemon caught by the user viewing the page. Any id in this list will have its Pokemon highlighted when the user views a Profile
        public List<int> CurrentUserCollection { get; set; } = new List<int>();
        //Tells us whether or no the user is viewing their own profile
        public Boolean isOwnProfile { get; set; } = false;
    }
}
