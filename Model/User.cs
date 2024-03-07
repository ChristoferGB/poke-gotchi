namespace poke_gotchi.Model
{
    public class User
    {
        public string Name { get; set; } = "";
        public List<AdoptedPokemon> AdoptedPokemon { get; set; } = new List<AdoptedPokemon>();

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetAdoptedPokemon(List<AdoptedPokemon> adoptedPokemon)
        {
            AdoptedPokemon = adoptedPokemon;
        }

        public void AdoptPokemon(AdoptedPokemon pokemon)
        {
            AdoptedPokemon.Add(pokemon);
        }
    }
}
