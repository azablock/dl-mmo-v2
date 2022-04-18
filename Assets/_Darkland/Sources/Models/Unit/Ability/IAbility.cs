using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Scripts.Unit;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Ability {
    
    public interface IGameplayAbilityDataFactory<out T> {
        T Create(GameObject gameObject);
        bool CanCreate(GameObject gameObject);
    }

    public class FireballGameplayAbilityDataFactory : IGameplayAbilityDataFactory<FireballData> {

        public FireballData Create(GameObject gameObject) {
            return new FireballData();
        }

        public bool CanCreate(GameObject gameObject) {
            var targetNetworkIdentityHolderBehaviour = gameObject.GetComponent<TargetNetworkIdentityHolderBehaviour>();

            return targetNetworkIdentityHolderBehaviour
                   && targetNetworkIdentityHolderBehaviour.ServerTargetNetworkIdentity()
                   && targetNetworkIdentityHolderBehaviour.GetComponent<HpBehaviour>();
        }
    }

    public interface IGameplayAbility {
        IEnumerator<float> Activate();
        bool CanActivate();
    }

    public class FireballGameplayAbility : IGameplayAbility {

        public IEnumerator<float> Activate() {
            yield break;
        }

        public bool CanActivate() {
            return false;
        }
    }

    public class GameplayAbilitySystem : MonoBehaviour {

        public List<IGameplayAbility> gameplayAbilities;
        
        public void Use<T>() where T : IGameplayAbility {
            var gameplayAbility = gameplayAbilities.FirstOrDefault(it => it is T);

            if (gameplayAbility != null && gameplayAbility.CanActivate()) {
                gameplayAbility.Activate();
            }
        }
    }
    
    

}