using System;
using FreshMvvm;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Imi.Project.Mobile.ViewModels;
using Moq;
using Xunit;

namespace Imi.Project.Mobile.Tests
{
    public class GameTests
    {
        #region Detail Tests

        [Fact]
        public void DetailPageSaveCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var validGame = new GameModel
            {
                Id = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Opponent = "Louis",
                OpponentScore = 1,
                RacketId = Guid.NewGuid(),
                Score = 1,
                ShuttleCockId = Guid.NewGuid()
            };

            // Services
            var gamesService = new Mock<IGamesService>();
            var racketsService = new Mock<IRacketsService>();
            var shuttleService = new Mock<IShuttleCocksService>();
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new GameDetailPageModel(locationsService.Object, gamesService.Object, shuttleService.Object, racketsService.Object, vibrationsService.Object)
            {
                SelectedModel = validGame,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnSave.Execute(null);

            //Assert
            vibrationsService.Verify(v => v.Vibrate());
        }

        [Fact]
        public void DetailPageSaveCommand_WithValidInput_ExecutesCallToService()
        {
            // Arrange
            var validGame = new GameModel
            {
                Id = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Opponent = "Louis",
                OpponentScore = 1,
                RacketId = Guid.NewGuid(),
                Score = 1,
                ShuttleCockId = Guid.NewGuid()
            };

            // Services
            var gamesService = new Mock<IGamesService>();
            var racketsService = new Mock<IRacketsService>();
            var shuttleService = new Mock<IShuttleCocksService>();
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new GameDetailPageModel(locationsService.Object, gamesService.Object, shuttleService.Object, racketsService.Object, vibrationsService.Object)
            {
                SelectedModel = validGame,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnSave.Execute(null);

            //Assert
            gamesService.Verify(gameService => gameService.UpdateGameAsync(It.IsAny<GameModel>()));
        }

        [Fact]
        public void DetailPageDeleteCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var validGame = new GameModel
            {
                Id = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Opponent = "Louis",
                OpponentScore = 1,
                RacketId = Guid.NewGuid(),
                Score = 1,
                ShuttleCockId = Guid.NewGuid()
            };

            // Services
            var gamesService = new Mock<IGamesService>();
            var racketsService = new Mock<IRacketsService>();
            var shuttleService = new Mock<IShuttleCocksService>();
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new GameDetailPageModel(locationsService.Object, gamesService.Object, shuttleService.Object, racketsService.Object, vibrationsService.Object)
            {
                SelectedModel = validGame,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnDelete.Execute(null);

            //Assert
            vibrationsService.Verify(v => v.Vibrate());
        }

        [Fact]
        public void InitDetailPage_WithData_ReturnsSelectedModelNotNull()
        {
            // Arrange
            var gamesService = new Mock<IGamesService>();
            var racketsService = new Mock<IRacketsService>();
            var shuttleService = new Mock<IShuttleCocksService>();
            var locationsService = new Mock<ILocationsService>();
            var detailPage = new GameDetailPageModel(locationsService.Object, gamesService.Object, shuttleService.Object, racketsService.Object, null);

            // Act
            detailPage.Init(new GameModel());

            //Assert
            Assert.NotNull(detailPage.SelectedModel);
        }

        #endregion

        #region Add Tests

        [Fact]
        public void AddPageSaveCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var validGame = new GameModel
            {
                Id = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Opponent = "Louis",
                OpponentScore = 1,
                RacketId = Guid.NewGuid(),
                Score = 1,
                ShuttleCockId = Guid.NewGuid()
            };

            // Services
            var gamesService = new Mock<IGamesService>();
            var racketsService = new Mock<IRacketsService>();
            var shuttleService = new Mock<IShuttleCocksService>();
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new AddGamePageModel(gamesService.Object, racketsService.Object, shuttleService.Object, locationsService.Object, vibrationsService.Object)
            {
                NewGameModel = validGame,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnSave.Execute(null);

            //Assert
            vibrationsService.Verify(v => v.Vibrate());
        }

        [Fact]
        public void AddPageSaveCommand_WithValidInput_ExecutesCallToService()
        {
            // Arrange
            var validGame = new GameModel
            {
                Id = Guid.NewGuid(),
                LocationId = Guid.NewGuid(),
                Opponent = "Louis",
                OpponentScore = 1,
                RacketId = Guid.NewGuid(),
                Score = 1,
                ShuttleCockId = Guid.NewGuid()
            };

            // Services
            var gamesService = new Mock<IGamesService>();
            var racketsService = new Mock<IRacketsService>();
            var shuttleService = new Mock<IShuttleCocksService>();
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new AddGamePageModel(gamesService.Object, racketsService.Object, shuttleService.Object, locationsService.Object, vibrationsService.Object)
            {
                NewGameModel = validGame,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnSave.Execute(null);

            //Assert
            gamesService.Verify(gameService => gameService.AddGameAsync(It.IsAny<GameModel>()));
        }

        #endregion

        #region Overview Tests

        [Fact]
        public void OverviewPageOnAddCommand_Called_ExecutesVibrateCall()
        {
            // Arrange

            // Services
            var gamesService = new Mock<IGamesService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new GamesPageModel(gamesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnAddGame.Execute(null);

            //Assert
            vibrationsService.Verify(v => v.Vibrate());
        }

        [Fact]
        public void OverviewPageOnRefreshCommand_Called_ExecutesVibrateCall()
        {
            // Arrange

            // Services
            var gamesService = new Mock<IGamesService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new GamesPageModel(gamesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnRefresh.Execute(null);

            //Assert
            vibrationsService.Verify(v => v.Vibrate());
        }

        [Fact]
        public void OverviewPageOnViewDetailsCommand_Called_ExecutesVibrateCall()
        {
            // Arrange

            // Services
            var gamesService = new Mock<IGamesService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new GamesPageModel(gamesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnViewDetails.Execute(null);

            //Assert
            vibrationsService.Verify(v => v.Vibrate());
        }

        #endregion
    }
}