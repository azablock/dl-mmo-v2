namespace _Darkland.Sources.Models.Persistence.GameReport {

    public class GameReportRepository : Repository<GameReportEntity> {

        public override string collectionName => "gameReport";
    }

}