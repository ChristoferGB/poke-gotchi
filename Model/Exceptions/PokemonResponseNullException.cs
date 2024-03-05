namespace poke_gotchi.Model.Exceptions
{
    public class PokemonResponseNullException : Exception
    {
        public PokemonResponseNullException() 
            : base("The list of Pokemon returned by the API was null. Try again in a few moments.") { }
    }
}
