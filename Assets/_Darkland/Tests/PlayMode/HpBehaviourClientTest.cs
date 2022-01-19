using System.Collections;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Tests.Common;
using Mirror;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode {

    internal class ClientHpChangeListenerBehaviour : NetworkBehaviour {

        public int clientHpChangedCallCount { get; private set; }
        public int clientMaxHpChangedCallCount { get; private set; }
        
        [Client]
        public void ClientConnectWithHpBehaviour(HpBehaviour hpBehaviour) {
            hpBehaviour.ClientHpChanged += HpBehaviourOnClientHpChanged;
            hpBehaviour.ClientMaxHpChanged += HpBehaviourOnClientMaxHpChanged;
        }  

        [Client]
        public void ClientDisconnectWithHpBehaviour(HpBehaviour hpBehaviour) {
            hpBehaviour.ClientHpChanged -= HpBehaviourOnClientHpChanged;
            hpBehaviour.ClientMaxHpChanged -= HpBehaviourOnClientMaxHpChanged;
        }

        [Client]
        private void HpBehaviourOnClientHpChanged(int obj) {
            clientHpChangedCallCount++;
        }

        [Client]
        private void HpBehaviourOnClientMaxHpChanged(int obj) {
            clientMaxHpChangedCallCount++;
        }
    }

    [TestFixture]
    public class HpBehaviourClientTest : MirrorPlayModeTest {

        private HpBehaviour _hpBehaviour;
        private ClientHpChangeListenerBehaviour _clientHpChangeListenerBehaviour;

        [UnitySetUp]
        public override IEnumerator UnitySetUp() {
            yield return base.UnitySetUp();

            NetworkServer.Listen(2);
            ConnectHostClientBlockingAuthenticatedAndReady();
            CreateNetworkedAndSpawnPlayer(out _, out _, out _hpBehaviour, NetworkServer.localConnection);
            
            CreateLocalConnectionPair(out var connectionToClient, out var _);
            CreateNetworkedAndSpawnPlayer(out _, out _, out _clientHpChangeListenerBehaviour, connectionToClient);
            
            _clientHpChangeListenerBehaviour.ClientConnectWithHpBehaviour(_hpBehaviour);
        }

        [UnityTearDown]
        public override IEnumerator UnityTearDown() {
            _clientHpChangeListenerBehaviour.ClientDisconnectWithHpBehaviour(_hpBehaviour);
            
            yield return base.UnityTearDown();
        }
        
        [UnityTest]
        public IEnumerator ServerChangeHp_ClientHpChangedCalledOnce() {
            //given
            
            //when
            _hpBehaviour.ServerChangeMaxHp(10);
            _hpBehaviour.ServerChangeHp(2);

            //then
            yield return null;
            Assert.AreEqual(1, _clientHpChangeListenerBehaviour.clientHpChangedCallCount);
        }      

        [UnityTest]
        public IEnumerator ServerChangeMaxHp_ClientMaxHpChangedCalledOnce() {
            //given
            
            //when
            _hpBehaviour.ServerChangeMaxHp(10);

            //then
            yield return null;
            Assert.AreEqual(1, _clientHpChangeListenerBehaviour.clientMaxHpChangedCallCount);
        }

    }

}