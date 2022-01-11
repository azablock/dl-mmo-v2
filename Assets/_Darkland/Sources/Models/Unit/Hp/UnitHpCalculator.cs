using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public static class UnitHpCalculator {
        public static int CalculateHp(IUnitHpHolder holder, int hpDelta) {
            var newHp = holder.hp + hpDelta;
            
            return newHp < 0 ? 0 : Math.Min(newHp, holder.maxHp);
        }
        
        public static int CalculateMaxHp(IUnitHpHolder holder, int maxHpDelta) {
            return Math.Max(1, holder.maxHp + maxHpDelta);
        }
    }
}