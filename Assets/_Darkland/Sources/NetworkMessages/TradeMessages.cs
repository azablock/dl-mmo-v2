using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public static class TradeMessages {

        public struct BuyItemRequestMessage : NetworkMessage {
            public string itemName;
        }
        
        public struct SellItemRequestMessage : NetworkMessage {
            public int backpackSlot;
        }

    }

}