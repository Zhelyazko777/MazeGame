namespace Game.Tests.Engine.Services
{
    using FluentAssertions;
    using Game.Engine.Services;
    using Game.Engine.Services.Contracts;
    using Game.Exits.Contracts;
    using Game.Items.Contracts;
    using Game.Renderer.Contracts;
    using Game.Rooms.Contracts;
    using Moq;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class LocationServiceTests
    {
        private readonly ILocationService locationService;
        private const string firstRoomName = "FirstRoom";
        private const string secondRoomName = "SecondRoom";

        public LocationServiceTests()
        {
            var mockedRenderer = new Mock<IRenderer>();
            mockedRenderer.Setup(mr => mr.RenderMessageOnNewLine(It.IsAny<string>()));
            mockedRenderer.Setup(mr => mr.RenderWithoutNewLine(It.IsAny<string>()));
            this.locationService = new LocationService(mockedRenderer.Object);
        }

        [Fact]
        public void ChangeLocationWithCorrectDataShouldChangeRoom()
        {
            //Arrange
            var rooms = this.InitializeMockedRoomsandExits(false, false);
            var key = this.InitializeKey(false);

            //Act
            var newRoom = this.locationService.ChangeLocation(secondRoomName, rooms[0], key);

            //Assert
            newRoom.Name.Should().Be(secondRoomName);
        }

        [Fact]
        public void ChangeLocationWithLockedExitAndNullKeyShouldThrowException()
        {
            //Arrange
            var rooms = this.InitializeMockedRoomsandExits(true, true);

            //Assert
            Action act = () => this.locationService.ChangeLocation(secondRoomName, rooms[0], null);
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void ChangeLocationWithLockedExitAndUsedKeyShouldThrowException()
        {
            //Arrange
            var rooms = this.InitializeMockedRoomsandExits(true, true);
            var key = this.InitializeKey(true);

            //Assert
            Action act = () => this.locationService.ChangeLocation(secondRoomName, rooms[0], key);
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void ChnageLocationWithLockedExitAndNotUsedKeyShouldBeSeccessful()
        {
            //Arrange
            var rooms = this.InitializeMockedRoomsandExits(true, true);
            var key = this.InitializeKey(false);

            //Act
            var newRoom = this.locationService.ChangeLocation(secondRoomName, rooms[0], key);

            //Assert
            newRoom.Name.Should().Be(secondRoomName);
        }

        private IList<IRoom> InitializeMockedRoomsandExits(bool isFirstExitLocked, bool isSecondExitLocked)
        {
            var firstMockedRoom = new Mock<IRoom>();
            var secondMockedRoom = new Mock<IRoom>();
            var firstMockedExit = new Mock<IExit>();
            firstMockedExit.SetupGet(me => me.FirstRoom).Returns(firstMockedRoom.Object);
            firstMockedExit.SetupGet(me => me.SecondRoom).Returns(secondMockedRoom.Object);
            firstMockedExit.SetupGet(me => me.IsLocked).Returns(isFirstExitLocked);
            firstMockedRoom.SetupGet(mr => mr.Exits).Returns(new List<IExit> { firstMockedExit.Object });
            firstMockedRoom.SetupGet(mr => mr.Name).Returns(firstRoomName);
            secondMockedRoom.SetupGet(mr => mr.Name).Returns(secondRoomName);
            var secondMockedExit = new Mock<IExit>();
            secondMockedExit.SetupGet(me => me.FirstRoom).Returns(secondMockedRoom.Object);
            secondMockedExit.SetupGet(me => me.SecondRoom).Returns(firstMockedRoom.Object);
            secondMockedExit.SetupGet(me => me.IsLocked).Returns(isSecondExitLocked);

            return new List<IRoom> { firstMockedRoom.Object, secondMockedRoom.Object };
        }

        private IItem InitializeKey(bool isKeyUsed)
        {
            var mockedKey = new Mock<IItem>();
            mockedKey.SetupGet(mk => mk.IsUsed).Returns(isKeyUsed);

            return mockedKey.Object;
        }
    }
}
