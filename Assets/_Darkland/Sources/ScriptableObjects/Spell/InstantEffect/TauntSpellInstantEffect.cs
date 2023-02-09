using _Darkland.Sources.Models.Interaction;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(TauntSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(TauntSpellInstantEffect))]
    public class TauntSpellInstantEffect : SpellInstantEffect {

        public override void Process(GameObject caster) {
            var casterIdentity = caster.GetComponent<NetworkIdentity>();
            var targetNetIdHolder = caster.GetComponent<ITargetNetIdHolder>();
            targetNetIdHolder.TargetNetIdentity.GetComponent<ITargetNetIdHolder>().Set(casterIdentity.netId);
        }

    }

}