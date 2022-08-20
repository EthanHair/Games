using System.Collections.Concurrent;

namespace DiceRoller.Models
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
