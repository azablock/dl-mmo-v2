using System;
using UnityEngine;
using Mirror;

namespace _Darkland.Sources.Scripts.Terminal {

    public class TerminalInputHandler : MonoBehaviour {
#if UNITY_SERVER && !UNITY_EDITOR_64
        private string inputBuffer = "";

        private void Update() {
            GetInput();
        }

        private void GetInput() {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            switch (consoleKeyInfo.Key) {
                case ConsoleKey.Backspace:
                    if (inputBuffer.Length > 0) {
                        // Delete the character (backspace + space + backspace)
                        Console.Write("\b \b");
                        inputBuffer = inputBuffer.Substring(0, inputBuffer.Length - 1);
                    }

                    break;
                case ConsoleKey.Enter:
                    if (inputBuffer.Length > 0) {
                        Console.WriteLine();
                        inputBuffer = inputBuffer.Trim();
                        ServerProcessInput();
                        inputBuffer = "";
                    }

                    break;
                default:
                    // Restrict to ASCII printable characters by matching range from space through tilde
                    if (System.Text.RegularExpressions.Regex.IsMatch(consoleKeyInfo.KeyChar.ToString(), @"[ -~]")) {
                        //remove if mac

#if UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
//Console.ReadKey(true); //is also a potential fix ? ^^
#else
                        Console.Write(consoleKeyInfo.KeyChar);

#endif
                        inputBuffer += consoleKeyInfo.KeyChar;
                    }

                    break;
            }
        }

        // [Server] todo? 
        private void ServerProcessInput() {
            //Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("You entered {0}", inputBuffer);
            //Console.ResetColor();

            if (inputBuffer == "hello") {
                Console.WriteLine("\n\nHELLO FROM CONSOLE\n\n");
            }

            if (inputBuffer == "spawn bot") {
                DarklandNetworkManager.self.DarklandBotManager.ServerSpawnBot();
            }

            if (inputBuffer == "unspawn bot") {
                DarklandNetworkManager.self.DarklandBotManager.ServerSpawnBot();
            }
            
            if (inputBuffer == "stop") {
                Debug.Log("ProcessInput stop");
                if (NetworkManager.singleton) {
                    NetworkManager.singleton.StopServer();
                    Application.Quit();
                }
            }
        }
#endif
    }

}