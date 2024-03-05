namespace poke_gotchi.Model
{
    public class Ability
    {
        public string Name { get; set; }

        public Ability(string name)
        {
            Name = name;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
