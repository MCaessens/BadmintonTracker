using System;
using FreshMvvm;
using Imi.Project.Common.Enums;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Imi.Project.Mobile.ViewModels;
using Moq;
using Xunit;

namespace Imi.Project.Mobile.Tests
{
    public class ShuttleTests
    {
        private static readonly ShuttleCockModel ValidShuttleCock = new ShuttleCockModel
        {
            Id = Guid.NewGuid(),
            Brand = "TestBrand",
            Model = "TestModel",
            ShuttleType = ShuttleType.Feather
        };

        #region Detail Tests

        [Fact]
        public void InitDetailPage_WithData_ReturnsSelectedModelNotNull()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var detailPage = new ShuttleCockDetailPageModel(shuttlesService.Object, null);

            // Act
            detailPage.Init(new ShuttleCockModel());

            //Assert
            Assert.NotNull(detailPage.SelectedModel);
        }

        [Fact]
        public void DetailPageOnSaveCommand_WithValidInput_ExecutesCallToService()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new ShuttleCockDetailPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                SelectedModel = ValidShuttleCock,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnSave.Execute(null);

            //Assert
            shuttlesService.Verify(shuttleService => shuttleService.UpdateShuttleCockAsync(It.IsAny<ShuttleCockModel>()));
        }

        [Fact]
        public void DetailPageOnSaveCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new ShuttleCockDetailPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnSave.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        [Fact]
        public void DetailPageOnDeleteCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new ShuttleCockDetailPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                SelectedModel = ValidShuttleCock,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnDelete.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        [Fact]
        public void DetailPageOnUploadImageCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new ShuttleCockDetailPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnUploadImage.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        [Fact]
        public void DetailPageOnTakePictureCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new ShuttleCockDetailPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnTakePicture.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        #endregion

        #region Add Tests

        [Fact]
        public void AddPageOnSaveCommand_WithValidInput_ExecutesCallToService()
        {
            // Arrange

            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var addPage = new AddShuttleCockPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                NewShuttle = ValidShuttleCock,
                CoreMethods = coreMethods.Object
            };

            // Act
            addPage.OnSave.Execute(null);

            //Assert
            shuttlesService.Verify(shuttleService => shuttleService.AddShuttleCockAsync(It.IsAny<ShuttleCockModel>()));
        }

        [Fact]
        public void AddPageSaveCommand_Called_ExecutesVibrateCall()
        {
            // Arrange

            // Services
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new AddShuttleCockPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                NewShuttle = ValidShuttleCock,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnSave.Execute(null);

            //Assert
            vibrationsService.Verify(v => v.Vibrate());
        }

        [Fact]
        public void AddPageOnUploadImageCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var addPage = new AddShuttleCockPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            addPage.OnUploadImage.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        [Fact]
        public void AddPageOnTakePictureCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var addPage = new AddShuttleCockPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            addPage.OnTakePicture.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        #endregion

        #region Overview Tests

        [Fact]
        public void OverviewPageOnRefreshCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new ShuttleCocksPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnRefresh.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        [Fact]
        public void OverviewPageOnViewDetailsCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new ShuttleCocksPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnViewDetails.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        [Fact]
        public void OverviewPageOnAddRacketCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var shuttlesService = new Mock<IShuttleCocksService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new ShuttleCocksPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnAddShuttleCock.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        #endregion
    }
}