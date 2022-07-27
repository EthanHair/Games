namespace Games.Classes
{
    public interface ICardGame : IGame
    {
        public Dictionary<string, int> SuitValueDict { get; }

        public Dictionary<string, int> RankValueDict { get; }

        public Deck _deck { get; set; }
    }
}
