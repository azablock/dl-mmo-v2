using _Darkland.Sources.Models.Unit.Traits;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitStatsUpdateListener : NetworkBehaviour {

        private UnitStatsBehaviour _unitStatsBehaviour;
        private IUnitTraitsHolder _unitTraitsHolder;
        
        public override void OnStartServer() {
            _unitTraitsHolder.Changed += UnitTraitsHolderOnChanged;
        }

        private void UnitTraitsHolderOnChanged(UnitTraitId arg1, UnitTraitValue arg2) {
            _unitStatsBehaviour.ServerUpdateStats();
        }
    }

}