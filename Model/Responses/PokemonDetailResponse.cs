namespace poke_gotchi.Model.Responses
{
    public class PokemonDetailResponse
    {
        private string name;

        public string Name { get => name.ToUpper(); set => name = value; }
        public string Url { get; set; }

        public PokemonDetailResponse(string name, string url)
        {
            Name = name;
            Url = url;
        }
    }
}
