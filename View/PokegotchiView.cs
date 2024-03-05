using poke_gotchi.Model;
using poke_gotchi.Model.Responses;
using System.Xml.Linq;

namespace poke_gotchi.View
{
    public class PokegotchiView
    {
        public static void Welcome()
        {
            Console.WriteLine("Welcome! Here you'll be able to choose your lovely friend, your Pokemon!");
            Console.Write("To begin, please tell us your name: ");
        }

        public static void MainMenu(string username)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Main Menu");
            Console.WriteLine($"{username}, what do you wish?");
            Console.WriteLine("1 - Adopt a new pokemon");
            Console.WriteLine("2 - View adopted pokemon");
            Console.WriteLine("3 - Exit");
        }

        public static void ChoosePokemonMenu(PokemonListResponse pokemonList)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Choose a Pokemon from the list below:");

            for (int i = 0; i < pokemonList?.Results.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {pokemonList.Results[i].Name}");
            }
        }

        public static void AdoptPokemonMenu(string username, string pokemonName)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"{username}, would you like to: ");
            Console.WriteLine($"1 - View information about {pokemonName}");
            Console.WriteLine($"2 - Adopt {pokemonName}");
            Console.WriteLine("3 - Return");
        }

        public static void PokemonInformation(Pokemon pokemon, List<string> abilitiesNames)
        {
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

        public static void AdoptedPokemonMessage(string username, string pokemonName)
        {
            Console.WriteLine($"{username}, you have adopted {pokemonName} successfully!");
        }

        public static void AdoptedPokemonList(List<Pokemon> pokemonList)
        {
            foreach (var pokemon in pokemonList)
            {
                Console.WriteLine(pokemon.Name);
            }
        }
    }
}
