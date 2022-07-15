using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Bot {

    public class DarklandBotManager : MonoBehaviour {

        [SerializeField]
        private GameObject darklandBotPrefab;
        
        //todo should be as Set
        private List<uint> _botNetIds;

        private void Awake() {
            _botNetIds = new List<uint>();
            // NetworkManager.singleton.spawnPrefabs.Add(darklandBotPrefab);
        }
        
        
        

        [Server]
        public void ServerSpawnBot() {
            // var botGameObject = Instantiate(darklandBotPrefab);
            // NetworkServer.Spawn(botGameObject);
            // _botNetIds.Add(botGameObject.GetComponent<NetworkIdentity>().netId);
        }

        [Server]
        public void ServerUnSpawnBot() {
            // if (_botNetIds.Count == 0) return; 
            // var randomBotNetId = Random.Range(0, _botNetIds.Count - 1);
            // var botNetId = _botNetIds[randomBotNetId];

            var bots = FindObjectsOfType<DarklandBot>();
            
            if (bots.Length == 0) return;

            NetworkServer.Destroy(bots.First().gameObject);
            
            // NetworkServer.RemovePlayerForConnection(botNetworkIdentity.connectionToServer, true);
            
            // NetworkServer.UnSpawn(botNetworkIdentity.gameObject);
        }
    }

}