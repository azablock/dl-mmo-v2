namespace _Darkland.Sources.Models.Hero {

    public interface IHeroSheet {

        /**
         * action power, -> dmg spells, healing spells, mana regen spells, defensive spells
         * max hp,
         */
        public int might { get; }
        
        /**
         * max hp,
         * health regen,
         * physical resistance (wytrzymalosc na ciosy "normalne" niemagiczne)
         */
        public int constitution { get; }
        
        /**
         * action speed,
         * action success rate (is hit, is successful heal),
         * block chance
         * action crit chance,
         * movement speed
         * 
         */
        public int dexterity { get; }

        /**
         * max mana,
         * mana regen,
         * action crit chance,
         * 
         */
        public int intellect { get; }
        
        /**
         * mana regen,
         * magic resistance,
         * action success rate (is hit, is successful heal),
         * action range (action area of effect)
         */
        public int soul { get; }
    }

}