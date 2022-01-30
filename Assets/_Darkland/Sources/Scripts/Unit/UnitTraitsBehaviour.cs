using _Darkland.Sources.Models.Unit.Traits;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {
    
    public class UnitTraitsBehaviour : MonoBehaviour {

        private IUnitTraitsHolder _unitTraitsHolder;

        private void Awake() {
            _unitTraitsHolder = new UnitTraitsHolder();
        }

        [Server]
        public void ServerChangeBasic(UnitTraitId traitId, int newValue) {
            _unitTraitsHolder.ChangeBasic(traitId, newValue);
        }

        [Server]
        public void ServerChangeBonus(UnitTraitId traitId, int newValue) {
            _unitTraitsHolder.ChangeBonus(traitId, newValue);
        }

        [Server]
        public UnitTraitsData ServerCurrent() => UnitTraitsData.Mapper.Map(_unitTraitsHolder);
    }
}