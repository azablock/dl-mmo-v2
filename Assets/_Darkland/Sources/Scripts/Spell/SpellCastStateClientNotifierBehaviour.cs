using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;

namespace _Darkland.Sources.Scripts.Spell {

    public interface ISpellCastStateClientNotifier {

        event Action<List<bool>> ClientNotified;

    }

    public class SpellCastStateClientNotifierBehaviour : NetworkBehaviour, ISpellCastStateClientNotifier {

        private ITargetNetIdHolder _targetNetIdHolder;
        private ISpellCaster _spellCaster;
        private IDiscretePosition _discretePosition;
        private IStatsHolder _statsHolder;
        private Stat _manaStat;

        public event Action<List<bool>> ClientNotified;

        private void Awake() {
            _targetNetIdHolder = GetComponent<ITargetNetIdHolder>();
            _spellCaster = GetComponent<ISpellCaster>();
            _discretePosition = GetComponent<IDiscretePosition>();
            _statsHolder = GetComponent<IStatsHolder>();
        }

        public override void OnStartServer() {
            _targetNetIdHolder.ServerChanged += ServerTargetChanged;
            _targetNetIdHolder.ServerCleared += ServerTargetCleared;
            _discretePosition.Changed += ServerOnCasterPosChanged;
            _manaStat = _statsHolder.Stat(StatId.Mana);
            
            //todo (1) jakis dziwny bug...
            // _statsHolder.Stat(StatId.Mana).Changed += ServerOnManaChanged;
        }
        
        public override void OnStopServer() {
            _targetNetIdHolder.ServerChanged -= ServerTargetChanged;
            _targetNetIdHolder.ServerCleared -= ServerTargetCleared;
            _discretePosition.Changed -= ServerOnCasterPosChanged;
            _manaStat.Changed -= ServerOnManaChanged;
        }

        //todo (1) jakis dziwny bug...
        public override void OnStartLocalPlayer() => CmdConnectToManaChange();

        [Command]
        private void CmdConnectToManaChange() {
            _manaStat.Changed += ServerOnManaChanged;
        }

        [Server]
        private void ServerTargetChanged(NetworkIdentity target) {
            target.GetComponent<IDiscretePosition>().Changed += ServerOnTargetPosChanged;
        }

        [Server]
        private void ServerTargetCleared(NetworkIdentity target) {
            target.GetComponent<IDiscretePosition>().Changed -= ServerOnTargetPosChanged;
            ServerNotify();
        }

        [Server]
        private void ServerOnCasterPosChanged(PositionChangeData _) => ServerNotify();

        [Server]
        private void ServerOnTargetPosChanged(PositionChangeData _) => ServerNotify();

        [Server]
        private void ServerOnManaChanged(StatVal _) {
            ServerNotify();
        }

        [Server]
        private void ServerNotify() {
            var states = _spellCaster
                .AvailableSpells
                .Select(spell => spell.CastConditions.All(it => it.CanCast(gameObject, spell)))
                .ToList();

            TargetRpcClientNotify(states);
        }

        [TargetRpc]
        private void TargetRpcClientNotify(List<bool> canCastFlags) => ClientNotified?.Invoke(canCastFlags);
        
    }

}