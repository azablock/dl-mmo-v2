using System.Collections.Generic;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.ScriptableObjects.Hero;
using UnityEngine;

namespace _Darkland.Sources.Models.Hero {

    public interface IHeroVocation {

        HeroVocationType VocationType { get; }
        List<ISpell> AvailableSpells { get; }
        IHeroVocationStartingStats StartingStats { get; }
        LevelTraitDistribution LevelTraitDistribution { get; }
        Sprite VocationSprite { get; }

    }

}