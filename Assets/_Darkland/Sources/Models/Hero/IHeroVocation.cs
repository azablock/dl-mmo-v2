using System.Collections.Generic;
using _Darkland.Sources.Models.Spell;

namespace _Darkland.Sources.Models.Hero {

    public interface IHeroVocation {

        HeroVocationType VocationType { get; }
        List<ISpell> AvailableSpells { get; }

    }

}