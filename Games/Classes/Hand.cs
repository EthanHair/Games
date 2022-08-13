namespace Games.Classes
{
    public class Hand
    {
        public List<Card> Cards = new List<Card>();

        public string HandHolder;

        public Hand(string handHolder)
        {
            HandHolder = handHolder;
        }
    }
}
