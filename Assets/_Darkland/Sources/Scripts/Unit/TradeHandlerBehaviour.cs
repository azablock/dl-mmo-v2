using _Darkland.Sources.Models.Equipment;
using Mirror;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Unit {

    public interface ITradeHandler {

        void BuyItem(IEqItemDef item);
        void SellItem(int backpackSlot);

    }
    
    public class TradeHandlerBehaviour : NetworkBehaviour, ITradeHandler {

        private IEqHolder _eqHolder;
        private IGoldHolder _goldHolder;

        private void Awake() {
            _eqHolder = GetComponent<IEqHolder>();
            _goldHolder = GetComponent<IGoldHolder>();
        }

        [Server]
        public void BuyItem(IEqItemDef item) {
            if (_eqHolder.ServerBackpackFull()) return;
            if (_goldHolder.GoldAmount - item.ItemPrice < 0) return;
    
            _goldHolder.ServerSubtractGold(item.ItemPrice);
            _eqHolder.AddToBackpack(item);
        }

        [Server]
        public void SellItem(int backpackSlot) {
            var item = _eqHolder.ServerBackpackItem(backpackSlot);
            Assert.IsNotNull(item);
            
            _goldHolder.ServerAddGold(item.ItemPrice);
            _eqHolder.RemoveFromBackpack(backpackSlot);
        }

    }

}