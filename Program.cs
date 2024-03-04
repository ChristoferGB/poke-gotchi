using poke_gotchi.Entities;
using RestSharp;
using System.Text.Json;

static class Program
{
    static void Main()
    {
        var client = new RestClient("https://pokeapi.co/api/v2/pokemon/");
        var request = new RestRequest("1", Method.Get);

        var response = client.Get(request).Content;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        Pokemon? pokemon = response == null ? 
            null : 
            JsonSerializer.Deserialize<Pokemon>(response, options);

        List<string>? abilitiesNames = pokemon == null ? 
            new List<string>() :
            pokemon.Abilities.Select(x => x.Ability.Name).ToList();

        Console.WriteLine($"Name: {pokemon?.Name}");
        Console.WriteLine($"Height: {pokemon?.Height}");
        Console.WriteLine($"Nome: {pokemon?.Weight}");
        Console.WriteLine("Abilities: ");

        foreach (var abilityName in abilitiesNames)
        {
            Console.WriteLine(abilityName);
        }
    }
}