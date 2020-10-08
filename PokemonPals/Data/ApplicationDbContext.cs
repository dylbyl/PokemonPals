using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokemonPals.Models;
using Microsoft.AspNetCore.Identity;

namespace PokemonPals.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Avatar> Avatar { get; set; }
        public DbSet<CaughtPokemon> CaughtPokemon { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<TradeRequest> TradeRequest { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<CaughtPokemon>()
        //        .HasMany(cp => cp.TradeRequests)
        //        .WithOne(tr => tr.DesiredPokemon)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<CaughtPokemon>()
        //        .HasMany(cp => cp.TradeRequests)
        //        .WithOne(tr => tr.OfferedPokemon)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}
    }
}
