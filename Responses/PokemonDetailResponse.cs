﻿namespace poke_gotchi.Responses
{
    public class PokemonDetailResponse
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public PokemonDetailResponse(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}