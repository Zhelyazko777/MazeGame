namespace Game.Tests.Engine.Services
{
    using Xunit;
    using FluentAssertions;
    using Game.Engine.Services.Contracts;
    using Game.Engine.Services;
    using Moq;
    using Game.Players.Contracts;
    using System.Collections.Generic;
    using Game.Items.Contracts;
    using Game.Engine;
    using Game.Tests.Helpers;
    using System;
    using Game.Common;

    public class HealthServiceTests
    {
        private readonly IHealthService healthService;

        public HealthServiceTests()
        {
            this.healthService = new HealthService(null);
        }

        [Fact]
        public void DecreasePlayersHealthShouldDecreaseHealthPointsWithTwentyWithPassedPlayerWithoutWeaponAndWithRoomWithMonster()
        {
            //Arrange
            var player = this.GetMockedPlayer(new List<MockableItem>(), 100);

            //Act
            this.healthService.DecreasePlayersHealth(player, true);

            //Assert
            player.Health.Should().Be(80);
        }

        [Fact]
        public void DecreasePlayersHealthShouldDecreaseHealthPointsWithTenWithPassedPlayerWithBombInRoomWithMonster()
        {
            //Arrange
            var player = this.GetMockedPlayer(new List<MockableItem> { new MockableItem(RoomItems.Bomb.ToString(), false) }, 100);

            //Act
            this.healthService.DecreasePlayersHealth(player, true);

            //Assert
            player.Health.Should().Be(90);
        }

        [Fact]
        public void DecreasePlayersHealthShouldDecreaseHealthPointsWithTenWithPassedPlayerWithSwordInRoomWithMonster()
        {
            //Arrange
            var player = this.GetMockedPlayer(new List<MockableItem> { new MockableItem(RoomItems.Sword.ToString(), false) }, 100);
            
            //Act
            this.healthService.DecreasePlayersHealth(player, true);

            //Assert
            player.Health.Should().Be(90);
        }

        [Fact]
        public void DecreasePlayersHealthShouldDecreaseHealthPointsWithTwentyWithPassedPlayerWithoutWeaponButOtherItemsInRoomWithMonster()
        {
            //Arrange
            var player = this.GetMockedPlayer(new List<MockableItem> { new MockableItem(RoomItems.Key.ToString(), false) }, 100);

            //Act
            this.healthService.DecreasePlayersHealth(player, true);

            //Assert
            player.Health.Should().Be(80);
        }

        [Fact]
        public void DecreasePlayersHealthShouldDecreaseHealthPointsWith20WithPassedWeaponsWhichAreUsedInRoomWithMonster()
        {
            //Arrange
            var player = this.GetMockedPlayer(new List<MockableItem> { new MockableItem(RoomItems.Sword.ToString(), true),
                                                                       new MockableItem(RoomItems.Bomb.ToString(), true) }, 100);

            //Act
            this.healthService.DecreasePlayersHealth(player, true);

            //Assert
            player.Health.Should().Be(80);
        }

        [Fact]
        public void IncreasePlayersHealthIfHealthpackShouldNotIncreaseHealthPointsWithoutHealthPack()
        {
            //Arrange
            var player = this.GetMockedPlayer(new List<MockableItem>(), 90);

            //Act
            this.healthService.IncreasePlayersHealthIfHealthpack(player);

            //Assert
            player.Health.Should().Be(90);
        }

        [Fact]
        public void IncreasePlayersHealthIfHealthpackShouldIncreaseHealthPointsWithHealthPackInPlayersBag()
        {
            //Arrange
            var player = this.GetMockedPlayer(new List<MockableItem> { new MockableItem(RoomItems.HealthPack.ToString(), false) }, 90);

            //Act
            this.healthService.IncreasePlayersHealthIfHealthpack(player);

            //Assert
            player.Health.Should().Be(100);
        }

        [Fact]
        public void IncreasePlayersHealthIfHealthpackShouldNotInreaseHealthPointsWithUsedHealthpack()
        {
            //Arrange
            var player = this.GetMockedPlayer(new List<MockableItem> { new MockableItem(RoomItems.HealthPack.ToString(), true) }, 90);

            //Act
            this.healthService.IncreasePlayersHealthIfHealthpack(player);

            //Assert
            player.Health.Should().Be(90);
        }

        [Fact]
        public void IncreasePlayersHealthIfHealthpackShouldNotIncreaseWithOtherItemsWithoutHealthPack()
        {
            //Arrange
            var player = this.GetMockedPlayer(new List<MockableItem> { new MockableItem(RoomItems.Key.ToString(), false) }, 90);

            //Act
            this.healthService.IncreasePlayersHealthIfHealthpack(player);

            //Assert
            player.Health.Should().Be(90);
        }

        private IPlayer GetMockedPlayer(IEnumerable<MockableItem> items, int health)
        {
            var mockedPlayer = new Mock<IPlayer>();
            var itemsList = new List<IItem>();

            foreach (var item in items)
            {
                var mockedItem = new Mock<IItem>();
                mockedItem.SetupGet(mi => mi.IsUsed).Returns(item.IsUsed);
                mockedItem.SetupGet(mi => mi.Name).Returns(item.Name);
                itemsList.Add(mockedItem.Object);
            }
            mockedPlayer.SetupProperty(mp => mp.Health, health);
            mockedPlayer.SetupGet(mp => mp.Backpack.Items).Returns(itemsList);

            return mockedPlayer.Object;
        }
    }
}
