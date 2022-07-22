﻿using Games.Classes;

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

        public Deck _deck { get; } = new Deck();

        public List<Card> DealerHand = new List<Card>();
        public int GetDealerHandScore()
        {
            int score = 0;
            foreach (Card card in DealerHand)
            {
                score += RankValueDict[card.Rank];
            }
            return score;
        }
        public ScoreState CheckDealerScore()
        {
            int dealerScore = GetDealerHandScore();
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

        public List<Card> PlayerHand = new List<Card>();

        public int GetPlayerHandScore()
        {
            int score = 0;
            foreach (Card card in PlayerHand)
            {
                score += RankValueDict[card.Rank];
            }
            return score;
        }

        public ScoreState CheckPlayerScore()
        {
            int playerScore = GetPlayerHandScore();
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

        public bool playerStayed = false;

        // Playing Game Methods
        public void CheckGame()
        {
            var dealerResult = CheckDealerScore();
            var playerResult = CheckPlayerScore();
            bool dealerWin = false;
            bool playerWin = false;

            // Added this if under then compare scores stuff
            if (dealerResult == ScoreState.Under && playerResult == ScoreState.Under && playerStayed)
            {
                if (GetDealerHandScore() < GetPlayerHandScore())
                {
                    GameOver("Player");
                }
                else
                {
                    GameOver("Dealer");
                }
            }

            if (playerResult == ScoreState.Bust ||
                dealerResult == ScoreState.Win ||
                (playerResult == ScoreState.Bust && playerResult == ScoreState.Bust))
            {
                dealerWin = true;
            }
            else if (playerResult == ScoreState.Win ||
                (dealerResult == ScoreState.Bust && playerResult == ScoreState.Under))
            {
                playerWin = true;
            }

            if (dealerWin)
            {
                GameOver("Dealer");
            }
            else if (playerWin)
            {
                GameOver("Player");
            }
            else
            {
                UpdateUI();
            }
        }

        public void DealCard(string dealee)
        {
            if (dealee == "Dealer")
            {
                DealerHand.Add(_deck.DrawCard());
            }
            if (dealee == "Player")
            {
                PlayerHand.Add(_deck.DrawCard());
            }
            UpdateUI();
        }

        public void StartGame()
        {
            for (int i = 0; i < 5; i++)
            {
                _deck.Shuffle();
            }
            DealCard("Player");
            DealCard("Dealer");
            DealCard("Player");
            DealCard("Dealer");
        }

        public void PlayerHit()
        {
            DealCard("Player");
            CheckGame();

            if (CheckDealerScore() == ScoreState.DealerHitAgain && CheckPlayerScore() != ScoreState.Bust)
            {
                DealCard("Dealer");
            }
            CheckGame();
        }

        public async Task PlayerStay()
        {
            playerStayed = true;
            await MakeDealerFinish();
            if (CheckDealerScore() == ScoreState.Bust)
            {
                GameOver("Player");
            }
            else if (GetDealerHandScore() < GetPlayerHandScore())
            {
                GameOver("Player");
            }
            else
            {
                GameOver("Dealer");
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
    }
}
