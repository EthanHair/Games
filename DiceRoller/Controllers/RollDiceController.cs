using DiceRoller.Hubs;
using DiceRoller.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace DiceRoller.Controllers
{
    [ApiController]
    [Route("roll")]
    public class RollDiceController : Controller
    {
        private Random random = new Random();

        private Stopwatch stopwatch = new Stopwatch();

        private ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = 8 };

        private DiceRollHub Hub { get; set; }

        public RollDiceController(DiceRollHub hub)
        {
            Hub = hub;
        }

        [HttpPost]
        public async Task<IActionResult> RollDice(RollRequest rollRequest)
        {
            // Send roll started message
            await Hub.RollStarted();

            stopwatch.Reset();
            stopwatch.Start();

            // ConcurrentBag to store all the roll results
            var rolls = new ConcurrentBag<int>();

            // Calculate the number of rolls per batch
            var numberOfRollsPerLoop = (int)Math.Floor((double)(rollRequest.numberRolls / 10));

            // Execute rolls and send 10 batches
            for (int batch = 0; batch < 10; batch++)
            {
                var rollResponse = new RollResponse();

                var loopResult = Parallel.For(0, numberOfRollsPerLoop, options, i =>
                {
                    var roll = 0;

                    for (int j = 0; j < rollRequest.numberDice; j++)
                    {
                        roll += random.Next(rollRequest.numberSides) + 1;
                    }

                    rollResponse.rolls.Add(roll);
                    rolls.Add(roll);
                });

                if (loopResult.IsCompleted)
                {
                    await Hub.SendRolls(rollResponse.rolls);
                }
            }

            // If the number of requested rolls is not divisible by 10, calculate the rolls left, do them, and send them
            var rollsLeftOver = rollRequest.numberRolls - (numberOfRollsPerLoop * 10);

            if (rollsLeftOver > 0)
            {
                var leftoverRollResponse = new DiceRoller.Models.RollResponse();

                var leftoverLoopResult = Parallel.For(0, rollsLeftOver, options, i =>
                {
                    var roll = 0;

                    for (int j = 0; j < rollRequest.numberRolls; j++)
                    {
                        roll += random.Next(rollRequest.numberSides) + 1;
                    }

                    leftoverRollResponse.rolls.Add(roll);
                    rolls.Add(roll);
                });

                if (leftoverLoopResult.IsCompleted)
                {
                    await Hub.SendRolls(leftoverRollResponse.rolls);
                }
            }

            // Send the roll finished message
            stopwatch.Stop();

            await Hub.RollFinished($"{stopwatch.Elapsed.TotalMilliseconds} ms");

            if (rolls.Count == rollRequest.numberRolls)
            {
                return Ok($"It took {stopwatch.Elapsed.TotalMilliseconds} ms to make the requested roll");
            }
            else
            {
                return BadRequest("All the rolls were not executed");
            }
        }
    }
}
