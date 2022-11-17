using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public static class ChatMessages {
        
        public struct ChatMessageRequestMessage : NetworkMessage {
            public string message;
        }

        public struct ChatMessageResponseMessage : NetworkMessage {
            public string message;
            public string heroName;
        }
    }

}