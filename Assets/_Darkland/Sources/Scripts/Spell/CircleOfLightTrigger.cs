using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell {

    public class CircleOfLightTrigger : MonoBehaviour {

        [ServerCallback]
        private void OnTriggerEnter(Collider other) {
            if (!other.GetComponent<DarklandHeroBehaviour>()) return;

            GetComponentInParent<CircleOfLightSpellBodyBehaviour>()
                ._onTriggerEnterCallback(other.GetComponent<NetworkIdentity>());
        }

        [ServerCallback]
        private void OnTriggerExit(Collider other) {
            if (!other.GetComponent<DarklandHeroBehaviour>()) return;

            GetComponentInParent<CircleOfLightSpellBodyBehaviour>()
                ._onTriggerExitCallback(other.GetComponent<NetworkIdentity>());
        }

    }

}