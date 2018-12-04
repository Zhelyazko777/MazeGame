namespace Game.Tests.Engine.Services
{
    using Xunit;
    using FluentAssertions;
    using Game.Engine.Services;
    using Game.Players;
    using Moq;
    using Game.Players.Contracts;
    using Game.Engine.Services.Contracts;
    using System.Collections.Generic;
    using Game.Items.Contracts;
    using Game.Engine;
    using System;
    using Game.Common;

    public class GameEndServiceTests
    {
        private readonly IGameEndService gameEndService;

        public GameEndServiceTests()
        {
            this.gameEndService = new GameEndService(null);
        }

        [Fact]
        public void CheckIsPlayerAliveShouldReturnTrueOfPlayerHasHealthPointsMoreThanZero()
        {
            //Arrange
            var mockedPlayer = new Mock<IPlayer>();
            mockedPlayer.SetupGet(mp => mp.Health).Returns(60);

            //Act
            var result = this.gameEndService.CheckIsPlayerAlive(mockedPlayer.Object);


            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckIsPlayerAlaiveshouldReturnFalseWhithPlayerWithZeroHealthPoints()
        {
            //Arrange
            var mockedPlayer = new Mock<IPlayer>();
            mockedPlayer.SetupGet(mp => mp.Health).Returns(0);

            //Act
            var result = this.gameEndService.CheckIsPlayerAlive(mockedPlayer.Object);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CheckIsPlayerAliveWithLessThanZeroHealthPointsShouldReturnFalse()
        {
            //Arrange
            var mockedPlayer = new Mock<IPlayer>();
            mockedPlayer.SetupGet(mp => mp.Health).Returns(-10);

            //Act
            var result = this.gameEndService.CheckIsPlayerAlive(mockedPlayer.Object);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CheckIsPlayerAliveWithNullPlayershouldThrowException()
        {
            //Assert
            Action act = () => this.gameEndService.CheckIsPlayerAlive(null);
            Assert.Throws<NullReferenceException>(act);
        }

        [Fact]
        public void CheckIsPlayerWinWithAutowinItemInBagShouldReturnTrue()
        {
            //Arrange
            var mockedPlayer = new Mock<IPlayer>();
            var mockedItem = new Mock<IItem>();
            mockedItem.SetupGet(i => i.Name).Returns(RoomItems.AutoWinItem.ToString());
            mockedPlayer.SetupGet(mp => mp.Backpack.Items).Returns(new List<IItem> { mockedItem.Object });

            //Act
            var result = this.gameEndService.CheckIsPlayerWin(mockedPlayer.Object);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckIsPlayerWinWithoutAutowinItemInBagShouldReturnFalse()
        {
            //Arrange
            var mockedPlayer = new Mock<IPlayer>();
            mockedPlayer.SetupGet(mp => mp.Backpack.Items).Returns(new List<IItem>());

            //Act
            var result = this.gameEndService.CheckIsPlayerWin(mockedPlayer.Object);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CheckIsPlayerWinShouldThrowExceptionWithNullPlayerPassed()
        {
            //Assert
            Action act = () => this.gameEndService.CheckIsPlayerWin(null);
            Assert.Throws<NullReferenceException>(act);
        }
    }
}
