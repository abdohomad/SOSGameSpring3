﻿using SOSGameLogic.Implementation;
using SOSGameLogic.Interfaces;


namespace SOSGameLogicTest
{
    public class PlayerTests
    {
        [Fact]
        public void GetPlayerSymbol_ReturnsInitializedSymbol()
        {
            // Arrange
            char expectedSymbol = 'S';
            IPlayer player = new Player(expectedSymbol);

            // Act
            char actualSymbol = player.GetPlayerSymbol();

            // Assert
            Assert.Equal(expectedSymbol, actualSymbol);
        }

        [Fact]
        public void GetScore_ReturnsZeroInitially()
        {
            // Arrange
            IPlayer player = new Player('S');

            // Act
            int score = player.GetScore();

            // Assert
            Assert.Equal(0, score);
        }

        [Fact]
        public void IncreaseScore_IncreasesScoreCorrectly()
        {
            // Arrange
            IPlayer player = new Player('S');

            // Act
            player.IncreaseScore(10);
            int score = player.GetScore();

            // Assert
            Assert.Equal(10, score);
        }

        [Fact]
        public void SetPlayerSymbol_SetsSymbolCorrectly()
        {
            // Arrange
            IPlayer player = new Player('S');

            // Act
            player.SetPlayerSymbol('O');
            char symbol = player.GetPlayerSymbol();

            // Assert
            Assert.Equal('O', symbol);
        }

        
    }
}
