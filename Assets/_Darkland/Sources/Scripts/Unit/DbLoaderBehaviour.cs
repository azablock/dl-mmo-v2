using System;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    //todo tmp
    public class DbLoaderBehaviour : NetworkBehaviour {

        private HpBehaviour _hpBehaviour;

        private void Awake() {
            _hpBehaviour = GetComponent<HpBehaviour>();
        }

        // public override void OnStartServer() {
        //
        // }

        public override void OnStartLocalPlayer() {
            _hpBehaviour.ClientHpChanged += HpBehaviourOnClientHpChanged;
            CmdLoad();
        }

        [Command]
        private void CmdLoad() {
            _hpBehaviour.ServerChangeMaxHp(10);
            _hpBehaviour.ServerRegainHpToMaxHp();
        }

        [Client]
        private void HpBehaviourOnClientHpChanged(int obj) {
            Debug.Log("[Client] local player: received hp " + obj, this);
        }
    }

}