using _Darkland.Sources.Scripts.Unit;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.RegainStrategy {

    public class HpRegainFromStatsRegainStrategy : RegainStrategy {

        public override float Get(GameObject go) {
            return go.GetComponent<UnitStatsBehaviour>().ServerGet().hpRegain;
        }
    }

}