using poke_gotchi.Model;
using poke_gotchi.Model.Exceptions;
using poke_gotchi.Model.Responses;
using poke_gotchi.View;
using RestSharp;
using System.Text.Json;

namespace poke_gotchi.Controller
{
    public class PokegotchiController
    {
        private static readonly User user = new();
        private static readonly RestClient client = new();
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        private static readonly string BASE_URL = "https://pokeapi.co/api/v2/pokemon/";

        public static void Play()
        {
            PokegotchiView.Welcome();

            string username = Console.ReadLine() ?? "Unknown User";
            user.SetName(username);

            int play = 1;

            while (play == 1)
            {
                PokegotchiView.MainMenu(user.Name);

                switch (Console.ReadLine())
                {
                    case "1":
                        AdoptPokemonMenu();
                        break;
                    case "2":
                        AdoptedPokemon();
                        break;
                    case "3":
                        play = 0;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private static void AdoptPokemonMenu()
        {
            PokemonListResponse pokemonList = GetPokemonListResponse();

            PokegotchiView.ChoosePokemonMenu(pokemonList);

            int choice;

            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= pokemonList.Results.Count)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
                else
                    break;
            }

            Pokemon pokemon = GetPokemon(pokemonList.Results[choice - 1]);

            AdoptionMenu(pokemon);
        }

        private static void AdoptedPokemon()
        {
            PokegotchiView.AdoptedPokemonList(user.AdoptedPokemon);
        }

        private static RestResponse GetPokemonResponse(string url)
        {
            var request = new RestRequest(url, Method.Get);

            var response = client.Get(request);

            return response ?? throw new PokemonResponseNullException();
        }

        private static PokemonListResponse GetPokemonListResponse()
        {
            RestResponse response = GetPokemonResponse(BASE_URL);

            PokemonListResponse? pokemonList = JsonSerializer.Deserialize<PokemonListResponse>(response.Content, options);

            return pokemonList ??
                   throw new DeserializationException(response, new Exception("GetPokemonListResponse"));
        }

        private static Pokemon GetPokemon(PokemonDetailResponse chosenPokemon)
        {
            RestResponse response = GetPokemonResponse(chosenPokemon.Url);

            Pokemon? pokemon = JsonSerializer.Deserialize<Pokemon>(response.Content, options);

            return pokemon ??
                   throw new DeserializationException(response, new Exception("GetPokemon"));
        }

        private static void AdoptionMenu(Pokemon pokemon)
        {
            int adopt = 1;

            while (adopt == 1)
            {
                PokegotchiView.AdoptPokemonMenu(user.Name, pokemon.Name);

                switch (Console.ReadLine())
                {
                    case "1":
                        GetPokemonInformation(pokemon);
                        break;
                    case "2":
                        AdoptPokemon(pokemon);
                        adopt = 0;
                        break;
                    case "3":
                        adopt = 0;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private static void GetPokemonInformation(Pokemon pokemon)
        {
            List<string> abilitiesNames = pokemon.Abilities.Select(x => x.Ability.Name).ToList();

            PokegotchiView.PokemonInformation(pokemon, abilitiesNames);
        }

        private static void AdoptPokemon(Pokemon pokemon)
        {
            user.AdoptPokemon(pokemon);

            PokegotchiView.AdoptedPokemonMessage(user.Name, pokemon.Name);
        }
    }
}
