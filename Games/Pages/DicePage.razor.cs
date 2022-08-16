using Games.Classes;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Games.Pages
{
    public partial class DicePage
    {
        [Inject]
        private ISnackbar Snackbar { get; set; }

        public List<Die> dice = new List<Die>();

        public Dictionary<int, int> result = new Dictionary<int, int>();

        public bool AskForInput = true;

        public int NumberOfSides = 0;
        public int NumberOfDie = 0;
        public int NumberOfRolls = 0;
        public int MaxValueForProgressBars = 5;

        public void CreateDiceListAndDict(int numSides, int numDie)
        {
            for (int i = 0; i < numDie; i++)
            {
                dice.Add(new Die(numSides));
            }

            for (int i = Die._sides[numSides].Min() + (NumberOfDie - 1); i <= Die._sides[numSides].Max() * NumberOfDie; i++)
            {
                result.Add(i, 0);
            }
        }

        public void RollDice(int numSides, int numDie, int numRolls)
        {
            CreateDiceListAndDict(numSides, numDie);

            AskForInput = false;

            for (int i = 0; i < numRolls; i++)
            {
                var roll = 0;

                foreach (Die die in dice)
                {
                    roll += die.Roll();
                }

                result[roll]++;

                MaxValueForProgressBars = Math.Max(MaxValueForProgressBars, result[roll]);
            }
            
            MaxValueForProgressBars += (int)Math.Ceiling(0.10 * MaxValueForProgressBars);
        }

        public string GetPercent(int value)
        {
            var percent = ((double)value / (double)NumberOfRolls) * 100;
            return String.Format("{0:0.00}%", percent);
        }

        public void Submit()
        {
            if (Die._sides.Keys.Contains(NumberOfSides))
            {
                RollDice(NumberOfSides, NumberOfDie, NumberOfRolls);
            }
            else
            {
                Snackbar.Add("The number of sides you have selected is not valid", Severity.Error);
            }
        }

        public void Reset()
        {
            AskForInput = true;
            MaxValueForProgressBars = 5;
            dice = new List<Die>();
            result = new Dictionary<int, int>();
        }
    }
}
