using Games.Classes;
using Games.Classes.GameClasses;
using static Games.Classes.GameClasses.Blackjack;

namespace GamesTests.GamesTests
{
    public class BlackjackTests
    {
        [Fact]
        public void New_Blackjack_Game_After_Start_Is_Dealt_Correctly()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.StartGame();
            // Assert
            Assert.Equal(2, sut.PlayerHand.Count);
            Assert.Equal(2, sut.DealerHand.Count);
            Assert.Equal(48, sut._deck.Cards.Count);
        }

        [Fact]
        public void On_PlayerScore_21_CheckPlayerScore_Returns_ScoreStateWin()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Ace", "Spades"));
            // Assert
            Assert.Equal(ScoreState.Win, sut.CheckPlayerScore());
        }

        [Fact]
        public void On_PlayerScore_Under_21_CheckPlayerScore_Returns_ScoreStateUnder()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Ace", "Spades"));
            // Assert
            Assert.Equal(ScoreState.Under, sut.CheckPlayerScore());
        }

        [Fact]
        public void On_PlayerScore_Over_21_CheckPlayerScore_Returns_ScoreStateBust()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Jack", "Spades"));
            // Assert
            Assert.Equal(ScoreState.Bust, sut.CheckPlayerScore());
        }

        [Fact]
        public void On_DealerScore_21_CheckDealerScore_Returns_ScoreStateWin()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Ace", "Spades"));
            // Assert
            Assert.Equal(ScoreState.Win, sut.CheckDealerScore());
        }

        [Fact]
        public void On_DealerScore_Under_16_CheckDealerScore_Returns_ScoreStatedealerHitAgain()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("2", "Spades"));
            // Assert
            Assert.Equal(ScoreState.DealerHitAgain, sut.CheckDealerScore());
        }

        [Fact]
        public void On_DealerScore_Over_16_And_Under_21_CheckDealerScore_Returns_ScoreStateUnder()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("8", "Spades"));
            // Assert
            Assert.Equal(ScoreState.Under, sut.CheckDealerScore());
        }

        [Fact]
        public void On_DealerScore_Over_21_CheckDealerScore_Returns_ScoreStateBust()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Jack", "Spades"));
            // Assert
            Assert.Equal(ScoreState.Bust, sut.CheckDealerScore());
        }

        [Fact]
        public void When_Dealer_DealerHitAgain_MakeDealerFinish_Method_Makes_The_Dealer_Bust_Or_Under()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            for (int i = 0; i < 5; i++)
            {
                sut._deck.Shuffle();
            }
            var task = sut.MakeDealerFinish();
            // Assert
            Assert.Equal(Task.CompletedTask, task);
            Assert.True(sut.CheckDealerScore() == ScoreState.Bust || sut.CheckDealerScore() == ScoreState.Under);
        }

        [Fact]
        public void When_Dealer_Win_MakeDealerFinish_Method_Doesnt_Make_Dealer_Draw_Again()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Ace", "Spades"));
            var task = sut.MakeDealerFinish();
            // Assert
            Assert.Equal(Task.CompletedTask, task);
            Assert.Equal(3, sut.DealerHand.Count);
        }

        [Fact]
        public void When_Dealer_Bust_MakeDealerFinish_Method_Doesnt_Make_Dealer_Draw_Again()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Jack", "Spades"));
            var task = sut.MakeDealerFinish();
            // Assert
            Assert.Equal(Task.CompletedTask, task);
            Assert.Equal(3, sut.DealerHand.Count);
        }

        [Fact]
        public void When_Dealer_Under_MakeDealerFinish_Method_Doesnt_Make_Dealer_Draw_Again()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("8", "Spades"));
            var task = sut.MakeDealerFinish();
            // Assert
            Assert.Equal(Task.CompletedTask, task);
            Assert.Equal(2, sut.DealerHand.Count);
        }

        [Fact]
        public void When_Dealer_Bust_and_Player_Under_Raises_GameOver_Event_With_Player_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Jack", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("9", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Player", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Bust_and_Player_Win_Raises_GameOver_Event_With_Player_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Jack", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Ace", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Player", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Bust_and_Player_Bust_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Jack", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Jack", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Under_and_Player_Bust_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("8", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Jack", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Under_and_Player_Win_Raises_GameOver_Event_With_Player_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("8", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Ace", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Player", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Under_and_Player_Under_With_Player_Higher_Score_Raises_GameOver_Event_With_Player_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("7", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("9", "Spades"));

            sut.playerStayed = true;
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Player", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Under_and_Player_Under_With_Player_Lower_Score_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("5", "Spades"));

            sut.playerStayed = true;
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Win_and_Player_Bust_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Ace", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Jack", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Win_and_Player_Win_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Ace", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Ace", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Win_and_Player_Under_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Ace", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("5", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Bust_PlayerStay_Raises_GameOver_Event_With_Player_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Jack", "Spades"));

            sut.PlayerStay();
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.CheckGame());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Player", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Under_With_Lower_Score_PlayerStay_Raises_GameOver_Event_With_Player_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("7", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("9", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.PlayerStay());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Player", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Dealer_Under_With_Higher_Score_PlayerStay_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("9", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("7", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.PlayerStay());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Player_Hits_And_Busts_And_Dealer_Under_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("7", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Ace", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.PlayerHit());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(2, sut.DealerHand.Count);
            Assert.Equal(4, sut.PlayerHand.Count);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Player_Hits_And_Busts_And_Dealer_Win_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Ace", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Ace", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.PlayerHit());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(3, sut.DealerHand.Count);
            Assert.Equal(4, sut.PlayerHand.Count);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Player_Hits_And_Busts_And_Dealer_Bust_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("Queen", "Spades"));
            sut.DealerHand.Add(new Card("Jack", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Ace", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.PlayerHit());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(3, sut.DealerHand.Count);
            Assert.Equal(4, sut.PlayerHand.Count);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Player_Hits_And_Busts_And_DealerHitAgain_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("2", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));
            sut.PlayerHand.Add(new Card("Queen", "Spades"));
            sut.PlayerHand.Add(new Card("Ace", "Spades"));
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.PlayerHit());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(2, sut.DealerHand.Count);
            Assert.Equal(4, sut.PlayerHand.Count);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_Player_Hits_And_Is_Under_And_Dealer_Has_Higher_Score_PlayerStay_Raises_GameOver_Event_With_Dealer_Win()
        {
            // Arrange
            var sut = new Blackjack();
            // Act
            sut.DealerHand.Add(new Card("King", "Spades"));
            sut.DealerHand.Add(new Card("9", "Spades"));

            sut.PlayerHand.Add(new Card("King", "Spades"));

            sut.PlayerHit();
            // Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.PlayerStay());
            Assert.NotNull(gameOverEvent);
            Assert.Equal(2, sut.DealerHand.Count);
            Assert.Equal(2, sut.PlayerHand.Count);
            Assert.Equal("Ace", sut.PlayerHand[1].Rank);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Dealer", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_GameOver_Method_Is_Called_GameOver_Event_Is_Raised()
        {
            // Arrange
            var sut = new Blackjack();
            // Act and Assert
            var gameOverEvent = Assert.Raises<GameEventArgs>(h => sut.GameOverEvent += h,
                                                             h => sut.GameOverEvent -= h,
                                                             () => sut.GameOver("Player"));
            Assert.NotNull(gameOverEvent);
            Assert.Equal(sut, gameOverEvent.Sender);
            Assert.Equal("Player", gameOverEvent.Arguments.Text);
        }

        [Fact]
        public void When_UpdateUI_Method_Is_Called_UpdateUI_Event_Is_Raised()
        {
            // Arrange
            var sut = new Blackjack();
            // Act and Assert
            var updateUIEvent = Assert.Raises<GameEventArgs>(h => sut.UpdateUIEvent += h,
                                                             h => sut.UpdateUIEvent -= h,
                                                             () => sut.UpdateUI());
            Assert.NotNull(updateUIEvent);
            Assert.Equal(sut, updateUIEvent.Sender);
            Assert.Equal("Update UI", updateUIEvent.Arguments.Text);
        }
    }
}
