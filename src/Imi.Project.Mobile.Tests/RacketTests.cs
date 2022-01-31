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
    public class RacketTests
    {
        private static readonly RacketModel ValidRacket = new RacketModel
        {
            Id = Guid.NewGuid(),
            Brand = "TestBrand",
            Model = "TestModel",
            RacketType = RacketType.Attacking
        };

        #region Detail Tests

        [Fact]
        public void InitDetailPage_WithData_ReturnsSelectedModelNotNull()
        {
            // Arrange
            var racketsService = new Mock<IRacketsService>();
            var detailPage = new RacketDetailPageModel(racketsService.Object, null);

            // Act
            detailPage.Init(new RacketModel());

            //Assert
            Assert.NotNull(detailPage.SelectedModel);
        }

        [Fact]
        public void DetailPageOnSaveCommand_WithValidInput_ExecutesCallToService()
        {
            // Arrange

            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new RacketDetailPageModel(racketsService.Object, vibrationsService.Object)
            {
                SelectedModel = ValidRacket,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnSave.Execute(null);

            //Assert
            racketsService.Verify(racketService => racketService.UpdateRacketAsync(It.IsAny<RacketModel>()));
        }

        [Fact]
        public void DetailPageOnSaveCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new RacketDetailPageModel(racketsService.Object, vibrationsService.Object)
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
            var shuttlesService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new RacketDetailPageModel(shuttlesService.Object, vibrationsService.Object)
            {
                SelectedModel = ValidRacket,
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
            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new RacketDetailPageModel(racketsService.Object, vibrationsService.Object)
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
            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new RacketDetailPageModel(racketsService.Object, vibrationsService.Object)
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

            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var addPage = new AddRacketPageModel(racketsService.Object, vibrationsService.Object)
            {
                NewRacket = ValidRacket,
                CoreMethods = coreMethods.Object
            };

            // Act
            addPage.OnSave.Execute(null);

            //Assert
            racketsService.Verify(racketService => racketService.AddRacketAsync(It.IsAny<RacketModel>()));
        }

        [Fact]
        public void AddPageSaveCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var validRacket = new RacketModel()
            {
                Id = Guid.NewGuid(),
                Brand = "TestBrand",
                Model = "TestModel",
                RacketType = RacketType.Attacking
            };

            // Services
            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var detailPage = new AddRacketPageModel(racketsService.Object, vibrationsService.Object)
            {
                NewRacket = validRacket,
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
            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var addPage = new AddRacketPageModel(racketsService.Object, vibrationsService.Object)
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
            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var addPage = new AddRacketPageModel(racketsService.Object, vibrationsService.Object)
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
            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new RacketsPageModel(racketsService.Object, vibrationsService.Object)
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
            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new RacketsPageModel(racketsService.Object, vibrationsService.Object)
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
            var racketsService = new Mock<IRacketsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new RacketsPageModel(racketsService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnAddRacket.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        #endregion
    }
}