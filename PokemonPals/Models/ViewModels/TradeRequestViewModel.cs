﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models.ViewModels
{
    public class TradeRequestViewModel
    {
        public CaughtPokemon DesiredPokemon { get; set; } = new CaughtPokemon();
        public int DesiredPokemonId { get; set; } = 0;
        public Boolean isDesiredOwned { get; set; } = false;
        public CaughtPokemon OfferedPokemon = new CaughtPokemon();
        public int OfferedPokemonId { get; set; } = 0;
        public List<CaughtPokemon> TradeOpenPokemon { get; set; } = new List<CaughtPokemon>();
        [StringLength(120)]
        public string Comment { get; set; }
    }
}
