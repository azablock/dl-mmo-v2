using System;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell {

    public class CircleOfLightSpellBodyBehaviour : MonoBehaviour {

        [SerializeField]
        private BoxCollider boxCollider;
        public Action<NetworkIdentity> _onTriggerEnterCallback;
        public Action<NetworkIdentity> _onTriggerExitCallback;

        [Server]
        public void ServerInit(float radius,
                               Action<NetworkIdentity> onTriggerEnterCallback,
                               Action<NetworkIdentity> onTriggerExitCallback) {
            _onTriggerEnterCallback = onTriggerEnterCallback;
            _onTriggerExitCallback = onTriggerExitCallback;

            boxCollider.size = new Vector3(radius, radius, 1);
            boxCollider.gameObject.SetActive(true);

            // StartCoroutine(ServerProcessDarkNova());
        }

        // [Server]
        // private IEnumerator ServerProcessDarkNova() {
        //     yield return new WaitForSeconds(0.1f);
        //     Destroy(gameObject);
        // }

    }

}