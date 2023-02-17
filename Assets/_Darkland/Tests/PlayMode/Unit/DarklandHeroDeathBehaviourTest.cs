using System.Collections;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Tests.Common;
using Mirror;
using UnityEngine.TestTools;

namespace _Darkland.Tests.PlayMode.Unit {

    public class DarklandHeroDeathBehaviourTest : MirrorPlayModeTest {

        private IDeathEventEmitter _deathEventEmitter;
        private DarklandHeroDeathBehaviour _darklandHeroDeathBehaviour;

        [UnitySetUp]
        public override IEnumerator UnitySetUp() {
            yield return base.UnitySetUp();
        
            NetworkServer.Listen(1);
            ConnectHostClientBlockingAuthenticatedAndReady();
            CreateNetworkedAndSpawnPlayer(out var gameObject, out _, NetworkServer.localConnection);

            var a = gameObject.AddComponent<DarklandUnitDeathBehaviour>();
            
            _darklandHeroDeathBehaviour = gameObject.AddComponent<DarklandHeroDeathBehaviour>();
        }
        
        [UnityTest]
        public IEnumerator A() {
            //Arrange
            // _darklandUnitDeathBehaviour.
    
            //Act
            
            //Assert
            yield return null;
        }
    }

}