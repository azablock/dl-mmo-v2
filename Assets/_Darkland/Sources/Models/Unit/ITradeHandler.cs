using _Darkland.Sources.Models.Equipment;

namespace _Darkland.Sources.Scripts.Unit {

    public interface ITradeHandler {

        void BuyItem(IEqItemDef item);
        void SellItem(int backpackSlot);

    }

}