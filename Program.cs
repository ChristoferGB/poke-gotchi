using RestSharp;

namespace poke_gotchi;

static class Program
{
    [STAThread]
    static void Main()
    {
        var client = new RestClient("https://pokeapi.co/api/v2/pokemon/");
        var request = new RestRequest("", Method.Get);

        var response = client.Get(request).Content;

        Console.WriteLine(response);

        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }    
}