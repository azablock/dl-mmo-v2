using Mirror;

namespace _Darkland.Sources.Models.Chat {

    public static class ChatMessagesFormatter {

        public static string FormatServerLog(string message) {
            var networkTimeStr = $"{NetworkTime.time:0.0000}";
            return $"[Server] ({networkTimeStr}): {message}";
        } 
    }

}