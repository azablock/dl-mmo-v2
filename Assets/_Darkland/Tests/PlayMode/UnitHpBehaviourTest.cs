using System.Collections;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Tests.Common;
using Mirror;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode {

    public class UnitHpBehaviourTest : MirrorPlayModeTest {

        private HpBehaviour _hpBehaviour;

        [UnitySetUp]
        public override IEnumerator UnitySetUp() {
            yield return base.UnitySetUp();
        
            NetworkServer.Listen(1);
            ConnectHostClientBlockingAuthenticatedAndReady();
            CreateNetworkedAndSpawnPlayer(out _, out _, out _hpBehaviour, NetworkServer.localConnection);
        }
        //
        // [UnityTest]
        // public IEnumerator HpSetToMaxHp_HpEqualToMaxHp() {
        //     //given
        //     var hpDelta = 10;
        //     var discretePosition = Substitute.For<DiscretePositionBehaviour>();
        //
        //     //when
        //     _hpBehaviour.ServerChangeMaxHp(hpDelta);
        //     _hpBehaviour.ServerChangeHp(hpDelta);
        //     
        //     //then
        //     Assert.AreEqual(_hpBehaviour.maxHp, _hpBehaviour.hp);
        //     Assert.AreEqual(hpDelta, _hpBehaviour.hp);
        //
        //     yield return null;
        // }
        //
        // [UnityTest]
        // public IEnumerator NewMaxHpLessThanHp_HpEqualToMaxHp() {
        //     //given
        //     var maxHpDelta = -5;
        //
        //     //when
        //     _hpBehaviour.ServerChangeMaxHp(10);
        //     _hpBehaviour.ServerChangeHp(10);
        //     _hpBehaviour.ServerChangeMaxHp(maxHpDelta);
        //
        //     //then
        //     Assert.AreEqual(_hpBehaviour.hp, _hpBehaviour.maxHp);
        //
        //     yield return null;
        // }
        //
        // [UnityTest]
        // public IEnumerator WhenNewMaxHpLessThanHp_ServerChangeHpCalledAfterMaxHpChange() {
        //     //given
        //     var maxHpDelta = -5;
        //
        //     //when
        //     _unitHpBehaviour.ServerChangeMaxHp(10);
        //     _unitHpBehaviour.ServerChangeHp(10);
        //     _unitHpBehaviour.ServerChangeMaxHp(maxHpDelta);
        //
        //     //then
        //     Assert.AreEqual(2, _serverHpChangedCalled);
        //
        //     yield return null;
        // }
        //
        // [UnityTest]
        // public IEnumerator WhenNewHpGreaterThanMaxHp_ServerChangeHpEqualToMaxHp() {
        //     //given
        //     var hpDelta = 5000;
        //
        //     //when
        //     _unitHpBehaviour.ServerChangeMaxHp(10);
        //     _unitHpBehaviour.ServerChangeHp(hpDelta);
        //
        //     //then
        //     Assert.AreEqual(_unitHpBehaviour.hp, _unitHpBehaviour.maxHp);
        //
        //     yield return null;
        // }
    }

}