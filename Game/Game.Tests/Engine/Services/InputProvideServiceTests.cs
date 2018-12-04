namespace Game.Tests.Engine.Services
{
    using Xunit;
    using FluentAssertions;
    using Game.Engine.Services;
    using Moq;
    using Game.Renderer.Contracts;
    using Game.Reader.Contracts;
    using Game.Common.Contracts;
    using Game.Backpacks;
    using Game.Rooms.Contracts;

    public class InputProvideServiceTests
    {
        [Fact]
        public void InitializePlayersShouldInitialializePlayers()
        {
            //Arrange
            var mockedRenderer = new Mock<IRenderer>();
            mockedRenderer.Setup(mr => mr.RenderWithoutNewLine(It.IsIn<string>()));
            var mockedReader = new Mock<IReader>();
            mockedReader.Setup(mr => mr.Read()).Returns("WhateverName");
            mockedReader.Setup(mr => mr.ReadInt()).Returns(2);
            var mockedFactory = new Mock<IGameFactory>();
            mockedFactory.Setup(mf => mf.CreateBackpakc()).Returns(new Backpack());
            var inputService = new InputProviderService(mockedRenderer.Object, mockedReader.Object, mockedFactory.Object);
            var mockedStartRoom = new Mock<IRoom>();
            mockedStartRoom.SetupGet(msr => msr.Name).Returns("MainRoom");

            //Act
            var players = inputService.InitializePlayers(mockedStartRoom.Object);

            //Assert
            players.Should().HaveCount(2);
        }
    }
}
