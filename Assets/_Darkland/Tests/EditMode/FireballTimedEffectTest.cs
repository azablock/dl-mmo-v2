using _Darkland.Sources.ScriptableObjects.Spell.TimedEffect;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace _Darkland.Tests.EditMode {

    [TestFixture]
    public class FireballTimedEffectTest {

        private FireballTimedEffect _effect;

        [OneTimeSetUp]
        public void SetUp() {
            _effect = ScriptableObject.CreateInstance<FireballTimedEffect>();
        }

        [Test]
        public void T1() {
            //Arrange
            GameObject caster = Substitute.For<GameObject>();
            // _effect.Process();

            //Act


            //Assert
        }
    }

}