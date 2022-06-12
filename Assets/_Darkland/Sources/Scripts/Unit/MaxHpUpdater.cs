using System;
using _Darkland.Sources.Models.Unit.Stats;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public class MaxHpUpdater : NetworkBehaviour {

        private UnitStatsBehaviour _unitStatsBehaviour;


        private HpBehaviour _hpBehaviour;
        
        private Action<GameObject, UnitStats> a;
        
        
        public override void OnStartServer() {
            _unitStatsBehaviour.Changed += UnitStatsBehaviourOnChanged;


            a = (go, stats) => {
                

            };
        }

        private void UnitStatsBehaviourOnChanged(UnitStats unitStats) {
            _hpBehaviour.ServerChangeMaxHp(unitStats.maxHp);
        }
    }

}