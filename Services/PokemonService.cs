using poke_gotchi.Model;
using poke_gotchi.Model.Exceptions;
using poke_gotchi.Model.Responses;
using RestSharp;
using System.Text.Json;

namespace poke_gotchi.Services
{
    public class PokemonService
    {
        private static readonly RestClient client = new();
        private static readonly string BASE_URL = "https://pokeapi.co/api/v2/pokemon/";
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private static RestResponse GetPokemon(string url)
        {
            var request = new RestRequest(url, Method.Get);

            var response = client.Get(request);

            return response ?? throw new PokemonResponseNullException();
        }

        public PokemonListResponse GetPokemonList()
        {
            RestResponse response = GetPokemon(BASE_URL);

            PokemonListResponse? pokemonList = JsonSerializer.Deserialize<PokemonListResponse>(response.Content, options);

            return pokemonList ??
                   throw new DeserializationException(response, new Exception("GetPokemonListResponse"));
        }

        public Pokemon GetChosenPokemon(PokemonDetailResponse chosenPokemon)
        {
            RestResponse response = GetPokemon(chosenPokemon.Url);

            Pokemon? pokemon = JsonSerializer.Deserialize<Pokemon>(response.Content, options);

            return pokemon ??
                   throw new DeserializationException(response, new Exception("GetPokemon"));
        }
    }
}
