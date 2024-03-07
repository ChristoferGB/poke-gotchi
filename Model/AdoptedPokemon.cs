namespace poke_gotchi.Model
{
    public class AdoptedPokemon : Pokemon
    {
        private int satiety;
        private int mood;
        private int energy;

        public int Satiety { get => satiety; private set => satiety = value; }
        public int Mood { get => mood; private set => mood = value; }
        public int Energy { get => energy; private set => energy = value; }

        public AdoptedPokemon(string name, int height, int weight, List<AbilitySetting> abilities) : base(name, height, weight, abilities)
        {
            Satiety = new Random().Next(0, 11);
            Mood = 5;
            Energy = 5;
        }

        public string GetSatietyStatus()
        {
            return Satiety > 7 ? $"{Name} ate too much!" :
                Satiety <= 7 && Satiety >= 4 ? $"{Name} is not hungry" :
                $"{Name} is hungry";
        }

        public string GetEnergyStatus()
        {
            return Energy > 7 ? $"{Name} is filled with energy" :
                Energy <= 7 && Energy >= 4 ? $"{Name} is on normal energy level" :
                $"{Name} is tired and needs to sleep";
        }

        public string GetMoodStatus()
        {
            return Mood > 7 ? $"{Name} is super happy" :
                Mood <= 7 && Mood >= 4 ? $"{Name} is doing okay" :
                $"{Name} is feeling sad";
        }

        public void Feed()
        {
            satiety++;
            energy--;
        }

        public void Play()
        {
            mood++;
            satiety--;
        }

        public void Rest()
        {
            energy++;
            mood--;
        }
    }
}
