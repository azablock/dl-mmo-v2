using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace _Darkland.Tests.EditMode {

    [TestFixture]
    public class CheckHealthOnMaxHealthStatChangeHookTest {

        private CheckHealthOnMaxHealthStatChangeHook _hook;

        [OneTimeSetUp]
        public void SetUp() {
            _hook = ScriptableObject.CreateInstance<CheckHealthOnMaxHealthStatChangeHook>();
        }
        
        [Test]
        public void MaxHealthIncreased_HealthValueNotChanged() {
            //Arrange
            var statsHolder = Substitute.For<IStatsHolder>();
            var healthValue = StatValue.Zero;
            var maxHealthValue = StatValue.OfBasic(10);
            var healthStat = new Stat(StatId.Health, () => healthValue, value => { healthValue = value; });
            var maxHealthStat = new Stat(StatId.MaxHealth, () => maxHealthValue, value => { maxHealthValue = value; });
            statsHolder.Stat(StatId.Health).Returns(healthStat);
            statsHolder.Stat(StatId.MaxHealth).Returns(maxHealthStat);

            //Act
            _hook.Register(statsHolder);
            statsHolder.Stat(StatId.MaxHealth).Add(StatValue.OfBasic(1));
            _hook.Unregister(statsHolder);
            
            //Assert
            Assert.AreEqual(0, statsHolder.Stat(StatId.Health).Basic);
            Assert.AreEqual(0, statsHolder.Stat(StatId.Health).Bonus);
        }

        [Test]
        public void MaxHealthSetToBeLessThanHealth_HealthValueEqualToMaxHealth() {
            //Arrange
            const int healthInitialValue = 5;
            var statsHolder = Substitute.For<IStatsHolder>();
            var healthValue = StatValue.OfBasic(healthInitialValue);
            var maxHealthValue = StatValue.OfBasic(healthInitialValue + 1);
            var healthStat = new Stat(StatId.Health, () => healthValue, value => { healthValue = value; });
            var maxHealthStat = new Stat(StatId.MaxHealth, () => maxHealthValue, value => { maxHealthValue = value; });
            statsHolder.Stat(StatId.Health).Returns(healthStat);
            statsHolder.Stat(StatId.MaxHealth).Returns(maxHealthStat);

            //Act
            _hook.Register(statsHolder);
            statsHolder.Stat(StatId.MaxHealth).Set(StatValue.OfBasic(healthInitialValue - 1));
            _hook.Unregister(statsHolder);
            
            //Assert
            Assert.AreEqual(healthInitialValue - 1, statsHolder.Stat(StatId.Health).Basic);
        }
    }

}