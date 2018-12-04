namespace Game.Tests.Engine.Services
{
    using FluentAssertions;
    using Game.Common;
    using Game.Common.Contracts;
    using Game.Engine;
    using Game.Engine.Services;
    using Game.Engine.Services.Contracts;
    using Game.Items;
    using Game.Items.Contracts;
    using Game.Players.Contracts;
    using Game.Renderer.Contracts;
    using Game.Rooms.Contracts;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class ItemServiceTests
    {
        [Fact]
        public void InitializeRoomItemsShouldRetrunArrayOfThreeItems()
        {
            //Arrange
            var mockedRandom = new Mock<IRandomGenerator>();
            mockedRandom.Setup(mr => mr.GetNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(3);
            var mockedFactory = new Mock<IGameFactory>();
            mockedFactory
                .Setup(mf => mf.CreateItem(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new Item(RoomItems.Bomb.ToString(), GlobalConstants.BombWeight));
            var itemService = new ItemService(mockedRandom.Object, mockedFactory.Object, null);

            //Act
            var items = itemService.InitializeRoomItems();

            //Assert
            items.Should().HaveCount(3);
        }

        [Fact]
        public void CheckIsItemAvailableShouldReturnTrueWithCollectionWhereItemExistsAndIsNotUsed()
        {
            //Arrange
            var itemService = this.GetItemServiceWithRenderer();
            var mockedItem = this.GetMockedItem(RoomItems.Key.ToString(), false, GlobalConstants.KeyWeight);
            var mockedItemsCollection = new List<IItem> { mockedItem };

            //Act
            var result = itemService.CheckIsItemAvailable(RoomItems.Key.ToString(), mockedItemsCollection);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void CheckIsItemAvailableShouldReturnFalseWithCollectionWhereItemExistsAndIsUsed()
        {
            //Arrange
            var itemService = this.GetItemServiceWithRenderer();
            var mockedItem = this.GetMockedItem(RoomItems.Key.ToString(), true, GlobalConstants.KeyWeight);
            var mockedItemsCollection = new List<IItem> { mockedItem };

            //Act
            var result = itemService.CheckIsItemAvailable(RoomItems.Key.ToString(), mockedItemsCollection);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void CheckIsItemAvailableShouldRetrunFalseIfItemIsNotInGivenCollection()
        {
            //Arrange
            var itemService = this.GetItemServiceWithRenderer();
            var mockedItem = this.GetMockedItem("WhateverMissingName", false, 0);
            var mockedItemsCollection = new List<IItem> { mockedItem };

            //Act
            var result = itemService.CheckIsItemAvailable(RoomItems.Key.ToString(), mockedItemsCollection);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void PickItemShouldAddPlayersBagItemIfItExistsInTheRoom()
        {
            //Arrange
            var roomItem = this.GetMockedItem(RoomItems.Bomb.ToString(), false, GlobalConstants.BombWeight);
            var mockedPlayer = this.GetMockedPlayer(GlobalConstants.BackpackMaxWeight, new List<IItem>(), new List<IItem> { roomItem });
            var itemService = this.GetItemServiceWithRenderer();
            
            //Act
            itemService.PickItem(RoomItems.Bomb.ToString(), mockedPlayer);

            //Assert
            mockedPlayer.Backpack.Items.Select(i => i.Name).Contains(RoomItems.Bomb.ToString());
            mockedPlayer.Backpack.Items.Should().BeEmpty();
        }

        [Fact]
        public void PickItemShouldThrowExceptionIfThereIsNotEnoughAvailableSpaceInBag()
        {
            //Arrange
            var roomItem = this.GetMockedItem(RoomItems.Bomb.ToString(), false, GlobalConstants.BombWeight);
            var mockedPlayer = this.GetMockedPlayer(2, new List<IItem>(), new List<IItem> { roomItem });
            var itemService = this.GetItemServiceWithRenderer();

            //Assert
            //
            Action act = () => itemService.PickItem(RoomItems.Bomb.ToString(), mockedPlayer);
            Assert.Throws<InvalidOperationException>(act);
        }
        
        [Fact]
        public void DropItemShouldAddRoomItemAndRemoveFromPlayer()
        {
            //Arrange
            var backpackItem = this.GetMockedItem(RoomItems.Sword.ToString(), false, GlobalConstants.SwordWeight);
            var mockedPlayer = this.GetMockedPlayer(GlobalConstants.BackpackMaxWeight, new List<IItem> { backpackItem }, new List<IItem>());
            var itemService = this.GetItemServiceWithRenderer();

            //Act
            itemService.DropItem(RoomItems.Sword.ToString(), mockedPlayer);

            //Assert
            mockedPlayer.Backpack.Items.Should().BeEmpty();
            mockedPlayer.CurrentRoom.Items.Should().HaveCount(1);
        }

        [Fact]
        public void DropItemShouldThrowExceptionWithIncorrrectItemName()
        {
            //Arrange
            var backpackItem = this.GetMockedItem(RoomItems.Sword.ToString(), false, GlobalConstants.SwordWeight);
            var mockedPlayer = this.GetMockedPlayer(GlobalConstants.BackpackMaxWeight, new List<IItem> { backpackItem }, new List<IItem>());
            var itemService = this.GetItemServiceWithRenderer();

            //Assert
            //
            Action act = () => itemService.DropItem("WhateverMissingName", mockedPlayer);
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void DropItemShouldThrowExceptionWithEmptyStringItemName()
        {
            //Arrange
            var backpackItem = this.GetMockedItem(RoomItems.Sword.ToString(), false, GlobalConstants.SwordWeight);
            var mockedPlayer = this.GetMockedPlayer(GlobalConstants.BackpackMaxWeight, new List<IItem> { backpackItem }, new List<IItem>());
            var itemService = this.GetItemServiceWithRenderer();

            //Assert
            //
            Action act = () => itemService.DropItem(String.Empty, mockedPlayer);
            Assert.Throws<InvalidOperationException>(act);
        }

        [Fact]
        public void GetKeyShouldReturnNullIfKeyNotExistInPlayersBag()
        {
            //Arrange
            var backpackItem = this.GetMockedItem(RoomItems.Sword.ToString(), false, GlobalConstants.SwordWeight);
            var mockedPlayer = this.GetMockedPlayer(GlobalConstants.BackpackMaxWeight, new List<IItem> { backpackItem }, new List<IItem>());
            var itemService = this.GetItemServiceWithRenderer();

            //Act
            var result = itemService.GetKey(mockedPlayer.Backpack.Items);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public void GetKeyWithPassedCollectionWithKeyShouldReturnKey()
        {
            //Arrange
            var mockedItem = this.GetMockedItem(RoomItems.Key.ToString(), false, GlobalConstants.KeyWeight);
            var mockedPlayer = this.GetMockedPlayer(GlobalConstants.BackpackMaxWeight, new List<IItem> { mockedItem }, new List<IItem>());
            var itemService = this.GetItemServiceWithRenderer();

            //Act
            var result = itemService.GetKey(mockedPlayer.Backpack.Items);

            //Assert
            result.Should().NotBeNull();
        }

        private IItem GetMockedItem(string itemName, bool isUsed, int itemWeight)
        {
            var mockedItem = new Mock<IItem>();
            mockedItem.SetupGet(mi => mi.Name).Returns(itemName);
            mockedItem.SetupGet(mi => mi.IsUsed).Returns(isUsed);
            mockedItem.SetupGet(mi => mi.Weight).Returns(itemWeight);
            return mockedItem.Object;
        }

        private IPlayer GetMockedPlayer(int availableSpace, ICollection<IItem> backpackItems, ICollection<IItem> roomItems)
        {
            var mockedPlayer = new Mock<IPlayer>();
            var mockedItem = this.GetMockedItem(RoomItems.Bomb.ToString(), false, GlobalConstants.BombWeight);
            var usedMockedItem = this.GetMockedItem(RoomItems.HealthPack.ToString(), true, GlobalConstants.HealthPackWeight);
            mockedPlayer.SetupGet(mp => mp.Backpack.Items).Returns(backpackItems);
            mockedPlayer.SetupGet(mp => mp.CurrentRoom.Items).Returns(roomItems);
            mockedPlayer.Setup(mp => mp.CurrentRoom.AddItem(It.IsAny<IItem>())).Callback<IItem>(i => roomItems.Add(i));
            mockedPlayer.Setup(mp => mp.Backpack.RemoveUsedItems())
                .Callback(() => backpackItems.Where(i => i.IsUsed).ToList().ForEach(i => backpackItems.Remove(i)));
            mockedPlayer.Setup(mp => mp.Backpack.RemoveItem(It.IsAny<IItem>())).Callback<IItem>(i => backpackItems.Remove(i));
            mockedPlayer.Setup(mp => mp.CurrentRoom.RemoveItem(It.IsAny<IItem>())).Callback<IItem>(i => roomItems.Remove(i));
            mockedPlayer.SetupGet(mp => mp.Backpack.AvailableWeight)
                .Callback(() => mockedPlayer.SetupGet(p => p.Backpack.AvailableWeight)
                    .Returns(availableSpace - backpackItems.Sum(i => i.Weight)))
                .Returns(availableSpace - backpackItems.Sum(i => i.Weight));

            var player = mockedPlayer.Object;
            player.Health = GlobalConstants.PlayerMaxHealthPoints;

            return player;
        }

        private IItemService GetItemServiceWithRenderer()
        {
            var mockedRenderer = new Mock<IRenderer>();
            mockedRenderer.Setup(mr => mr.RenderMessageOnNewLine(It.IsAny<string>()));
            mockedRenderer.Setup(mr => mr.RenderWithoutNewLine(It.IsAny<string>()));

            return new ItemService(null, null, mockedRenderer.Object);
        }
    }
}
