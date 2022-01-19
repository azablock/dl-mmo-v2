using System;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Hp;
using NSubstitute;
using NUnit.Framework;

namespace _Darkland.Tests.EditMode {

    public class PlayerDeathEventEmitterTest {

        private IPlayerDeathEventEmitter _playerDeathEventEmitter;

        [Test]
        public void HpChangedGivesValueEqualToZero_PlayerDeadEventCalledOnce() {
            //Arrange
            var eventCallCount = 0;
            var hpHolder = Substitute.For<IHpEventsHolder>();
            void OnPlayerDead() { eventCallCount++; }

            _playerDeathEventEmitter = new PlayerDeathEventEmitter(hpHolder);
            _playerDeathEventEmitter.PlayerDead += OnPlayerDead;

            //Act
            hpHolder.HpChanged += Raise.Event<Action<int>>(0);
            _playerDeathEventEmitter.PlayerDead -= OnPlayerDead;

            //Assert
            Assert.AreEqual(1, eventCallCount);
        }

        [Test]
        public void HPChangedGivesValueGreaterThanZero_PlayerDeadEventNotCalled() {
            //Arrange
            var eventCallCount = 0;
            var hpHolder = Substitute.For<IHpEventsHolder>();
            void OnPlayerDead() { eventCallCount++; }

            _playerDeathEventEmitter = new PlayerDeathEventEmitter(hpHolder);
            _playerDeathEventEmitter.PlayerDead += OnPlayerDead;

            //Act
            hpHolder.HpChanged += Raise.Event<Action<int>>(1);
            _playerDeathEventEmitter.PlayerDead -= OnPlayerDead;

            //Assert
            Assert.AreEqual(0, eventCallCount);
        }
    }

}