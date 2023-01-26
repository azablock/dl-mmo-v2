using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public interface IUnitConditionEffect {

        string effectName { get; }

    }
    
    // public interface 


    public class UnitConditionEffectHolderBehaviour : NetworkBehaviour, IUnitConditionEffectHolder {

        private List<IUnitConditionEffect> _effects;

        public event Action<IUnitConditionEffect> ServerEffectAdded;
        public event Action<IUnitConditionEffect> ServerEffectRemoved;
        public event Action<string> ClientEffectAdded;
        public event Action<string> ClientEffectRemoved;

        public override void OnStartServer() {
            _effects = new List<IUnitConditionEffect>();
        }

        [Server]
        public void Add(IUnitConditionEffect effect) {
            //todo asserts
            _effects.Add(effect);
        }


        [Server]
        public void Remove(IUnitConditionEffect effect) {
            //todo asserts
            _effects.Remove(effect);
        }

        [TargetRpc]
        private void TargetRpcEffectAdded(string effectName) {
            ClientEffectAdded?.Invoke(effectName);
        }

        [TargetRpc]
        private void TargetRpcEffectRemoved(string effectName) {
            ClientEffectRemoved?.Invoke(effectName);
        }

    }

}