using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class TradeIndexViewModel
    {
        //A list of CaughtPokemon that match the search term
        public List<CaughtPokemon> SearchResults { get; set; } = new List<CaughtPokemon>();
        //A list of IDs of the Pokemon the currently logged in user has caught. With this, any Pokemon that has an ID in this list will be highlighted as "caught"
        public List<int> CurrentUserCollection { get; set; } = new List<int>();
        //A list of trade requests sent TO the current user
        public List<TradeRequest> IncomingRequests { get; set; } = new List<TradeRequest>();
        //A list of trade requests sent BY the current user
        public List<TradeRequest> OutgoingRequests { get; set; } = new List<TradeRequest>();
    }
}
