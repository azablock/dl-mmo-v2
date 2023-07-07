using _Darkland.Sources.Scripts.Presentation;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Core {

    public static class RichTextFormatter {

        public static string FormatServerLog(string message) {
            var networkTimeStr = $"{NetworkTime.time:0.000}";
            var serverPrefix = Colored("[Server]", DarklandColorSet._.info);

            return $"{serverPrefix} ({networkTimeStr}):\t{message}";
        }

        public static string FormatChatMessage(string heroName, string message, bool isLocalPlayer) {
            var heroNameColor = isLocalPlayer ? DarklandColorSet._.success : DarklandColorSet._.light;
            return $"{Colored(heroName, heroNameColor)}: {message}";
        }

        public static string Colored(string val, Color color) => $"<color={HtmlRgba(color)}>{val}</color>";
        public static string Bold(string val) => $"<b>{val}</b>";
        public static string Size(string val, int size) => $"<size={size}>{val}</size>";
        
        private static string HtmlRgba(Color color) => $"#{ColorUtility.ToHtmlStringRGBA(color)}";
    }

}