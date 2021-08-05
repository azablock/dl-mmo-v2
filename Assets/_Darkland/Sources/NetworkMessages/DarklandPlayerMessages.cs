using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public static class DarklandPlayerMessages {

        public struct ActionRequestMessage : NetworkMessage {
            public uint darklandPlayerNetId;
        }

        public struct ActionResponseMessage : NetworkMessage {
            public uint darklandPlayerNetId;
            public int sentActionRequestMessagesCount;
        }
    }

}