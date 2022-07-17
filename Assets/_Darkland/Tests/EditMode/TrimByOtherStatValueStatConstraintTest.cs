using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.StatConstraint;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

namespace _Darkland.Tests.EditMode {

    [TestFixture]
    public class TrimByOtherStatValueStatConstraintTest {

        private TrimByOtherStatValueStatConstraint _constraint;

        [OneTimeSetUp]
        public void SetUp() {
            _constraint = ScriptableObject.CreateInstance<TrimByOtherStatValueStatConstraint>();
        }

        [Test]
        public void InputStatValueGreaterThanConstraintValue_ResultEqualToConstraintValue() {
            //Arrange
            const int maxHealthValue = 10;
            var statsHolder = Substitute.For<IStatsHolder>();
            var healthStatValue = StatValue.OfBasic(maxHealthValue + 1);
            var maxHealthStat = new Stat(StatId.MaxHealth, () => StatValue.OfBasic(maxHealthValue), value => { });
            statsHolder.Stat(StatId.MaxHealth).Returns(maxHealthStat);
            _constraint.trimByStatId = StatId.MaxHealth;

            //Act
            var result = _constraint.Apply(statsHolder, healthStatValue);

            //Assert
            Assert.AreEqual(maxHealthValue, result.basic);
        }
    }

}