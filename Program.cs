using poke_gotchi.Entities;
using poke_gotchi.Responses;
using RestSharp;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace poke_gotchi
{
    public class Program
    {
        private static readonly RestClient client = new();
        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        private static User user = new();
        private static readonly string BASE_URL = "https://pokeapi.co/api/v2/pokemon/";

        public static void Main()
        {
            Console.WriteLine("Welcome! Here you'll be able to choose your lovely friend, your Pokemon!");
            Console.Write("To begin, please tell us your name: ");
            user.SetName(Console.ReadLine());

            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Main Menu");
                Console.WriteLine($"{user.Name}, what do you wish?");
                Console.WriteLine("1 - Adopt a new pokemon");
                Console.WriteLine("2 - View adopted pokemon");
                Console.WriteLine("3 - Exit");

                if (!int.TryParse(Console.ReadLine(), out int menuChoice))
                {
                    Console.WriteLine("Invalid option");
                    continue;
                }
                else
                {
                    switch (menuChoice)
                    {
                        case 1:
                            AdoptPokemonMenu();
                            continue;
                        case 2:
                            AdoptedMenu();
                            continue;
                        case 3:
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            continue;
                    }
                    break;
                }
            }
        }

        private static void AdoptPokemonMenu()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Choose a Pokemon from the list below:");

            string? response = GetPokemonResponse(BASE_URL);

            PokemonListResponse? pokemonList = response == null ?
                null :
                JsonSerializer.Deserialize<PokemonListResponse>(response, options);

            for (int i = 0; i < pokemonList?.Results.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {pokemonList.Results[i].Name}");
            }

            int choice;

            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= pokemonList?.Results.Count)
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
                else
                    break;
            }

            Pokemon pokemon = GetPokemon(pokemonList.Results[choice - 1]);

            AdoptionMenu(pokemon);
        }

        private static void AdoptionMenu(Pokemon pokemon)
        {
            while (true)
            {
                Console.WriteLine("\n");
                Console.WriteLine($"{user.Name}, would you like to: ");
                Console.WriteLine($"1 - View information about {pokemon.Name}");
                Console.WriteLine($"2 - Adopt {pokemon.Name}");
                Console.WriteLine("3 - Return");

                if (!int.TryParse(Console.ReadLine(), out int menuChoice))
                {
                    Console.WriteLine("Invalid option");
                    continue;
                }
                else
                {
                    switch (menuChoice)
                    {
                        case 1:
                            GetPokemonInformation(pokemon);
                            continue;
                        case 2:
                            AdoptPokemon(pokemon);
                            break;
                        case 3:
                            break;
                        default:
                            Console.WriteLine("Invalid option");
                            continue;
                    }
                    break;
                }
            }
        }

        private static void AdoptedMenu()
        {
            foreach (var pokemon in user.AdoptedPokemon)
            {
                Console.WriteLine(pokemon.Name);
            }
        }

        private static string? GetPokemonResponse(string url)
        {
            var request = new RestRequest(url, Method.Get);

            var response = client.Get(request).Content;

            return response;
        }

        private static void AdoptPokemon(Pokemon pokemon)
        {
            user.AdoptPokemon(pokemon);
            Console.WriteLine($"{user.Name}, you have adopted {pokemon.Name} successfully!");
        }

        private static void GetPokemonInformation(Pokemon pokemon)
        {
            List<string>? abilitiesNames = pokemon == null ?
                new List<string>() :
                pokemon.Abilities.Select(x => x.Ability.Name).ToList();

            Console.Write("\n");
            Console.WriteLine("Chosen Pokemon \n");

            Console.WriteLine($"Name: {pokemon?.Name}");
            Console.WriteLine($"Height: {pokemon?.Height}");
            Console.WriteLine($"Weight: {pokemon?.Weight}");
            Console.WriteLine("Abilities: ");

            foreach (var abilityName in abilitiesNames)
            {
                Console.WriteLine(abilityName);
            }
        }

        private static Pokemon GetPokemon(PokemonDetailResponse chosenPokemon)
        {
            var request = new RestRequest(chosenPokemon.Url, Method.Get);

            var response = client.Get(request).Content;

            Pokemon? pokemon = response == null ?
                null :
                JsonSerializer.Deserialize<Pokemon>(response, options);

            return pokemon;
        }
    }
}