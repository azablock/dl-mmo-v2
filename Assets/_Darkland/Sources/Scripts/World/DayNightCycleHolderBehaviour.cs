using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class DayNightCycleHolderBehaviour : MonoBehaviour {

        [SerializeField]
        private int fullDayLength; //65536 ->>> 54 sekundy
        
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