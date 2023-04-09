using System;

namespace _Darkland.Sources.Scripts.Unit {

    public interface IGoldHolder {

        void Set(int val);
        int GoldAmount { get; }
        event Action<int> ClientGoldAmountChanged;

    }

    public static class GoldHolderExtensions {

        public static void ServerAddGold(this IGoldHolder goldHolder, int val) {
            goldHolder.Set(goldHolder.GoldAmount + val);
        }

        public static void ServerSubtractGold(this IGoldHolder goldHolder, int val) {
            goldHolder.Set(goldHolder.GoldAmount - val);
        }

    }

}