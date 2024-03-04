﻿namespace poke_gotchi.Entities
{
    public class User
    {
        public string? Name { get; set; }
        public List<Pokemon> AdoptedPokemon { get; set; } = new List<Pokemon>();

        public User() { }

        public void SetName(string? name)
        {
            Name = name;
        }

        public void SetAdoptedPokemon(List<Pokemon> adoptedPokemon)
        {
            AdoptedPokemon = adoptedPokemon;
        }

        public void AdoptPokemon(Pokemon pokemon)
        {
            AdoptedPokemon?.Add(pokemon);
        }
    }
}