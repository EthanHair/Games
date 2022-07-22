using static MudBlazor.CategoryTypes;

namespace Games.Classes
{
    public class Deck
    {
        public static string[] Suits = new string[] { "Spades", "Hearts", "Clubs", "Diamonds"};
        public static string[] Ranks = new string[] { "Ace", "King", "Queen", "Jack", "10", "9", "8", "7", "6", "5", "4", "3", "2" };
        private static Random rng = new Random();
        public List<Card> Cards { get; set; } = new List<Card>();

        public static Card CardBack = new Card("back", "card");

        public Deck()
        {
            foreach (string suit in Suits)
            {
                foreach (string rank in Ranks)
                {
                    Card card = new Card(rank, suit);
                    Cards.Add(card);
                }
            }
        }
        public void Shuffle()
        {
            Cards = Cards.OrderBy(a => rng.Next()).ToList();
        }

        public Card DrawCard()
        {
            if (Cards.Count != 0)
            {
                var drawnCard = Cards[0];
                Cards.Remove(drawnCard);
                return drawnCard;
            }
            else
            {
                // Maybe create a custom exception and then need to wrap every draw call in a catch block and handle if its empty
                return Deck.CardBack;
            }
        }
    }
}
