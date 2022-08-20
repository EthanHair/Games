using System.Collections.Concurrent;

namespace Games.Classes
{
    public class RollResponse
    {
        public Guid Id { get; set; }

        public ConcurrentBag<int> rolls { get; set; }

        public RollResponse()
        {
            Id = Guid.NewGuid();
            rolls = new ConcurrentBag<int>();
        }
    }
}