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

        public int NumberOfSides;

        public Random rnd = new Random();

        public int Roll()
        {
            return rnd.Next(NumberOfSides) + 1;
        }
    }
}
