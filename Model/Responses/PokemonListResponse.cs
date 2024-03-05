namespace poke_gotchi.Model.Responses
{
    public class PokemonListResponse
    {
        public int Count { get; set; }
        public List<PokemonDetailResponse> Results { get; set; }

        public PokemonListResponse(int count, List<PokemonDetailResponse> results)
        {
            Count = count;
            Results = results;
        }
    }
}
