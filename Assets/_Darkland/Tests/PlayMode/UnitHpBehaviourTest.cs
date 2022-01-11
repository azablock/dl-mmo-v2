using System.Collections;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Tests.Common;
using Mirror;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode {

    public class UnitHpBehaviourTest : MirrorPlayModeTest {

        private UnitHpBehaviour2 _unitHpBehaviour;
        private int _serverHpChangedCalled;
        private int _clientHpChangedCalled;
        
        [UnitySetUp]
        public override IEnumerator UnitySetUp() {
            yield return base.UnitySetUp();

            NetworkServer.Listen(1);
            ConnectHostClientBlockingAuthenticatedAndReady();
            CreateNetworkedAndSpawnPlayer(out _, out _, out _unitHpBehaviour, NetworkServer.localConnection);

            _serverHpChangedCalled = 0;
            _clientHpChangedCalled = 0;
            
            _unitHpBehaviour.unitHpActions.serverHpChanged += hp => { _serverHpChangedCalled++; };
            _unitHpBehaviour.unitHpActions.clientHpChanged += hp => { _clientHpChangedCalled++; };
        }
        
        [UnityTest]
        public IEnumerator WhenServerChangeHpCalled_HpChangedActionsCalled() {
            //given
            var hpDelta = 10;
        
            //when
            _unitHpBehaviour.ServerChangeMaxHp(hpDelta);
            _unitHpBehaviour.ServerChangeHp(hpDelta);
            
            //then
            Assert.AreEqual(1, _serverHpChangedCalled);
            Assert.AreEqual(1, _clientHpChangedCalled);
        
            yield return null;
        }

        [UnityTest]
        public IEnumerator WhenNewMaxHpLessThanHp_ServerChangeHpEqualToMaxHp() {
            //given
            var maxHpDelta = -5;

            //when
            _unitHpBehaviour.ServerChangeMaxHp(10);
            _unitHpBehaviour.ServerChangeHp(10);
            _unitHpBehaviour.ServerChangeMaxHp(maxHpDelta);

            //then
            Assert.AreEqual(_unitHpBehaviour.hp, _unitHpBehaviour.maxHp);

            yield return null;
        }

        [UnityTest]
        public IEnumerator WhenNewMaxHpLessThanHp_ServerChangeHpCalledAfterMaxHpChange() {
            //given
            var maxHpDelta = -5;

            //when
            _unitHpBehaviour.ServerChangeMaxHp(10);
            _unitHpBehaviour.ServerChangeHp(10);
            _unitHpBehaviour.ServerChangeMaxHp(maxHpDelta);

            //then
            Assert.AreEqual(2, _serverHpChangedCalled);

            yield return null;
        }
        
        [UnityTest]
        public IEnumerator WhenNewHpGreaterThanMaxHp_ServerChangeHpEqualToMaxHp() {
            //given
            var hpDelta = 5000;

            //when
            _unitHpBehaviour.ServerChangeMaxHp(10);
            _unitHpBehaviour.ServerChangeHp(hpDelta);

            //then
            Assert.AreEqual(_unitHpBehaviour.hp, _unitHpBehaviour.maxHp);

            yield return null;
        }
    }

}