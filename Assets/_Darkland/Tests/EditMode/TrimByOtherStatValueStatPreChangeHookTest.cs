using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace _Darkland.Tests.EditMode {

    [TestFixture]
    public class TrimByOtherStatValueStatPreChangeHookTest {

        private TrimByOtherStatValueStatPreChangeHook _preChangeHook;

        [OneTimeSetUp]
        public void SetUp() {
            _preChangeHook = ScriptableObject.CreateInstance<TrimByOtherStatValueStatPreChangeHook>();
        }

        [Test]
        public void InputStatValueGreaterThanConstraintValue_ResultEqualToConstraintValue() {
            //Arrange
            const int maxHealthValue = 10;
            var statsHolder = Substitute.For<IStatsHolder>();
            var healthStatValue = StatVal.OfBasic(maxHealthValue + 1);
            var maxHealthStat = new Stat(StatId.MaxHealth, () => StatVal.OfBasic(maxHealthValue), value => { });
            statsHolder.Stat(StatId.MaxHealth).Returns(maxHealthStat);
            _preChangeHook.trimByStatId = StatId.MaxHealth;

            //Act
            var result = _preChangeHook.Apply(statsHolder, healthStatValue);

            //Assert
            Assert.AreEqual(maxHealthValue, result.Basic);
        }
    }

}