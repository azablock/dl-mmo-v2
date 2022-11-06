using System;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace _Darkland.Tests.EditMode {

    [TestFixture]
    public class CheckHealthOnMaxHealthStatPostChangeHookTest {

        private CheckHealthOnMaxHealthStatPostChangeHook _hook;
        private IStatsHolder _statsHolder;

        [OneTimeSetUp]
        public void SetUp() {
            _hook = ScriptableObject.CreateInstance<CheckHealthOnMaxHealthStatPostChangeHook>();
            _statsHolder = Substitute.For<IStatsHolder>();
        }

        [Test]
        public void MaxHealthIncreased_HealthValueNotChanged() {
            //Arrange
            const float healthInitialValue = 1;
            var healthValue = healthInitialValue;
            var maxHealthValue = 10.0f;
            var healthStat = new Stat(StatId.Health, () => healthValue, value => { healthValue = value; });
            var maxHealthStat = new Stat(StatId.MaxHealth, () => maxHealthValue, value => { maxHealthValue = value; });
            _statsHolder.Stat(StatId.Health).Returns(healthStat);
            _statsHolder.Stat(StatId.MaxHealth).Returns(maxHealthStat);
            _statsHolder.ValueOf(StatId.Health).Returns(_ => healthStat.Get());
            _statsHolder.Stats(StatId.Health, StatId.MaxHealth)
                        .Returns(new Tuple<Stat, Stat>(healthStat, maxHealthStat));

            //Act
            _statsHolder.Stat(StatId.MaxHealth).Add(1);
            _hook.OnStatChange(_statsHolder);

            //Assert
            Assert.AreEqual(healthInitialValue, _statsHolder.ValueOf(StatId.Health));
        }

        [Test]
        public void MaxHealthSetToBeLessThanHealth_HealthValueEqualToMaxHealth() {
            //Arrange
            const float healthInitialValue = 5;
            var healthValue = healthInitialValue;
            var maxHealthValue = healthInitialValue;
            var healthStat = new Stat(StatId.Health, () => healthValue, value => { healthValue = value; });
            var maxHealthStat = new Stat(StatId.MaxHealth, () => maxHealthValue, value => { maxHealthValue = value; });
            _statsHolder.Stat(StatId.Health).Returns(healthStat);
            _statsHolder.Stat(StatId.MaxHealth).Returns(maxHealthStat);
            _statsHolder.ValueOf(StatId.Health).Returns(_ => healthStat.Get());
            _statsHolder.Stats(StatId.Health, StatId.MaxHealth)
                        .Returns(new Tuple<Stat, Stat>(healthStat, maxHealthStat));

            //Act
            _statsHolder.Stat(StatId.MaxHealth).Set(healthInitialValue - 1);
            _hook.OnStatChange(_statsHolder);

            //Assert
            Assert.AreEqual(healthInitialValue - 1, _statsHolder.ValueOf(StatId.Health));
        }
    }

}