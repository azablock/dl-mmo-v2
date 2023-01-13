using System;
using Mirror;

namespace _Darkland.Sources.Scripts.Presentation.Unit {

    public class DarklandUnit : NetworkBehaviour {
        
        public event Action ClientStarted;
        
        public override void OnStartClient() {
            ClientStarted?.Invoke();
        }
    }

}