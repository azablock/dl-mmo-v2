using System;
using _Darkland.Sources.Models.Stats2;
using _Darkland.Sources.Models.Unit.Stats2;
using NSubstitute;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode.Models.Stats2 {

    [TestFixture]
    public class TrimToMaxByOtherStatHandlerTest {

        private readonly ITrimToMaxByOtherStatHandler _handler = new TrimToMaxByOtherStatHandler();
        private IStatsHolder _statsHolder;
        private Stat _healthStat;
        private Stat _maxHealthStat;
        private StatVal _healthStatVal;
        private StatVal _maxHealthStatVal;

        [SetUp]
        public void SetUp() {
            _healthStatVal = StatVal.Zero;
            _healthStat = new Stat(StatId.Health, () => _healthStatVal, value => { _healthStatVal = value; });
            
            _maxHealthStatVal = StatVal.Zero;
            _maxHealthStat = new Stat(StatId.MaxHealth, () => _maxHealthStatVal, value => { _maxHealthStatVal = value; });
            
            _statsHolder = Substitute.For<IStatsHolder>();
            _statsHolder.Stat(StatId.Health).Returns(_healthStat);
            _statsHolder.Stat(StatId.MaxHealth).Returns(_maxHealthStat);
            _statsHolder.ValueOf(StatId.Health).Returns(_ => _healthStat.Get());
            _statsHolder.Stats(StatId.Health, StatId.MaxHealth).Returns(new Tuple<Stat, Stat>(_healthStat, _maxHealthStat));
        }

        [Test]
        public void When_MaxHealthIncreased_Then_HealthValueNotChanged() {
            //Arrange
            const float healthInitialValue = 1;
            _healthStat.Set(StatVal.OfBasic(healthInitialValue));
            _maxHealthStat.Set(StatVal.OfBasic(10.0f));
 
            //Act
            _statsHolder.Stat(StatId.MaxHealth).Add(StatVal.OfBasic(1));
            _handler.Handle(_statsHolder, StatId.Health, StatId.MaxHealth);

            //Assert
            Assert.AreEqual(healthInitialValue, _statsHolder.BasicVal(StatId.Health));
        }

        [Test]
        public void When_MaxHealthSetToBeLessThanHealth_Then_HealthValueEqualToMaxHealth() {
            //Arrange
            const float healthInitialValue = 5;
            _healthStat.Set(StatVal.OfBasic(healthInitialValue));
            _maxHealthStat.Set(StatVal.OfBasic(healthInitialValue));
            
            //Act
            _statsHolder.Stat(StatId.MaxHealth).Set(StatVal.OfBasic(healthInitialValue - 1));
            _handler.Handle(_statsHolder, StatId.Health, StatId.MaxHealth);

            //Assert
            Assert.AreEqual(healthInitialValue - 1, _statsHolder.BasicVal(StatId.Health));
        }
    }

}