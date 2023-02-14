using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitEffectClientNotifierBehaviour : NetworkBehaviour, IUnitEffectClientNotifier {

        public List<string> ActiveEffectsNames => _activeEffectsNames.ToList();
        public event Action<string> ClientAdded;
        public event Action<string> ClientRemoved;
        public event Action ClientRemovedAll;
        public event Action<List<string>> ClientNotified;

        private IUnitEffectHolder _unitEffectHolder;

        private readonly SyncList<string> _activeEffectsNames = new();

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

        public override void OnStartClient() {
            _activeEffectsNames.Callback += ActiveEffectsNamesOnCallback;
        }

        public override void OnStopClient() {
            _activeEffectsNames.Callback -= ActiveEffectsNamesOnCallback;
        }

        private void ActiveEffectsNamesOnCallback(SyncList<string>.Operation op, int itemindex, string olditem, string newitem) {
            switch (op) {

                case SyncList<string>.Operation.OP_ADD:
                    break;
                case SyncList<string>.Operation.OP_CLEAR:
                    break;
                case SyncList<string>.Operation.OP_INSERT:
                    break;
                case SyncList<string>.Operation.OP_REMOVEAT:
                    break;
                case SyncList<string>.Operation.OP_SET:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(op), op, null);
            }
            
            ClientNotified?.Invoke(ActiveEffectsNames);
        }

        [Server]
        private void ServerOnAdded(IUnitEffect effect) {
            _activeEffectsNames.Add(effect.EffectName);
        }

        [Server]
        private void ServerOnRemoved(string effectName) {
            _activeEffectsNames.Remove(effectName);
        }

        [Server]
        private void ServerOnRemovedAll() {
            _activeEffectsNames.Clear();
        }
        
        // [TargetRpc]
        // private void TargetRpcEffectAdded(string effectName) => ClientAdded?.Invoke(effectName);
        //
        // [TargetRpc]
        // private void TargetRpcEffectRemoved(string effectName) => ClientRemoved?.Invoke(effectName);
        //
        // [TargetRpc]
        // private void TargetRpcEffectRemovedAll() => ClientRemovedAll?.Invoke();

    }

}