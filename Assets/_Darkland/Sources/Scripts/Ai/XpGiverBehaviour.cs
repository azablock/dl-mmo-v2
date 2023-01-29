using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    //todo nazwa...
    public class XpGiverBehaviour : MonoBehaviour {

        public int xp;

        [Server]
        public void ServerGiveXpToHero(DarklandHeroBehaviour heroBehaviour) {
            heroBehaviour.GetComponent<XpHolderBehaviour>().ServerGain(xp);
        }
    }

}