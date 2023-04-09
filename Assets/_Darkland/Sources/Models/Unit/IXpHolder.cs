using System;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit {

    public interface IXpHolder {
        int xp { get; }
        int level { get; }
        event Action<int> ServerLevelChanged;
        event Action<int> ClientXpChanged;
        event Action<ExperienceLevelChangeEvent> ClientLevelChanged;
        
        public static int LevelXpCap(int lvl) => lvl == 1 ? 0
            : (int)(Mathf.Pow(lvl, 3) + 60 * Mathf.Pow(lvl, 2) + 100 * Mathf.Pow(lvl, 1) + 140);
    }

    public static class XpHolderExtensions {

        public static ExperienceLevelChangeEvent LevelEvt(this IXpHolder holder) => new() {
            nextLevelXpCap = IXpHolder.LevelXpCap(holder.level + 1),
            currentLevelXpCap = IXpHolder.LevelXpCap(holder.level),
            level = holder.level,
            currentXp = holder.xp
        };
        
    }

}