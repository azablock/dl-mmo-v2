namespace _Darkland.Sources.Models.Unit {

    public struct ExperienceLevelChangeEvent {

        public int level;
        public int currentXp;
        public int currentLevelXpCap;
        public int nextLevelXpCap;

    }

    public struct XpChangeEvent {

        public int xp;

    }

}