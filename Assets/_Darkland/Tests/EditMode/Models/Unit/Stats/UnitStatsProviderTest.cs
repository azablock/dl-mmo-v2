using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit.Stats;
using _Darkland.Sources.Models.Unit.StatsCounter;
using NSubstitute;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode.Models.Unit.Stats {

    [TestFixture]
    public class UnitStatsProviderTest {

        private IUnitStatsProvider _unitStatsProvider;

        [Test]
        public void UnitStatsProviderHasOneBlankUnitStatsCounter_ResultUnitStatsAreWithEmptyValues() {
            //Arrange
            var counterMock = Substitute.For<IUnitStatsCounter>();
            counterMock.Get().Returns(new UnitStats());

            var counters = new List<IUnitStatsCounter> {counterMock};

            _unitStatsProvider = new UnitStatsProvider(counters);

            //Act
            var unitStats = _unitStatsProvider.Get();

            //Assert
            Assert.AreEqual(0.0f, unitStats.attackSpeed);
            Assert.AreEqual(1, _unitStatsProvider.UnitStatsCounters.ToArray().Length);
        }

        [Test]
        public void UnitStatsProviderHasEmptyUnitStatsCounters_ResultUnitStatsAreWithEmptyValues() {
            //Arrange
            _unitStatsProvider = new UnitStatsProvider(new List<IUnitStatsCounter>());

            //Act
            var unitStats = _unitStatsProvider.Get();

            //Assert
            Assert.AreEqual(0.0f, unitStats.attackSpeed);
        }
    }

}