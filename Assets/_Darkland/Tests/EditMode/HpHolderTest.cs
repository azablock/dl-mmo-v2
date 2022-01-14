using System;
using _Darkland.Sources.Models.Unit.Hp;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode {

    [TestFixture]
    public class HpHolderTest {

        private IHpHolder _hpHolder;

        [Test]
        public void HpDeltaPositive_HpEqualToHpDelta() {
            //Arrange
            _hpHolder = new HpHolder();

            //Act
            _hpHolder.ChangeMaxHp(10);
            _hpHolder.ChangeHp(5);
            
            //Assert
            Assert.AreEqual(5, _hpHolder.hp);
        }

        [Test]
        public void HpPlusHpDeltaIsLessThanZero_HpEqualToZero() {
            //Arrange
            _hpHolder = new HpHolder();

            //Act
            _hpHolder.ChangeMaxHp(10);
            _hpHolder.ChangeHp(-100000);
            
            //Assert
            Assert.AreEqual(0, _hpHolder.hp);
        }

        [Test]
        public void MaxHpPlusMaxHpDeltaLessThanZero_MaxHpEqualToOne() {
            //Arrange
            _hpHolder = new HpHolder();

            //Act
            _hpHolder.ChangeMaxHp(-10);
            
            //Assert
            Assert.AreEqual(1, _hpHolder.maxHp);
        }

        [Test]
        public void HpGreaterThanOneAndMaxHpPlusMaxHpDeltaLessThanZero_HpEqualToOne() {
            //Arrange
            _hpHolder = new HpHolder();

            //Act
            _hpHolder.ChangeMaxHp(10);
            _hpHolder.ChangeHp(10);
            _hpHolder.ChangeMaxHp(-1000);
            
            //Assert
            Assert.AreEqual(1, _hpHolder.hp);
        }

        [Test]
        public void HpPlusHpDeltaGreaterThanMaxHp_HpEqualToMaxHp() {
            //Arrange
            _hpHolder = new HpHolder();

            //Act
            _hpHolder.ChangeMaxHp(10);
            _hpHolder.ChangeHp(10000);
            
            //Assert
            Assert.AreEqual(_hpHolder.maxHp, _hpHolder.hp);
        }

        [Test]
        public void MaxHpDeltaNegativeAndHpEqualToMaxHp_HpEqualToMaxHp() {
            //Arrange
            _hpHolder = new HpHolder();

            //Act
            _hpHolder.ChangeMaxHp(10);
            _hpHolder.ChangeHp(10);
            _hpHolder.ChangeMaxHp(-5);
            
            //Assert
            Assert.AreEqual(_hpHolder.maxHp, _hpHolder.hp);
        }

        [Test]
        public void HpPlusHpDeltaLessThanMaxHp_MaxHpNotChanged() {
            //Arrange
            _hpHolder = new HpHolder();
            const int initialMaxHp = 10;

            //Act
            _hpHolder.ChangeMaxHp(initialMaxHp);
            _hpHolder.ChangeHp(5);
            
            //Assert
            Assert.AreEqual(initialMaxHp, _hpHolder.maxHp);
        }

        [Test]
        public void ChangeHpCalled_HpChangedEventCalled() {
            //Arrange
            _hpHolder = new HpHolder();
            var callCount = 0;
            void OnHpChanged(int hp) => callCount++;
            _hpHolder.hpChanged += OnHpChanged;

            //Act
            _hpHolder.ChangeHp(5);
            _hpHolder.hpChanged -= OnHpChanged;
            
            //Assert
            Assert.AreEqual(1, callCount);
        }

        [Test]
        public void ChangeMaxHpCalled_MaxHpChangedEventCalled() {
            //Arrange
            _hpHolder = new HpHolder();
            var callCount = 0;
            void OnMaxHpChanged(int maxHp) => callCount++;
            _hpHolder.maxHpChanged += OnMaxHpChanged;

            //Act
            _hpHolder.ChangeMaxHp(5);
            _hpHolder.maxHpChanged -= OnMaxHpChanged;
            
            //Assert
            Assert.AreEqual(1, callCount);
        }
    }

}