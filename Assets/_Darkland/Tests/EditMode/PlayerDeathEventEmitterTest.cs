using System;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Hp;
using _Darkland.Sources.Models.Unit.Stats2;
using NSubstitute;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode {

    public class PlayerDeathEventEmitterTest {

        private IPlayerDeathEventEmitter _playerDeathEventEmitter;

        [Test]
        public void HpChangedGivesValueEqualToZero_PlayerDeadEventCalledOnce() {
            //Arrange
            var eventCallCount = 0;
            var healthStat = Substitute.For<Stat>();
            void OnPlayerDead() { eventCallCount++; }

            _playerDeathEventEmitter = new PlayerDeathEventEmitter(healthStat);
            _playerDeathEventEmitter.PlayerDead += OnPlayerDead;

            //Act
            healthStat.Changed += Raise.Event<Action<StatValue>>(StatValue.Zero);
            _playerDeathEventEmitter.PlayerDead -= OnPlayerDead;

            //Assert
            Assert.AreEqual(1, eventCallCount);
        }

        [Test]
        public void HPChangedGivesValueGreaterThanZero_PlayerDeadEventNotCalled() {
            //Arrange
            var eventCallCount = 0;
            var healthStat = Substitute.For<Stat>();
            void OnPlayerDead() { eventCallCount++; }

            _playerDeathEventEmitter = new PlayerDeathEventEmitter(healthStat);
            _playerDeathEventEmitter.PlayerDead += OnPlayerDead;

            //Act
            healthStat.Changed += Raise.Event<Action<StatValue>>(StatValue.Zero);
            _playerDeathEventEmitter.PlayerDead -= OnPlayerDead;

            //Assert
            Assert.AreEqual(0, eventCallCount);
        }
    }

}