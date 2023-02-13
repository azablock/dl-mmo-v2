using _Darkland.Sources.Models.GameReport;
using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public static class ChatMessages {
        
        public struct ChatMessageRequestMessage : NetworkMessage {
            public string message;
        }

        public struct ChatMessageResponseMessage : NetworkMessage {
            public string message;
            public string heroName;
            public uint senderNetId;
        }

        public struct ServerLogResponseMessage : NetworkMessage {
            public string message;
        }
        
        //todo create new static class
        public struct GameReportRequestMessage : NetworkMessage {
            public string title;
            public string content;
            public GameReportType gameReportType;
        }
    }

}