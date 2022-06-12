using System.Collections;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.Unit.Stats2;
using _Darkland.Tests.Common;
using Mirror;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode.Stats2 {

    public class SimpleDarklandStatsHolder : DarklandStatsHolder {

        [DarklandStat(id = DarklandStatId.Health, getter = nameof(GetHealth), setter = nameof(SetHealth))]
        private FloatStat _health;

        public void SetHealth(FloatStat val) {
            _health = val;
        }

        public FloatStat GetHealth() => _health;
    }
    
    [TestFixture]
    public class DarklandStatsHolderTest : MirrorPlayModeTest {

        private SimpleDarklandStatsHolder _statsHolder;
        
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
            var healthStatApi = _statsHolder.Stat(DarklandStatId.Health);
            var healthStat = healthStatApi.getFunc();

            //Act
            
            //Assert
            Assert.AreEqual(0.0f, healthStat.Basic);
            Assert.AreEqual(0.0f, healthStat.Bonus);
            yield return null;
        }

        [UnityTest]
        public IEnumerator B() {
            //Arrange
            var healthStatApi = _statsHolder.Stat(DarklandStatId.Health);

            //Act
            healthStatApi.setAction(FloatStat.Of(1.0f, 0.0f));
            var healthStat = healthStatApi.getFunc();
            
            //Assert
            Assert.AreEqual(1.0f, healthStat.Basic);
            Assert.AreEqual(0.0f, healthStat.Bonus);
            yield return null;
        }

        [UnityTest]
        public IEnumerator C() {
            //Arrange
            var healthStatApi = _statsHolder.Stat(DarklandStatId.Health);
            var changedCallCount = 0;
            healthStatApi.Changed += stat => changedCallCount++;
            
            //Act
            healthStatApi.setAction(FloatStat.Of(1.0f, 0.0f));

            //Assert
            Assert.AreEqual(1, changedCallCount);
            yield return null;
        }
        
    }

}