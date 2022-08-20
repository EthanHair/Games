using DiceRoller.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace DiceRoller.Hubs
{
    public class DiceRollHub : Hub
    {
        private readonly IHubContext<DiceRollHub> _context;

        public DiceRollHub(IHubContext<DiceRollHub> context)
        {
            _context = context;
        }

        public async Task SendRolls(ConcurrentBag<int> rolls)
        {
            await _context.Clients.All.SendAsync("SendRolls", rolls);
        }

        public async Task RollStarted()
        {
            await _context.Clients.All.SendAsync("RollStarted", "The roll has started");
        }

        public async Task RollFinished(string elapsedTime)
        {
            await _context.Clients.All.SendAsync("RollFinished", elapsedTime);
        }

    }
}
