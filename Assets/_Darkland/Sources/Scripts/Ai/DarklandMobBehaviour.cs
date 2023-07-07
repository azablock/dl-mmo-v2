using _Darkland.Sources.Models.Core;
using _Darkland.Sources.ScriptableObjects.Mob;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Ai {

    public interface IMobDefHolder {

        IMobDef MobDef { get; }

    }

    public class DarklandMobBehaviour : NetworkBehaviour, IMobDefHolder {

        [SerializeField]
        private MobDef mobDef;
        private IDiscretePosition _discretePosition;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();

            Assert.IsTrue(mobDef.MinDamage <= mobDef.MaxDamage);
            Assert.IsTrue(mobDef.AttackPerceptionRange <= mobDef.PassivePerceptionRange);
            Assert.IsTrue(mobDef.MinGoldGain <= mobDef.MaxGoldGain);
        }

        // public override void OnStartServer() {
        // _discretePosition.Set(Vector3Int.FloorToInt(transform.position));
        // }

        public IMobDef MobDef => mobDef;

    }

}