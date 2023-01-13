using _Darkland.Sources.Models.Interaction;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay {

    public class TargetNetIdentityPanel : MonoBehaviour {

        [SerializeField]
        private TMP_Text targetNameText;

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientChanged += ClientOnTargetNetIdentityChanged;
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientChanged -= ClientOnTargetNetIdentityChanged;
        }

        [Client]
        private void ClientOnTargetNetIdentityChanged(NetworkIdentity targetNetIdentity) {
            targetNameText.text = $"{targetNetIdentity.gameObject.name} [netId={targetNetIdentity.netId}]";
        }
    }

}