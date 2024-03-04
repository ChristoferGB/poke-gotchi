﻿namespace poke_gotchi.Entities
{
    public class AbilitySetting
    {
        public Ability Ability { get; set; }

        public AbilitySetting(Ability ability)
        {
            Ability = ability;
        }

        public void SetAbility(Ability ability)
        {
            Ability = ability;
        }
    }
}
