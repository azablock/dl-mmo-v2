using System;
using _Darkland.Sources.Models.Unit.StatsCounter;
using _Darkland.Sources.Models.Unit.Traits;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode.Models.Unit.StatsCounter {

    [TestFixture]
    public class ByUnitTraitsUnitStatsCounterTest {

        private IUnitStatsCounter _counter;

        [Test]
        public void DexteritySet_AttackSpeedCalculated() {
            //Arrange
            var func = new Func<UnitTraitsData>(() => new UnitTraitsData {
                dexterity = UnitTraitValue.Of(1, 0)
            });

            _counter = new ByUnitTraitsUnitStatsCounter(func);

            //Act
            var unitStats = _counter.Get();

            //Assert
            Assert.AreEqual(1, unitStats.attackSpeed);
        }
    }

}