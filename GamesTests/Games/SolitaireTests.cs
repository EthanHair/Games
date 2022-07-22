using Games.Classes.GameClasses;

namespace GamesTests.GamesTests
{
    public class SolitaireTests
    {
        [Fact]
        public void New_Solitaire_Game_Is_Dealt_Correctly()
        {
            // Arrange
            var sut = new Solitaire();
            // Act
            // Assert
            Assert.Equal(1, sut._stacksDict["stack1"].Count);
            Assert.Equal(2, sut._stacksDict["stack2"].Count);
            Assert.Equal(3, sut._stacksDict["stack3"].Count);
            Assert.Equal(4, sut._stacksDict["stack4"].Count);
            Assert.Equal(5, sut._stacksDict["stack5"].Count);
            Assert.Equal(6, sut._stacksDict["stack6"].Count);
            Assert.Equal(7, sut._stacksDict["stack7"].Count);
            Assert.Equal(0, sut._stacksDict["spadeStack"].Count);
            Assert.Equal(0, sut._stacksDict["heartStack"].Count);
            Assert.Equal(0, sut._stacksDict["clubStack"].Count);
            Assert.Equal(0, sut._stacksDict["diamondStack"].Count);
            Assert.Equal(24, sut._stacksDict["drawStack"].Count);
            Assert.Equal(0, sut._stacksDict["handStack"].Count);
        }
    }
}