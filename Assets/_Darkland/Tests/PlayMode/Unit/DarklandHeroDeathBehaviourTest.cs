using System.Collections;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.DiscretePosition;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Sources.Scripts.Unit.Stats2;
using _Darkland.Tests.Common;
using Mirror;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode.Unit {

    [TestFixture]
    public class DarklandHeroDeathBehaviourTest : MirrorPlayModeTest {

        private SimpleStatsHolder _statsHolder;
        private GameObject _go;

        [UnitySetUp]
        public override IEnumerator UnitySetUp() {
            yield return base.UnitySetUp();
        
            NetworkServer.Listen(1);
            ConnectHostClientBlockingAuthenticatedAndReady();
            CreateNetworkedAndSpawnPlayer(out _go, out _, out _statsHolder, NetworkServer.localConnection);

            _go.AddComponent<DiscretePositionBehaviour>();
            _go.AddComponent<DarklandUnitDeathBehaviour>();
            _go.AddComponent<DarklandHeroDeathBehaviour>();
        }

        [UnityTest]
        public IEnumerator A() {
            _go.GetComponent<IDiscretePosition>().Set(Vector3Int.right);
            _go.GetComponent<IStatsHolder>().Set(StatId.Health, StatVal.OfBasic(0));
            
            yield return null;
            Assert.AreEqual(1.0,  _go.GetComponent<IStatsHolder>().BasicVal(StatId.Health));
            Assert.AreEqual(0.0f, _go.GetComponent<IStatsHolder>().BasicVal(StatId.Mana));
            Assert.AreEqual(Vector3Int.zero, _go.GetComponent<IDiscretePosition>().Pos);
        }
    }

}