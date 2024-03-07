using AutoMapper;
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
        private readonly Mapper mapper;
        private readonly PokegotchiView view;
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        private static readonly string BASE_URL = "https://pokeapi.co/api/v2/pokemon/";

        public PokegotchiController()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Pokemon, AdoptedPokemon>());
            mapper = new Mapper(config);
            view = new PokegotchiView();
        }

        public void Play()
        {
            view.Welcome();

            string username = Console.ReadLine() ?? "Unknown User";
            user.SetName(username);

            int play = 1;

            while (play == 1)
            {
                view.MainMenu(user.Name);

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

        private void AdoptPokemonMenu()
        {
            PokemonListResponse pokemonList = GetPokemonListResponse();

            view.ChoosePokemonMenu(pokemonList);

            int choice;

            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out choice) || choice == 0 || choice > pokemonList.Results.Count)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
                else
                    break;
            }

            Pokemon pokemon = GetPokemon(pokemonList.Results[choice - 1]);

            AdoptionMenu(pokemon);
        }

        private void AdoptedPokemon()
        {
            if (user.AdoptedPokemon.Count == 0)
            {
                view.AdoptedPokemonEmpty();
                return;
            }

            int pokemonChoosed;
            
            while (true)
            {
                view.AdoptedPokemonList(user.AdoptedPokemon, user.Name);

                if (!int.TryParse(Console.ReadLine(), out pokemonChoosed) || pokemonChoosed < 0 || pokemonChoosed > user.AdoptedPokemon.Count)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
                else
                    break;
            }

            if (pokemonChoosed != 0)
                InteractPokemon(user.AdoptedPokemon[pokemonChoosed - 1]);
        }

        private void InteractPokemon(AdoptedPokemon pokemon)
        {
            int interaction = 1;

            while (interaction == 1)
            {
                view.InteractPokemonMenu(user.Name, pokemon.Name);

                switch (Console.ReadLine())
                {
                    case "1":
                        GetPokemonStatus(pokemon);
                        break;
                    case "2":
                        FeedPokemon(pokemon);
                        break;
                    case "3":
                        PlayWithPokemon(pokemon);
                        break;
                    case "4":
                        RestPokemon(pokemon);
                        break;
                    case "5":
                        interaction = 0;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private void FeedPokemon(AdoptedPokemon pokemon)
        {
            pokemon.Feed();
            view.FeedPokemon(pokemon.Name);
        }

        private void PlayWithPokemon(AdoptedPokemon pokemon)
        {
            pokemon.Play();
            view.PlayWithPokemon(pokemon.Name);
        }

        private void RestPokemon(AdoptedPokemon pokemon)
        {
            pokemon.Rest();
            view.RestPokemon(pokemon.Name);
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

        private void AdoptionMenu(Pokemon pokemon)
        {
            int adopt = 1;

            while (adopt == 1)
            {
                view.AdoptPokemonMenu(user.Name, pokemon.Name);

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

        private void GetPokemonInformation(Pokemon pokemon)
        {
            List<string> abilitiesNames = pokemon.Abilities.Select(x => x.Ability.Name).ToList();

            view.PokemonInformation(pokemon, abilitiesNames);
        }

        private void GetPokemonStatus(AdoptedPokemon pokemon)
        {
            GetPokemonInformation(pokemon);
            view.AdoptedPokemonStatus(pokemon);
        }

        private void AdoptPokemon(Pokemon pokemon)
        {
            AdoptedPokemon adoptedPokemon = mapper.Map<AdoptedPokemon>(pokemon);

            user.AdoptPokemon(adoptedPokemon);
            view.AdoptedPokemonMessage(user.Name, adoptedPokemon.Name);
        }
    }
}
