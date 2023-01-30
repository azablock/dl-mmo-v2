using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts;
using _Darkland.Sources.Scripts.Equipment;
using _Darkland.Sources.Scripts.Persistence;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using MongoDB.Bson;
using UnityEngine;

namespace _Darkland.Sources.Models.Persistence.DarklandHero {

    public static class DarklandHeroService {

        [Server]
        public static void ServerSaveDarklandHero(GameObject darklandHeroGameObject) {
            var heroName = darklandHeroGameObject.GetComponent<UnitNameBehaviour>().unitName;
            var e = DarklandDatabaseManager
                .darklandHeroRepository
                .FindByName(heroName);

            var vocation = darklandHeroGameObject.GetComponent<DarklandHeroBehaviour>().heroVocation.VocationType;
            e.vocation = vocation.ToString();

            var position = darklandHeroGameObject.GetComponent<IDiscretePosition>().Pos;
            e.posX = position.x;
            e.posY = position.y;
            e.posZ = position.z;

            var statsHolder = darklandHeroGameObject.GetComponent<IStatsHolder>();
            var health = statsHolder.ValueOf(StatId.Health);
            e.health = (int)health;

            var traitValues = statsHolder.TraitStatsValues();
            e.might = (int)traitValues.might;
            e.constitution = (int)traitValues.constitution;
            e.dexterity = (int)traitValues.dexterity;
            e.intellect = (int)traitValues.intellect;
            e.soul = (int)traitValues.soul;

            var xpHolder = darklandHeroGameObject.GetComponent<IXpHolder>();
            e.xp = xpHolder.xp;
            e.level = xpHolder.level;

            var eqHolder = darklandHeroGameObject.GetComponent<IEqHolder>();
            var itemNames = eqHolder
                .Backpack
                .Select(it => it.ItemName)
                .ToList();

            e.itemNames = new List<string>(itemNames);
            
            foreach (var (wearableSlot, wearableItemDef) in eqHolder.EquippedWearables) {
                e.equippedWearables.Add(wearableSlot.ToString(), wearableItemDef.itemDef.ItemName);
            }

            DarklandDatabaseManager
                .darklandHeroRepository
                .ReplaceById(e);
        }

        [Server]
        public static void ServerLoadDarklandHero(GameObject darklandHeroGameObject, string heroName) {
            var darklandHero = darklandHeroGameObject.GetComponent<DarklandHeroBehaviour>();
            var e = DarklandDatabaseManager
                .darklandHeroRepository
                .FindByName(heroName);

            darklandHero.GetComponent<MongoIdHolder>().Set(e.id);

            var pos = new Vector3Int(e.posX, e.posY, e.posZ);
            darklandHero.GetComponent<IDiscretePosition>().Set(pos, true);
            darklandHero.transform.position = pos;

            darklandHero.ServerSetVocation(Enum.Parse<HeroVocationType>(e.vocation));
            darklandHero.GetComponent<UnitNameBehaviour>().ServerSet(heroName);

            var eqHolder = darklandHero.GetComponent<IEqHolder>();
            e.itemNames
                .Select(EqItemsContainer.ItemDef2)
                .ToList()
                .ForEach(it => eqHolder.AddToBackpack(it));
            
            foreach (var (slotName, itemName) in e.equippedWearables) {
                var itemDef = EqItemsContainer.ItemDef2(itemName);
                var wearableItemDef = new WearableItemDef {
                    itemDef = itemDef,
                    wearable = (IWearable)itemDef
                };
                
                eqHolder.EquippedWearables.Add(Enum.Parse<WearableSlot>(slotName), wearableItemDef);
            }

            var statsHolder = darklandHero.GetComponent<IStatsHolder>();
            statsHolder.SetTraitStats(new UnitTraits {
                might = e.might,
                constitution = e.constitution,
                dexterity = e.dexterity,
                intellect = e.intellect,
                soul = e.soul
            });

            //todo MaxHealth samo sie nie powinno wyliczyc? pewnie jeszzce za wczesnie
            statsHolder.Set(StatId.MaxHealth, HeroStatsCalculator.ValueOf(StatId.MaxHealth, statsHolder));
            statsHolder.Set(StatId.Health, e.health);
            statsHolder.Set(StatId.HealthRegain, 1);
            statsHolder.Set(StatId.MovementSpeed, 3);

            var xpHolder = darklandHero.GetComponent<XpHolderBehaviour>();
            xpHolder.ServerInit(e.xp, e.level);
        }

        public static void ServerCreateNewHero(ObjectId darklandAccountId,
                                               string heroName,
                                               HeroVocationType heroVocationType) {
            var darklandHeroEntity = new DarklandHeroEntity {
                name = heroName,
                darklandAccountId = darklandAccountId,
                createDate = DateTime.Now,
                vocation = heroVocationType.ToString(),
                health = 1,
                xp = 0,
                level = 1,
                posX = 0,
                posY = 0,
                posZ = 0,
                might = 1,
                constitution = 1,
                dexterity = 1,
                intellect = 1,
                soul = 1,
                itemNames = new List<string>(),
                equippedWearables = new Dictionary<string, string>()
            };

            DarklandDatabaseManager.darklandHeroRepository.Create(darklandHeroEntity);
        }

    }

}