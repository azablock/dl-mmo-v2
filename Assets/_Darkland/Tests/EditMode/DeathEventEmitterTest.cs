using System;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using NSubstitute;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode {

    public class DeathEventEmitterTest {

        private IDeathEventEmitter _deathEventEmitter;

        [Test]
        public void HealthStatChangedToZero_PlayerDeadEventCalledOnce() {
            //Arrange
            var eventCallCount = 0;
            var healthStat = Substitute.For<IStat>();
            void OnDeath() { eventCallCount++; }

            _deathEventEmitter = new DeathEventEmitter(healthStat);
            _deathEventEmitter.Death += OnDeath;

            //Act
            healthStat.Changed += Raise.Event<Action<StatValue>>(StatValue.Zero);
            _deathEventEmitter.Death -= OnDeath;

            //Assert
            Assert.AreEqual(1, eventCallCount);
        }

        [Test]
        public void HealthStatChangedToValueGreaterThanZero_DeathEventNotCalled() {
            //Arrange
            var eventCallCount = 0;
            var healthStat = Substitute.For<IStat>();
            void OnDeath() { eventCallCount++; }

            _deathEventEmitter = new DeathEventEmitter(healthStat);
            _deathEventEmitter.Death += OnDeath;

            //Act
            healthStat.Changed += Raise.Event<Action<StatValue>>(StatValue.OfBasic(2));
            _deathEventEmitter.Death -= OnDeath;

            //Assert
            Assert.AreEqual(0, eventCallCount);
        }
    }

}