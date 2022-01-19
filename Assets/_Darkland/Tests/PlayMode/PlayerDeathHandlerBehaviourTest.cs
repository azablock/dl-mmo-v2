using System.Collections;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Tests.Common;
using Mirror;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode {

    [TestFixture]
    public class PlayerDeathHandlerBehaviourTest : MirrorPlayModeTest {

        private PlayerDeathHandlerBehaviour _behaviour;
        
        [UnitySetUp]
        public override IEnumerator UnitySetUp() {
            yield return base.UnitySetUp();
        
            NetworkServer.Listen(1);
            ConnectHostClientBlockingAuthenticatedAndReady();
            CreateNetworkedAndSpawnPlayer(out _, out _, out _behaviour, NetworkServer.localConnection);
        }
        
        
        
    }

}