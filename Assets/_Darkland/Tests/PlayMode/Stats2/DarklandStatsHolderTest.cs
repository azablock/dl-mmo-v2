using System;
using System.Collections;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.Unit.Stats2;
using _Darkland.Tests.Common;
using Mirror;
using NSubstitute;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode.Stats2 {

    public class SimpleStatsHolder : StatsHolder {
        
        [DarklandStat(StatId.Health, nameof(ServerSetHealth))]
        private float _health;

        private IStatPreChangeHooksHolder _statPreChangeHooksHolder;

        [Server]
        private void ServerSetHealth(float val) => _health = val;

        private void Start() {
            _statPreChangeHooksHolder = Substitute.For<IStatPreChangeHooksHolder>();
            _statPreChangeHooksHolder.PreChangeHooks(Arg.Any<StatId>()).Returns(new List<IStatPreChangeHook>());
        }

        public override IStatPreChangeHooksHolder statPreChangeHooksHolder => _statPreChangeHooksHolder;
    }
    
    [TestFixture]
    public class DarklandStatsHolderTest : MirrorPlayModeTest {

        private SimpleStatsHolder _statsHolder;
        
        [UnitySetUp]
        public override IEnumerator UnitySetUp() {
            yield return base.UnitySetUp();
        
            NetworkServer.Listen(1);
            ConnectHostClientBlockingAuthenticatedAndReady();
            CreateNetworkedAndSpawnPlayer(out _, out _, out _statsHolder, NetworkServer.localConnection);
        }

        [UnityTest]
        public IEnumerator A() {
            //Arrange
            var healthStat = _statsHolder.Stat(StatId.Health);
            var healthStatValue = healthStat.Get();

            //Act
            
            //Assert
            Assert.AreEqual(0.0f, healthStatValue);
            yield return null;
        }

        [UnityTest]
        public IEnumerator B() {
            //Arrange
            var healthStat = _statsHolder.Stat(StatId.Health);

            //Act
            healthStat.Set(StatVal.OfBasic(1.0f));
            var healthStatValue = healthStat.Get();
            
            //Assert
            Assert.AreEqual(1.0f, healthStatValue);
            yield return null;
        }

        [UnityTest]
        public IEnumerator C() {
            //Arrange
            var healthStat = _statsHolder.Stat(StatId.Health);
            var changedCallCount = 0;
            healthStat.Changed += stat => changedCallCount++;
            
            //Act
            healthStat.Set(StatVal.OfBasic(1.0f));

            //Assert
            Assert.AreEqual(1, changedCallCount);
            yield return null;
        }

        [UnityTest]
        public IEnumerator whenDoesNotContainStatWithGivenId_ShouldThrowEx() {
            //Arrange
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _statsHolder.Stat(StatId.Hunger));
            
            yield return null;
        }
        
    }

}