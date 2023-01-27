using System;
using _Darkland.Sources.Models.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitEffectClientNotifierBehaviour : NetworkBehaviour, IUnitEffectClientNotifier {

        public event Action<string> ClientAdded;
        public event Action<string> ClientRemoved;
        public event Action ClientRemovedAll;
        
        private IUnitEffectHolder _unitEffectHolder;

        public override void OnStartServer() {
            _unitEffectHolder = GetComponent<IUnitEffectHolder>();
            
            _unitEffectHolder.ServerAdded += ServerOnAdded;
            _unitEffectHolder.ServerRemoved += ServerOnRemoved;
            _unitEffectHolder.ServerRemovedAll += ServerOnRemovedAll;
        }

        public override void OnStopServer() {
            _unitEffectHolder.ServerAdded -= ServerOnAdded;
            _unitEffectHolder.ServerRemoved -= ServerOnRemoved;
            _unitEffectHolder.ServerRemovedAll -= ServerOnRemovedAll;
        }

        [Server]
        private void ServerOnAdded(IUnitEffect effect) => TargetRpcEffectAdded(effect.EffectName);

        [Server]
        private void ServerOnRemoved(string effectName) => TargetRpcEffectRemoved(effectName);

        [Server]
        private void ServerOnRemovedAll() => TargetRpcEffectRemovedAll();
        
        [TargetRpc]
        private void TargetRpcEffectAdded(string effectName) => ClientAdded?.Invoke(effectName);

        [TargetRpc]
        private void TargetRpcEffectRemoved(string effectName) => ClientRemoved?.Invoke(effectName);

        [TargetRpc]
        private void TargetRpcEffectRemovedAll() => ClientRemovedAll?.Invoke();

    }

}