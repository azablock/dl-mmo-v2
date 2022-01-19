using _Darkland.Sources.Models.Unit.Hp;
using NSubstitute;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode {

    [TestFixture]
    public class HpCalculatorTest {

        private IHpCalculator _hpCalculator;
        private IHpHolder _hpHolderMock;

        [SetUp]
        public void SetUp() {
            _hpHolderMock = Substitute.For<IHpHolder>();
            _hpCalculator = new HpCalculator();
        }
        
        [Test]
        public void HpDeltaPositiveAndHpEqualsZero_HpEqualToHpDelta() {
            //Arrange
            var hpDelta = 5;
            _hpHolderMock.maxHp.Returns(10);
        
            //Act
            var result = _hpCalculator.CalculateHp(_hpHolderMock, hpDelta);
            
            //Assert
            Assert.AreEqual(hpDelta, result);
        }
        
        [Test]
        public void HpPlusHpDeltaIsLessThanZero_HpEqualToZero() {
            //Arrange
            _hpHolderMock.maxHp.Returns(10);
        
            //Act
            var result = _hpCalculator.CalculateHp(_hpHolderMock, -1000);
            
            //Assert
            Assert.AreEqual(0, result);
        }
        
        [Test]
        public void MaxHpPlusMaxHpDeltaLessThanZero_MaxHpEqualToOne() {
            //Arrange
            _hpHolderMock.maxHp.Returns(2);
        
            //Act
            var result = _hpCalculator.CalculateMaxHp(_hpHolderMock, -10);
            
            //Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void HpPlusHpDeltaGreaterThanMaxHp_HpEqualToMaxHp() {
            //Arrange
            _hpHolderMock.maxHp.Returns(10);

            //Act
            var result = _hpCalculator.CalculateHp(_hpHolderMock, 10000);
            
            //Assert
            Assert.AreEqual(_hpHolderMock.maxHp, result);
        }
        
    }

}