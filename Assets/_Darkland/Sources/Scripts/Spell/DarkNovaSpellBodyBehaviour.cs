using System;
using System.Collections;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell {

    public class DarkNovaSpellBodyBehaviour : MonoBehaviour {

        [SerializeField]
        private BoxCollider boxCollider;
        public Action<NetworkIdentity> _onTriggerCallback;

        [Server]
        public void ServerInit(float radius, Action<NetworkIdentity> onTriggerCallback) {
            _onTriggerCallback = onTriggerCallback;
            
            boxCollider.size = new Vector3(radius, radius, 1);
            boxCollider.gameObject.SetActive(true);

            StartCoroutine(ServerProcessDarkNova());
        }

        [Server]
        private IEnumerator ServerProcessDarkNova() {
            yield return new WaitForSeconds(0.25f);
            Destroy(gameObject);
        }
        
    }

}