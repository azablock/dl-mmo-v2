using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class DayNightCycleHolderBehaviour : MonoBehaviour {

        [SerializeField]
        [Tooltip("65536 ->>> ok. 54 sekundy")]
        private int fullDayLength;
        
        [Client]
        public void ServerSet(int val) {
            partOfDay = val;
        }

        public int partOfDay { get; private set; }

        private void FixedUpdate() {
            partOfDay++;
            partOfDay %= fullDayLength;
        }

        public int FullDayLength => fullDayLength;

    }

}