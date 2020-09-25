using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonPals.Models
{
    public class Pokemon
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int PokedexNumber { get; set; }
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        [Required]
        public string DefaultSpriteURL { get; set; }
        [Required]
        public string RBSpriteURL { get; set; }
        [Required]
        public string OfficialArtURL { get; set; }
        [Required]
        public int HP { get; set; }
        [Required]
        public int Attack { get; set; }
        [Required]
        public int Defense { get; set; }
        [Required]
        public int SpecialAttack { get; set; }
        [Required]
        public int SpecialDefense { get; set; }
        [Required]
        public int Speed { get; set; }
    }
}
