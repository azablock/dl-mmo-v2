using System;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts;
using _Darkland.Sources.Scripts.Persistence;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Persistence {

    public static class DarklandHeroService {

        [Server]
        public static void ServerSaveDarklandHero(GameObject darklandHeroGameObject) {
            var heroName = darklandHeroGameObject.GetComponent<UnitNameBehaviour>().unitName;
            var entity = DarklandDatabaseManager
                .darklandHeroRepository
                .FindByName(heroName);

            var position = darklandHeroGameObject.GetComponent<IDiscretePosition>().Pos;
            entity.posX = position.x;
            entity.posY = position.y;
            entity.posZ = position.z;

            var (health, maxHealth) = darklandHeroGameObject.GetComponent<IStatsHolder>().Values(StatId.Health, StatId.MaxHealth);
            entity.health = (int) health;
            entity.maxHealth = (int) maxHealth;

            var xpHolder = darklandHeroGameObject.GetComponent<IXpHolder>();
            var xp = xpHolder.xp;
            entity.xp = xp;
            entity.level = xpHolder.level;

            DarklandDatabaseManager
                .darklandHeroRepository
                .ReplaceById(entity);
        }

        [Server]
        public static void ServerLoadDarklandHero(GameObject darklandHeroGameObject, string heroName) {
            var darklandHero = darklandHeroGameObject.GetComponent<DarklandHero>();
            var entity = DarklandDatabaseManager
                .darklandHeroRepository
                .FindByName(heroName);

            darklandHero.GetComponent<MongoIdHolder>().ServerSetMongoId(entity.id);

            var pos = new Vector3Int(entity.posX, entity.posY, entity.posZ);
            darklandHero.GetComponent<IDiscretePosition>().Set(pos, true);
            darklandHero.transform.position = pos;

            darklandHero.GetComponent<UnitNameBehaviour>().ServerSet(heroName);

            var statsHolder = darklandHero.GetComponent<IStatsHolder>();
            statsHolder.Stat(StatId.MaxHealth).Set(entity.maxHealth);
            statsHolder.Stat(StatId.Health).Set(entity.health);
            statsHolder.Stat(StatId.HealthRegain).Set(1);
            statsHolder.Stat(StatId.MovementSpeed).Set(4);

            var xpHolder = darklandHero.GetComponent<XpHolderBehaviour>();
            xpHolder.ServerInit(entity.xp, entity.level);
        }
    }

}