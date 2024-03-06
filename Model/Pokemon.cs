namespace poke_gotchi.Model
{
    public class Pokemon
    {
        private int satiety;
        private int mood;
        private int energy;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int Satiety { get => satiety; private set => satiety = value; }
        public int Mood { get => mood; private set => mood = value; }
        public int Energy { get => energy; private set => energy = value; }
        public List<AbilitySetting> Abilities { get; set; }

        public Pokemon(string name, int height, int weight, List<AbilitySetting> abilities)
        {
            Name = name;
            Height = height;
            Weight = weight;
            Abilities = abilities;
            Satiety = new Random().Next(0, 11);
            Mood = 5;
            Energy = 5;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetHeight(int height)
        {
            Height = height;
        }

        public void SetWeight(int weight)
        {
            Weight = weight;
        }

        public void SetAbilities(List<AbilitySetting> abilities)
        {
            Abilities = abilities;
        }

        public string GetSatietyStatus()
        {
            return Satiety > 7 ? $"{Name} is satisfied" : 
                Satiety <= 7 && Satiety >= 4 ? $"{Name} is okay" :
                $"{Name} is hungry";
        }

        public string GetEnergyStatus()
        {
            return Energy > 7 ? $"{Name} is filled with energy" :
                Energy <= 7 && Energy >= 4 ? $"{Name} is okay" :
                $"{Name} is tired and needs to sleep";
        }

        public string GetMoodStatus()
        {
            return Mood > 7 ? $"{Name} is super happy" :
                Mood <= 7 && Mood >= 4 ? $"{Name} is okay" :
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
