using Games.Classes;

namespace Games.Classes.GameClasses
{
    public class Blackjack : IGame, ICardGame
    {
        // Game Details
        public string Name { get; } = "Blackjack";
        public string Description { get; } = "A casino favorite in which the dealer and player take turns getting cards trying to add up to 21";
        public string Route { get; } = "/blackjack";
        public string ImageRoute { get; } = "Images/blackjack.png";

        // Events
        public event EventHandler<GameEventArgs> GameOverEvent;

        public void GameOver(string winner)
        {
            GameOverEvent?.Invoke(this, new GameEventArgs(winner));
        }

        public event EventHandler<GameEventArgs> UpdateUIEvent;

        public void UpdateUI()
        {
            UpdateUIEvent?.Invoke(this, new GameEventArgs("Update UI"));
        }

        // Game Set Up
        public enum ScoreState
        {
            Win,
            Bust,
            Under,
            DealerHitAgain
        }
        public Dictionary<string, int> SuitValueDict { get; } = new Dictionary<string, int>();
        public Dictionary<string, int> RankValueDict { get; } = new Dictionary<string, int>() { {"King", 10 },
                                                                                                {"Queen", 10 },
                                                                                                {"Jack", 10 },
                                                                                                {"10", 10 },
                                                                                                {"9", 9 },
                                                                                                {"8", 8 },
                                                                                                {"7", 7 },
                                                                                                {"6", 6 },
                                                                                                {"5", 5 },
                                                                                                {"4", 4 },
                                                                                                {"3", 3 },
                                                                                                {"2", 2 },
                                                                                                {"Ace", 1 } };

        public Deck _deck { get; set; } = new Deck();

        public bool playerStayed = false;

        // Dealer
        public List<Card> DealerHand = new List<Card>();

        public ScoreState CheckDealerScore()
        {
            int dealerScore = GetScore("Dealer");
            if (dealerScore > 21)
            {
                return ScoreState.Bust;
            }
            if (dealerScore == 21)
            {
                return ScoreState.Win;
            }
            if (dealerScore < 16)
            {
                return ScoreState.DealerHitAgain;
            }
            else
            {
                return ScoreState.Under;
            }
        }

        // Player
        public List<Card> PlayerHand = new List<Card>();

        public ScoreState CheckPlayerScore()
        {
            int playerScore = GetScore("Player");
            if (playerScore > 21)
            {
                return ScoreState.Bust;
            }
            if (playerScore == 21)
            {
                return ScoreState.Win;
            }
            else
            {
                return ScoreState.Under;
            }
        }

        public Dictionary<string, List<Card>> HandDict { get; set; }

        // Contructor
        public Blackjack()
        {
            HandDict = new Dictionary<string, List<Card>>() { { "Player", PlayerHand }, { "Dealer", DealerHand } };
        }

        public int GetScore(string player)
        {
            var scoreList = new List<int>();
            foreach (Card card in HandDict[player])
            {
                scoreList.Add(RankValueDict[card.Rank]);
            }

            if (scoreList.Contains(1))
            {
                if (scoreList.Sum() + 10 > 21)
                {
                    return scoreList.Sum();
                }
                else
                {
                    return scoreList.Sum() + 10;
                }
            }
            else
            {
                return scoreList.Sum();
            }
        }

        // Playing Game Methods
        public async Task CheckGame()
        {
            var playerResult = CheckPlayerScore();

            if (playerResult == ScoreState.Bust)
            {
                GameOver("Dealer Won");
            }
            else if (playerResult == ScoreState.Win)
            {
                await PlayerStay();
                GameOver("Player Won");
            }
            else if (playerResult == ScoreState.Under)
            {
                UpdateUI();
            }
        }

        public void DealCard(string dealee)
        {
            HandDict[dealee].Add(_deck.DrawCard());
        }

        public void StartGame()
        {
            for (int i = 0; i < 5; i++)
            {
                _deck.Shuffle();
            }

            for (int i = 0; i < 2; i++)
            {
                foreach (string player in HandDict.Keys)
                {
                    DealCard(player);
                }
            }
            if (CheckPlayerScore() == ScoreState.Win)
            {
                GameOver("Player Win");
            }
        }

        public async Task PlayerHit()
        {
            DealCard("Player");
            await CheckGame();
        }

        public async Task PlayerStay()
        {
            playerStayed = true;
            await MakeDealerFinish();

            var dealerScore = GetScore("Dealer");
            var playerScore = GetScore("Player");

            if (CheckDealerScore() == ScoreState.Bust || dealerScore < playerScore)
            {
                GameOver("Player Won");
            }
            else if (dealerScore > playerScore)
            {
                GameOver("Dealer Won");
            }
            else
            {
                GameOver("Push");
            }
        }

        public Task MakeDealerFinish()
        {
            while (CheckDealerScore() == ScoreState.DealerHitAgain)
            {
                DealCard("Dealer");
            }
            return Task.CompletedTask;
        }

        public void NewGame()
        {
            _deck.RecollectCards();
            foreach (string player in HandDict.Keys)
            {
                if (HandDict[player] is not null)
                {
                    HandDict[player].Clear();
                }
            }
            StartGame();
        }
    }
}
