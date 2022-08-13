using Games.Classes;

namespace Games.Classes.GameClasses
{
    public class Solitaire : IGame, ICardGame
    {
        public string Name { get; } = "Solitaire";

        public string Description { get; } = "A classic single-player game";

        public string Route { get; } = "/solitaire";

        public string ImageRoute { get; } = "/Images/solitaire.jpg";

        public Dictionary<string, int> SuitValueDict { get; } = new Dictionary<string, int>() { };

        public Dictionary<string, int> RankValueDict { get; } = new Dictionary<string, int>() { };

        public Deck _deck { get; set; } = new Deck();

        private Hand _hand1 = new Hand("hand1");
        private Hand _hand2 = new Hand("hand2");
        private Hand _hand3 = new Hand("hand3");
        private Hand _hand4 = new Hand("hand4");
        private Hand _hand5 = new Hand("hand5");
        private Hand _hand6 = new Hand("hand6");
        private Hand _hand7 = new Hand("hand7");
        private Hand _spadeHand = new Hand("spadeHand");
        private Hand _heartHand = new Hand("heartHand");
        private Hand _clubHand = new Hand("vlubHand");
        private Hand _diamondHand = new Hand("diamondHand");
        private Hand _drawHand = new Hand("drawHand");
        private Hand _handHand = new Hand("handHand");

        public Dictionary<string, Hand> _handsDict { get; } = new Dictionary<string, Hand>();

        public Solitaire()
        {
            for (int i = 0; i < 5; i++)
            {
                _deck.Shuffle();
            }
            // Deal first row
            _deck.DrawCard(ref _hand1);
            _deck.DrawCard(ref _hand2);
            _deck.DrawCard(ref _hand3);
            _deck.DrawCard(ref _hand4);
            _deck.DrawCard(ref _hand5);
            _deck.DrawCard(ref _hand6);
            _deck.DrawCard(ref _hand7);
            // Deal second row
            _deck.DrawCard(ref _hand2);
            _deck.DrawCard(ref _hand3);
            _deck.DrawCard(ref _hand4);
            _deck.DrawCard(ref _hand5);
            _deck.DrawCard(ref _hand6);
            _deck.DrawCard(ref _hand7);
            // Deal third row
            _deck.DrawCard(ref _hand3);
            _deck.DrawCard(ref _hand4);
            _deck.DrawCard(ref _hand5);
            _deck.DrawCard(ref _hand6);
            _deck.DrawCard(ref _hand7);
            // Deal fouth row
            _deck.DrawCard(ref _hand4);
            _deck.DrawCard(ref _hand5);
            _deck.DrawCard(ref _hand6);
            _deck.DrawCard(ref _hand7);
            // Deal fifth row
            _deck.DrawCard(ref _hand5);
            _deck.DrawCard(ref _hand6);
            _deck.DrawCard(ref _hand7);
            // Deal sixth row
            _deck.DrawCard(ref _hand6);
            _deck.DrawCard(ref _hand7);
            // Deal seventh row
            _deck.DrawCard(ref _hand7);

            foreach (Card card in _deck.Cards.Reverse<Card>())
            {
                _drawHand.Cards.Add(card);
            }

            _handsDict = new Dictionary<string, Hand> { { "stack1", _hand1 },
                                                       { "stack2", _hand2 },
                                                       { "stack3", _hand3 },
                                                       { "stack4", _hand4 },
                                                       { "stack5", _hand5 },
                                                       { "stack6", _hand6 },
                                                       { "stack7", _hand7 },
                                                       { "spadeStack", _spadeHand },
                                                       { "heartStack", _heartHand },
                                                       { "clubStack", _clubHand },
                                                       { "diamondStack", _diamondHand },
                                                       { "drawStack", _drawHand },
                                                       { "handStack", _handHand }, };
        }
    }
}
