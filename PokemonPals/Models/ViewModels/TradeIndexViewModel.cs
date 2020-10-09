using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class TradeIndexViewModel
    {
        public List<CaughtPokemon> SearchResults { get; set; } = new List<CaughtPokemon>();
        public List<int> CurrentUserCollection { get; set; } = new List<int>();
        public List<TradeRequest> IncomingRequests { get; set; } = new List<TradeRequest>();
        public List<TradeRequest> OutgoingRequests { get; set; } = new List<TradeRequest>();
    }
}
