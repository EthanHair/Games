using static MudBlazor.CategoryTypes;

namespace Games.Classes
{
    public class Deck
    {
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

            _autoUseDiscardPile = true;
        }

        public Deck(bool autoUseDiscardPile)
        {
            foreach (string suit in Suits)
            {
                foreach (string rank in Ranks)
                {
                    Card card = new Card(rank, suit);
                    Cards.Add(card);
                }
            }

            _autoUseDiscardPile = autoUseDiscardPile;
        }

        public static string[] Suits = new string[] { "Spades", "Hearts", "Clubs", "Diamonds"};
        public static string[] Ranks = new string[] { "Ace", "King", "Queen", "Jack", "10", "9", "8", "7", "6", "5", "4", "3", "2" };
        private static Random rng = new Random();
        private bool _autoUseDiscardPile;
        public List<Card> Cards { get; set; } = new List<Card>();

        public List<Card> DrawnCards { get; set;} = new List<Card>();

        public List<Card> DiscardPile { get; set; } = new List<Card>();

        public static Card CardBack = new Card("back", "card");

        public void Shuffle()
        {
            Cards = Cards.OrderBy(a => rng.Next()).ToList();
        }

        public void DrawCard(ref Hand dealLocation)
        {
            if (Cards.Count != 0)
            {
                var drawnCard = Cards[0];
                Cards.Remove(drawnCard);
                DrawnCards.Add(drawnCard);
                dealLocation.Cards.Add(drawnCard);
            }
            else if (_autoUseDiscardPile)
            {
                if (DiscardPile.Count != 0)
                {
                    AddDiscardPileBackToDeck();
                    var drawnCard = Cards[0];
                    Cards.Remove(drawnCard);
                    DrawnCards.Add(drawnCard);
                    dealLocation.Cards.Add(drawnCard);
                }
                else
                {
                    throw new InvalidDrawException("Both the deck and the discard pile are empty");
                }
            }
            else
            {
                throw new InvalidDrawException("The deck is empty and the deck is set to not use the discard pile");
            }
        }

        public Card DrawCard()
        {
            if (Cards.Count != 0)
            {
                var drawnCard = Cards[0];
                Cards.Remove(drawnCard);
                DrawnCards.Add(drawnCard);
                return drawnCard;
            }
            else if (_autoUseDiscardPile)
            {
                if (DiscardPile.Count != 0)
                {
                    AddDiscardPileBackToDeck();
                    var drawnCard = Cards[0];
                    Cards.Remove(drawnCard);
                    DrawnCards.Add(drawnCard);
                    return drawnCard;
                }
                else
                {
                    throw new InvalidDrawException("Both the deck and the discard pile are empty");
                }
            }
            else
            {
                throw new InvalidDrawException("The deck is empty and the deck is set to not use the discard pile");
            }
        }

        public void RecollectCards()
        {
            foreach (Card card in DrawnCards)
            {
                card.IsFlipped = false;
                Cards.Add(card);
            }

            DrawnCards.Clear();

            for (int i = 0; i < 5; i++)
            {
                Shuffle();
            }
        }

        public void AddDiscardPileBackToDeck()
        {
            foreach (Card card in DiscardPile)
            {
                card.IsFlipped = false;
                Cards.Add(card);
            }

            DiscardPile.Clear();

            for (int i = 0; i < 5; i++)
            {
                Shuffle();
            }
        }
    }

    class InvalidDrawException : Exception
    {
        public InvalidDrawException() { }

        public InvalidDrawException(string reason) : base(String.Format("Invalid Draw: {0}", reason)) { }
    }
}
