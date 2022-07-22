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

        public string ImageRoute => "Images/" + Name + ".png";
    }
}
