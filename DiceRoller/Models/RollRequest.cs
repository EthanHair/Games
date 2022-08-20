using System.ComponentModel.DataAnnotations;

namespace DiceRoller.Models
{
    public class RollRequest
    {
        public int numberDice { get; set; }

        public int numberSides { get; set; }

        public int numberRolls { get; set; }
    }
}
