using _Darkland.Sources.Scripts.Ai;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell {

    public class DarkNovaTrigger : MonoBehaviour {

        [ServerCallback]
        private void OnTriggerEnter(Collider other) {
            if (!other.GetComponent<DarklandMobBehaviour>()) return;

            GetComponentInParent<DarkNovaSpellBodyBehaviour>()._onTriggerCallback(other.GetComponent<NetworkIdentity>());
        }

    }

}