namespace Games.Classes
{
    public class Die
    {
        public Die()
        {
            NumberOfSides = 6;
        }

        public Die(int numSides)
        {
            NumberOfSides = numSides;
        }

        public static Dictionary<int, int[]> _sides = 
                   new Dictionary<int, int[]>() { {4, new int[] { 1, 2, 3, 4 } }, 
                                                  {6, new int[] { 1, 2, 3, 4, 5, 6 }},
                                                  {8, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }},
                                                  {10, new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 } },
                                                  {12, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }},
                                                  {20, new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 }}
                                                };

        public int NumberOfSides;

        public Random rnd = new Random();

        public int Roll()
        {
            return _sides[NumberOfSides][rnd.Next(NumberOfSides)];
        }
    }
}
