﻿namespace poke_gotchi.Model
{
    public class Pokemon
    {
        private string name;

        public int Id { get; set; }
        public string Name { get => name.ToUpper(); set => name = value; }
        public int Height { get; set; }
        public int Weight { get; set; }
        
        public List<AbilitySetting> Abilities { get; set; }

        public Pokemon(string name, int height, int weight, List<AbilitySetting> abilities)
        {
            Name = name;
            Height = height;
            Weight = weight;
            Abilities = abilities;
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
    }
}
