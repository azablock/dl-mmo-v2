using _Darkland.Sources.Models.Core;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class LocalPlayerProximityCheckerBehaviour : MonoBehaviour {
        
        [SerializeField]
        private float maxVisibleDistance;
        [SerializeField]
        private GameObject toggleTarget;

        //todo sub to discretePosition change (local player)
        private void FixedUpdate() {
            if (DarklandHeroBehaviour.localHero == null) return;
            var localPlayerPos = DarklandHeroBehaviour.localHero.GetComponent<IDiscretePosition>().Pos;

            if (!LocalPlayerInProximity(localPlayerPos)) Hide();
        }

        public void Toggle() {
            if (toggleTarget.activeSelf) Hide();
            else Show();
        }

        private void Show() {
            if (DarklandHeroBehaviour.localHero == null) return;
            var localPlayerPos = DarklandHeroBehaviour.localHero.GetComponent<IDiscretePosition>().Pos;
            
            if (!LocalPlayerInProximity(localPlayerPos)) return;
            
            toggleTarget.SetActive(true);
        }

        private void Hide() => toggleTarget.SetActive(false);

        private bool LocalPlayerInProximity(Vector3Int localPlayerPos) {
            var transformPosition = transform.position;
            var inDistance = Vector3.Distance(localPlayerPos, transformPosition) < maxVisibleDistance;
            var zEqual = (int) transformPosition.z == localPlayerPos.z;
            
            return inDistance && zEqual;
        }


    }

}