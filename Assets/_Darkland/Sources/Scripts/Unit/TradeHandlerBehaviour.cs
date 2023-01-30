using _Darkland.Sources.Models.Equipment;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Unit {

    public interface ITradeHandler {

        void BuyItem(IEqItemDef item);
        void SellItem(int backpackSlot);
        
    }
    
    public class TradeHandlerBehaviour : MonoBehaviour, ITradeHandler {

        private IEqHolder _eqHolder;

        private void Awake() {
            _eqHolder = GetComponent<IEqHolder>();
        }

        [Server]
        public void BuyItem(IEqItemDef item) {
            if (_eqHolder.ServerBackpackFull()) return;
            //todo handle "has enough gold"
    
            _eqHolder.AddToBackpack(item);
        }

        [Server]
        public void SellItem(int backpackSlot) {
            Assert.IsNotNull(_eqHolder.ServerBackpackItem(backpackSlot));
            //todo add gold
            
            _eqHolder.RemoveFromBackpack(backpackSlot);
        }

    }

}