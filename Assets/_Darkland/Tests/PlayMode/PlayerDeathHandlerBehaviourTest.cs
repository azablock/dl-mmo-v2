using System.Collections;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Tests.Common;
using Mirror;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode {

    [TestFixture]
    public class PlayerDeathHandlerBehaviourTest : MirrorPlayModeTest {

        private PlayerDeathHandlerBehaviour _playerDeathHandlerBehaviour;
        private HpBehaviour _hpBehaviour;
        
        [UnitySetUp]
        public override IEnumerator UnitySetUp() {
            yield return base.UnitySetUp();
        
            NetworkServer.Listen(1);
            ConnectHostClientBlockingAuthenticatedAndReady();
            CreateNetworkedAndSpawnPlayer(out _, out _, out _playerDeathHandlerBehaviour, NetworkServer.localConnection);

            _hpBehaviour = _playerDeathHandlerBehaviour.GetComponent<HpBehaviour>();
        }

        [UnityTest]
        public IEnumerator A() {
            //Arrange

            //Act
            _hpBehaviour.ServerChangeMaxHp(10);
            _hpBehaviour.ServerChangeHp(0);
            
            //Assert
            yield return null;
            Assert.AreEqual(_hpBehaviour.hp, _hpBehaviour.maxHp);
        }
    }

}