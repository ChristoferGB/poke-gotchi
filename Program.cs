using poke_gotchi.Controller;

namespace poke_gotchi
{
    public class Program
    {
        public static void Main()
        {
            var controller = new PokegotchiController();
            controller.Play();
        }
    }
}