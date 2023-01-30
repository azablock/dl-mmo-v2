using System;
using Mirror;
using UnityEngine.Assertions;

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
    
    public class GoldHolderBehaviour : NetworkBehaviour, IGoldHolder {

        public int GoldAmount { get; private set; }
        public event Action<int> ClientGoldAmountChanged;

        [Server]
        public void Set(int val) {
            Assert.IsTrue(val >= 0);
            GoldAmount = val;
            TargetRpcGoldAmountChanged(GoldAmount);
        }
        
        [TargetRpc]
        private void TargetRpcGoldAmountChanged(int goldAmount) => ClientGoldAmountChanged?.Invoke(goldAmount);

    }

}