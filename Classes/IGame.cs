namespace Games.Classes
{
    public interface IGame
    {
        public string Name { get; }

        public string Description { get; }

        public string Route { get; }

        public string ImageRoute { get; }
    }
}
