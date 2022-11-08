using MongoDB.Bson;

namespace _Darkland.Sources.Models {

    public struct DarklandAuthRequest {
        public bool isBot;
        public string accountName;
    }

    public struct DarklandAuthResponse {
        public bool success;
        public string message;
    }

}