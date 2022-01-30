using _Darkland.Sources.Models.Unit.Traits;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode.Models.Unit.Traits {

    [TestFixture]
    public class UnitTraitsHolderTest {

        private IUnitTraitsHolder _unitTraitsHolder;

        [SetUp]
        public void SetUp() {
            _unitTraitsHolder = new UnitTraitsHolder();
        }
        
        [Test]
        [TestCase(UnitTraitId.Might)]
        [TestCase(UnitTraitId.Constitution)]
        [TestCase(UnitTraitId.Dexterity)]
        [TestCase(UnitTraitId.Intelligence)]
        [TestCase(UnitTraitId.Soul)]
        public void NewBasicTraitValueGreaterThanZero_ChangeBasic_BasicTraitEqualToArgValue(UnitTraitId traitId) {
            //Arrange
            const int newBasicTraitValue = 5;

            //Act
            _unitTraitsHolder.ChangeBasic(traitId, newBasicTraitValue);

            //Assert
            var unitTraitValue = _unitTraitsHolder.Get(traitId);
            Assert.AreEqual(newBasicTraitValue, unitTraitValue.basic);
            Assert.AreEqual(0, unitTraitValue.bonus);
            Assert.AreEqual(newBasicTraitValue, unitTraitValue.current);
        }

        [Test]
        [TestCase(UnitTraitId.Might)]
        [TestCase(UnitTraitId.Constitution)]
        [TestCase(UnitTraitId.Dexterity)]
        [TestCase(UnitTraitId.Intelligence)]
        [TestCase(UnitTraitId.Soul)]
        public void NewBonusTraitValueGreaterThanZero_ChangeBonus_BonusTraitEqualToArgValue(UnitTraitId traitId) {
            //Arrange
            const int newBonusTraitValue = 2;

            //Act
            _unitTraitsHolder.ChangeBonus(traitId, newBonusTraitValue);

            //Assert
            var unitTraitValue = _unitTraitsHolder.Get(traitId);
            Assert.AreEqual(0, unitTraitValue.basic);
            Assert.AreEqual(newBonusTraitValue, unitTraitValue.bonus);
            Assert.AreEqual(newBonusTraitValue, unitTraitValue.current);
        }
        
        [Test]
        [TestCase(UnitTraitId.Might)]
        [TestCase(UnitTraitId.Constitution)]
        [TestCase(UnitTraitId.Dexterity)]
        [TestCase(UnitTraitId.Intelligence)]
        [TestCase(UnitTraitId.Soul)]
        public void NewBasicTraitValueLessThanZero_ChangeBasic_BasicTraitEqualToZero(UnitTraitId traitId) {
            //Arrange
            const int newBasicTraitValue = -200;

            //Act
            _unitTraitsHolder.ChangeBasic(traitId, newBasicTraitValue);

            //Assert
            var unitTraitValue = _unitTraitsHolder.Get(traitId);
            Assert.AreEqual(0, unitTraitValue.basic);
            Assert.AreEqual(0, unitTraitValue.bonus);
            Assert.AreEqual(0, unitTraitValue.current);
        }

        [Test]
        [TestCase(UnitTraitId.Might)]
        [TestCase(UnitTraitId.Constitution)]
        [TestCase(UnitTraitId.Dexterity)]
        [TestCase(UnitTraitId.Intelligence)]
        [TestCase(UnitTraitId.Soul)]
        public void NewBonusTraitValueLessThanZero_ChangeBonus_BonusTraitEqualToZero(UnitTraitId traitId) {
            //Arrange
            const int newBonusTraitValue = -3000;

            //Act
            _unitTraitsHolder.ChangeBonus(traitId, newBonusTraitValue);

            //Assert
            var unitTraitValue = _unitTraitsHolder.Get(traitId);
            Assert.AreEqual(0, unitTraitValue.basic);
            Assert.AreEqual(0, unitTraitValue.bonus);
            Assert.AreEqual(0, unitTraitValue.current);
        }
    }

}