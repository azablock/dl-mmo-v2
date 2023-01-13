using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts;
using _Darkland.Sources.Scripts.Persistence;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Persistence {

    public static class DarklandHeroService {

        [Server]
        public static void ServerSaveDarklandHero(GameObject darklandHeroGameObject) {
            var darklandHero = darklandHeroGameObject.GetComponent<DarklandHero>();
            var entity = DarklandDatabaseManager
                .darklandHeroRepository
                .FindByName(darklandHero.heroName);

            var position = darklandHero.GetComponent<IDiscretePosition>().Pos;
            entity.posX = position.x;
            entity.posY = position.y;
            entity.posZ = position.z;

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

            darklandHero.heroName = heroName;

            var statsHolder = darklandHero.GetComponent<IStatsHolder>();
            statsHolder.Stat(StatId.MaxHealth).Set(1);
            statsHolder.Stat(StatId.Health).Set(1);
        }
    }

}