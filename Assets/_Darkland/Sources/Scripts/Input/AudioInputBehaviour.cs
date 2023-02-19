using System;
using _Darkland.Sources.Scripts.Audio;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class AudioInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction toggleAmbient;

        [SerializeField]
        private InputAction toggleMusic;

        private void OnEnable() {
            DarklandHeroBehaviour.LocalHeroStarted += Connect;
            DarklandHeroBehaviour.LocalHeroStopped += Disconnect;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.LocalHeroStarted -= Connect;
            DarklandHeroBehaviour.LocalHeroStopped -= Disconnect;
        }
        
        private void Connect() {
            toggleAmbient.performed += ClientToggleAmbient;
            toggleAmbient.Enable();
            
            toggleMusic.performed += ClientToggleMusic;
            toggleMusic.Enable();
            
            //todo tmp
            // audioRootManager.Toggle(audioRootManager.ambient);
            // audioRootManager.Toggle(audioRootManager.music);
        }

        private void Disconnect() {
            toggleAmbient.performed -= ClientToggleAmbient;
            toggleAmbient.Disable(); 
            
            toggleMusic.performed -= ClientToggleMusic;
            toggleMusic.Disable();
        }

        [Client]
        private void ClientToggleAmbient(InputAction.CallbackContext _) {
            audioRootManager.Toggle(audioRootManager.ambient);
        }
        
        [Client]
        private void ClientToggleMusic(InputAction.CallbackContext _) {
            audioRootManager.Toggle(audioRootManager.music);
        }


        private AudioRootManager audioRootManager => FindObjectOfType<AudioRootManager>();

    }

}