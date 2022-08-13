namespace Games.Classes
{
    public class Card
    {
        public Card(string rank, string suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public string Name => Rank + "of" + Suit;

        public string Rank { get; set; }

        public string Suit { get; set; }

        public bool IsFlipped = false;

        public string ImageRoute => (IsFlipped ? "Images/" + Name + ".png" : "Images/backofcard.png");

        public void FlipCard()
        {
            IsFlipped = !IsFlipped;
        }
    }
}
