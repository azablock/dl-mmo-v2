using System;
using _Darkland.Sources.Models.Stats2;
using _Darkland.Sources.Models.Unit.Stats2;
using NSubstitute;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode.Models.Stats2 {

    [TestFixture]
    public class TrimToMaxByOtherStatHandlerTest {

        private ITrimToMaxByOtherStatHandler _handler = new TrimToMaxByOtherStatHandler();
        private IStatsHolder _statsHolder;

        [OneTimeSetUp]
        public void SetUp() {
            _statsHolder = Substitute.For<IStatsHolder>();
        }

        [Test]
        public void MaxHealthIncreased_HealthValueNotChanged() {
            //Arrange
            const float healthInitialValue = 1;
            var healthValue = StatVal.OfBasic(healthInitialValue);
            var maxHealthValue = StatVal.OfBasic(10.0f);
            var healthStat = new Stat(StatId.Health, () => healthValue, value => { healthValue = value; });
            var maxHealthStat = new Stat(StatId.MaxHealth, () => maxHealthValue, value => { maxHealthValue = value; });

            _statsHolder.Stat(StatId.Health).Returns(healthStat);
            _statsHolder.Stat(StatId.MaxHealth).Returns(maxHealthStat);
            _statsHolder.ValueOf(StatId.Health).Returns(_ => healthStat.Get());
            _statsHolder.Stats(StatId.Health, StatId.MaxHealth).Returns(new Tuple<Stat, Stat>(healthStat, maxHealthStat));

            //Act
            _statsHolder.Stat(StatId.MaxHealth).Add(StatVal.OfBasic(1));
            _handler.Handle(_statsHolder, StatId.Health, StatId.MaxHealth);

            //Assert
            Assert.AreEqual(healthInitialValue, _statsHolder.ValueOf(StatId.Health).Basic);
        }

        [Test]
        public void MaxHealthSetToBeLessThanHealth_HealthValueEqualToMaxHealth() {
            //Arrange
            const float healthInitialValue = 5;
            var healthValue = StatVal.OfBasic(healthInitialValue);
            var maxHealthValue = StatVal.OfBasic(healthInitialValue);
            var healthStat = new Stat(StatId.Health, () => healthValue, value => { healthValue = value; });
            var maxHealthStat = new Stat(StatId.MaxHealth, () => maxHealthValue, value => { maxHealthValue = value; });
            
            _statsHolder.Stat(StatId.Health).Returns(healthStat);
            _statsHolder.Stat(StatId.MaxHealth).Returns(maxHealthStat);
            _statsHolder.ValueOf(StatId.Health).Returns(_ => healthStat.Get());
            _statsHolder.Stats(StatId.Health, StatId.MaxHealth).Returns(new Tuple<Stat, Stat>(healthStat, maxHealthStat));

            //Act
            _statsHolder.Stat(StatId.MaxHealth).Set(StatVal.OfBasic(healthInitialValue - 1));
            _handler.Handle(_statsHolder, StatId.Health, StatId.MaxHealth);

            //Assert
            Assert.AreEqual(healthInitialValue - 1, _statsHolder.ValueOf(StatId.Health).Basic);
        }
    }

}