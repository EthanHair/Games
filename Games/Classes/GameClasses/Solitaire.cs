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

        private Stack<Card> _stack1 = new Stack<Card>();
        private Stack<Card> _stack2 = new Stack<Card>();
        private Stack<Card> _stack3 = new Stack<Card>();
        private Stack<Card> _stack4 = new Stack<Card>();
        private Stack<Card> _stack5 = new Stack<Card>();
        private Stack<Card> _stack6 = new Stack<Card>();
        private Stack<Card> _stack7 = new Stack<Card>();
        private Stack<Card> _spadeStack = new Stack<Card>();
        private Stack<Card> _heartStack = new Stack<Card>();
        private Stack<Card> _clubStack = new Stack<Card>();
        private Stack<Card> _diamondStack = new Stack<Card>();
        private Stack<Card> _drawStack = new Stack<Card>();
        private Stack<Card> _handStack = new Stack<Card>();

        public Dictionary<string, Stack<Card>> _stacksDict { get; } = new Dictionary<string, Stack<Card>>();

        public Solitaire()
        {
            for (int i = 0; i < 5; i++)
            {
                _deck.Shuffle();
            }
            // Deal first row
            _stack1.Push(_deck.DrawCard());
            _stack2.Push(_deck.DrawCard());
            _stack3.Push(_deck.DrawCard());
            _stack4.Push(_deck.DrawCard());
            _stack5.Push(_deck.DrawCard());
            _stack6.Push(_deck.DrawCard());
            _stack7.Push(_deck.DrawCard());
            // Deal second row
            _stack2.Push(_deck.DrawCard());
            _stack3.Push(_deck.DrawCard());
            _stack4.Push(_deck.DrawCard());
            _stack5.Push(_deck.DrawCard());
            _stack6.Push(_deck.DrawCard());
            _stack7.Push(_deck.DrawCard());
            // Deal third row
            _stack3.Push(_deck.DrawCard());
            _stack4.Push(_deck.DrawCard());
            _stack5.Push(_deck.DrawCard());
            _stack6.Push(_deck.DrawCard());
            _stack7.Push(_deck.DrawCard());
            // Deal fouth row
            _stack4.Push(_deck.DrawCard());
            _stack5.Push(_deck.DrawCard());
            _stack6.Push(_deck.DrawCard());
            _stack7.Push(_deck.DrawCard());
            // Deal fifth row
            _stack5.Push(_deck.DrawCard());
            _stack6.Push(_deck.DrawCard());
            _stack7.Push(_deck.DrawCard());
            // Deal sixth row
            _stack6.Push(_deck.DrawCard());
            _stack7.Push(_deck.DrawCard());
            // Deal seventh row
            _stack7.Push(_deck.DrawCard());

            foreach (Card card in _deck.Cards.Reverse<Card>())
            {
                _drawStack.Push(card);
            }

            _stacksDict = new Dictionary<string, Stack<Card>> { { "stack1", _stack1 },
                                                                { "stack2", _stack2 },
                                                                { "stack3", _stack3 },
                                                                { "stack4", _stack4 },
                                                                { "stack5", _stack5 },
                                                                { "stack6", _stack6 },
                                                                { "stack7", _stack7 },
                                                                { "spadeStack", _spadeStack },
                                                                { "heartStack", _heartStack },
                                                                { "clubStack", _clubStack },
                                                                { "diamondStack", _diamondStack },
                                                                { "drawStack", _drawStack },
                                                                { "handStack", _handStack }, };
        }
    }
}
