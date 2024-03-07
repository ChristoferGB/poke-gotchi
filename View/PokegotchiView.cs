using poke_gotchi.Model;
using poke_gotchi.Model.Responses;

namespace poke_gotchi.View
{
    public class PokegotchiView
    {
        public void Welcome()
        {
            Console.WriteLine("Welcome! Here you'll be able to choose your lovely friend, your Pokemon!");
            Console.Write("To begin, please tell us your name: ");
        }

        public void MainMenu(string username)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Main Menu");
            Console.WriteLine($"{username}, what do you wish?");
            Console.WriteLine("1 - Adopt a new pokemon");
            Console.WriteLine("2 - View adopted pokemon");
            Console.WriteLine("3 - Exit");
        }

        public void ChoosePokemonMenu(PokemonListResponse pokemonList)
        {
            Console.WriteLine("\n");
            Console.WriteLine("Choose a Pokemon from the list below:");

            for (int i = 0; i < pokemonList?.Results.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {pokemonList.Results[i].Name}");
            }
        }

        public void AdoptPokemonMenu(string username, string pokemonName)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"{username}, would you like to: ");
            Console.WriteLine($"1 - View information about {pokemonName}");
            Console.WriteLine($"2 - Adopt {pokemonName}");
            Console.WriteLine("3 - Return");
        }

        public void PokemonInformation(Pokemon pokemon, List<string> abilitiesNames)
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

        public void AdoptedPokemonMessage(string username, string pokemonName)
        {
            Console.WriteLine($"{username}, you have adopted {pokemonName} successfully!");
        }

        public void AdoptedPokemonList(List<AdoptedPokemon> pokemonList, string username)
        {
            for (int i = 0; i < pokemonList.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {pokemonList[i].Name}");
            }

            Console.WriteLine($"{username}, if you wish to interact with one of your pokemon, type its number.");
            Console.WriteLine("If you want to return, just type 0.");
        }

        public void InteractPokemonMenu(string username, string pokemonName)
        {
            Console.WriteLine("\n");
            Console.WriteLine($"{username}, would you like to: ");
            Console.WriteLine($"1 - Know how {pokemonName} is doing");
            Console.WriteLine($"2 - Feed {pokemonName}");
            Console.WriteLine($"3 - Play with {pokemonName}");
            Console.WriteLine($"4 - Get {pokemonName} some rest");
            Console.WriteLine("5 - Return");
        }

        public void AdoptedPokemonStatus(AdoptedPokemon pokemon)
        {
            Console.WriteLine(pokemon.GetSatietyStatus());
            Console.WriteLine(pokemon.GetEnergyStatus());
            Console.WriteLine(pokemon.GetMoodStatus());
        }

        public void FeedPokemon(string pokemonName)
        {
            Console.WriteLine($"You have fed {pokemonName}!");
        }

        public void PlayWithPokemon(string pokemonName)
        {
            Console.WriteLine($"You have played with {pokemonName}!");
        }

        public void RestPokemon(string pokemonName)
        {
            Console.WriteLine($"You have rested {pokemonName}!");
        }

        public void AdoptedPokemonEmpty()
        {
            Console.WriteLine("You have not adopted any pokemon yet! Make sure to adopt a pokemon so that you can interact with it.");
        }
    }
}
