using System.Collections;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Tests.Common;
using Mirror;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode {

    public class HpBehaviourTest : MirrorPlayModeTest {

        private HpBehaviour _hpBehaviour;

        [UnitySetUp]
        public override IEnumerator UnitySetUp() {
            yield return base.UnitySetUp();
        
            NetworkServer.Listen(1);
            ConnectHostClientBlockingAuthenticatedAndReady();
            CreateNetworkedAndSpawnPlayer(out _, out _, out _hpBehaviour, NetworkServer.localConnection);
        }
        
        [UnityTest]
        public IEnumerator HpSetToMaxHp_HpEqualToMaxHp() {
            //given
            var hpDelta = 10;
        
            //when
            _hpBehaviour.ChangeMaxHp(hpDelta);
            _hpBehaviour.ChangeHp(hpDelta);
            
            //then
            yield return null;
            Assert.AreEqual(_hpBehaviour.maxHp, _hpBehaviour.hp);
            Assert.AreEqual(hpDelta, _hpBehaviour.hp);
        
        }
        
        [UnityTest]
        public IEnumerator NewMaxHpLessThanHp_HpEqualToMaxHp() {
            //given
            var maxHpDelta = -5;
        
            //when
            _hpBehaviour.ChangeMaxHp(10);
            _hpBehaviour.ChangeHp(10);
            _hpBehaviour.ChangeMaxHp(maxHpDelta);
        
            //then
            yield return null;
            Assert.AreEqual(_hpBehaviour.hp, _hpBehaviour.maxHp);
        }

        [UnityTest]
        public IEnumerator HpDeltaGreaterThanMaxHp_HpEqualToMaxHp() {
            //given
            var hpDelta = 5000;
        
            //when
            _hpBehaviour.ChangeMaxHp(10);
            _hpBehaviour.ChangeHp(hpDelta);
        
            //then
            yield return null;
            Assert.AreEqual(_hpBehaviour.hp, _hpBehaviour.maxHp);
        }
        
        [UnityTest]
        public IEnumerator HpGreaterThanOneAndMaxHpPlusMaxHpDeltaLessThanZero_HpEqualToOne() {
            //Arrange
        
            //Act
            _hpBehaviour.ChangeMaxHp(10);
            _hpBehaviour.ChangeHp(10);
            _hpBehaviour.ChangeMaxHp(-1000);
            
            //Assert
            yield return null;
            Assert.AreEqual(1, _hpBehaviour.hp);
        }
        
        [UnityTest]
        public IEnumerator MaxHpDeltaNegativeAndHpEqualToMaxHp_HpEqualToMaxHp() {
            //Arrange
        
            //Act
            _hpBehaviour.ChangeMaxHp(10);
            _hpBehaviour.ChangeHp(10);
            _hpBehaviour.ChangeMaxHp(-5);
        
            //Assert
            yield return null;
            Assert.AreEqual(_hpBehaviour.maxHp, _hpBehaviour.hp);
        }
        
        [UnityTest]
        public IEnumerator HpPlusHpDeltaLessThanMaxHp_MaxHpNotChanged() {
            //Arrange
            const int initialMaxHp = 10;
        
            //Act
            _hpBehaviour.ChangeMaxHp(initialMaxHp);
            _hpBehaviour.ChangeHp(5);
            
            //Assert
            yield return null;
            Assert.AreEqual(initialMaxHp, _hpBehaviour.maxHp);
        }    

        [UnityTest]
        public IEnumerator RegainHpToMaxHp_HpEqualToMaxHp() {
            //Arrange
            const int initialMaxHp = 10;
        
            //Act
            _hpBehaviour.ChangeMaxHp(initialMaxHp);
            _hpBehaviour.ServerRegainHpToMaxHp();
            
            //Assert
            yield return null;
            Assert.AreEqual(_hpBehaviour.maxHp, _hpBehaviour.hp);
            Assert.AreEqual(initialMaxHp, _hpBehaviour.hp);
        }
    }

}