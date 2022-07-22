using Games.Classes.GameClasses;
using Games.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Pages;

namespace GamesTests
{
    public class BlackjackPageTests
    {
        [Fact]
        public void When_GameOver_Event_Is_Raised_Call_GameOverEventHandler_And_Thus_Change_Appropriate_Properties()
        {
            // Arrange
            var sut = new BlackjackPage();
            // Act
            sut._blackjackGame.GameOver("Player");
            // Assert
            Assert.True(sut.gameOver);
            Assert.Equal("Player", sut.winner);
        }
        [Fact]
        public void When_StartGame_Method_Is_Called_GameStart_Bool_Is_True_And_Calls_The_Games_StartGame_Method()
        {
            // Arrange
            var sut = new BlackjackPage();
            // Act
            sut.StartGame();
            // Assert
            Assert.True(sut.gameStart);
            Assert.Equal(2, sut._blackjackGame.PlayerHand.Count);
            Assert.Equal(2, sut._blackjackGame.DealerHand.Count);
        }
    }
}
