using _Darkland.Sources.Models.Unit.Hp;
using NSubstitute;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode {

    [TestFixture]
    public class HpControllerTest {

        private IHpController _hpController;
        private IHpHolder _hpHolderMock;

        [SetUp]
        public void SetUp() {
            _hpHolderMock = Substitute.For<IHpHolder>();
            _hpController = new HpController(_hpHolderMock);
        }
        
        [Test]
        public void HpDeltaPositiveAndHpEqualsZero_HpEqualToHpDelta() {
            //Arrange
            var hpDelta = 5;
            _hpHolderMock.maxHp.Returns(10);
        
            //Act
            _hpController.ChangeHp(hpDelta);
            
            //Assert
            Assert.AreEqual(hpDelta, _hpController.HpHolder.hp);
        }
        
        [Test]
        public void HpPlusHpDeltaIsLessThanZero_HpEqualToZero() {
            //Arrange
            _hpHolderMock.maxHp.Returns(10);
        
            //Act
            _hpController.ChangeHp(-1000);
            
            //Assert
            Assert.AreEqual(0, _hpController.HpHolder.hp);
        }
        
        [Test]
        public void MaxHpPlusMaxHpDeltaLessThanZero_MaxHpEqualToOne() {
            //Arrange
            _hpHolderMock.maxHp.Returns(2);
        
            //Act
            _hpController.ChangeMaxHp(-10);
            
            //Assert
            Assert.AreEqual(1, _hpController.HpHolder.maxHp);
        }
        
        [Test]
        public void HpGreaterThanOneAndMaxHpPlusMaxHpDeltaLessThanZero_HpEqualToOne() {
            //Arrange
            _hpHolderMock.maxHp.Returns(10);
            _hpHolderMock.hp.Returns(10);
        
            //Act
            _hpController.ChangeMaxHp(-1000);
            
            //Assert
            Assert.AreEqual(1, _hpController.HpHolder.hp);
        }
        
        [Test]
        public void HpPlusHpDeltaGreaterThanMaxHp_HpEqualToMaxHp() {
            //Arrange
            _hpHolderMock.maxHp.Returns(10);

            //Act
            _hpController.ChangeHp(10000);
            
            //Assert
            Assert.AreEqual(_hpController.HpHolder.maxHp, _hpController.HpHolder.hp);
        }
        
        [Test]
        public void MaxHpDeltaNegativeAndHpEqualToMaxHp_HpEqualToMaxHp() {
            //Arrange
            _hpHolderMock.maxHp.Returns(10);

            //Act
            _hpController.ChangeHp(10);
            _hpController.ChangeMaxHp(-5);

            //Assert
            Assert.AreEqual(_hpController.HpHolder.maxHp, _hpController.HpHolder.hp);
        }
        
        [Test]
        public void HpPlusHpDeltaLessThanMaxHp_MaxHpNotChanged() {
            //Arrange
            const int initialMaxHp = 10;
            _hpHolderMock.maxHp.Returns(initialMaxHp);

            //Act
            _hpController.ChangeHp(5);
            
            //Assert
            Assert.AreEqual(initialMaxHp, _hpController.HpHolder.maxHp);
        }
    }

}