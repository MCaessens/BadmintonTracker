using FreshMvvm;
using Imi.Project.Mobile.Core.Interfaces;
using Imi.Project.Mobile.Core.Models;
using Imi.Project.Mobile.Infrastructure.Interfaces;
using Imi.Project.Mobile.ViewModels;
using Moq;
using Xunit;

namespace Imi.Project.Mobile.Tests
{
    public class LocationTests
    {
        private static readonly LocationModel ValidLocation = new LocationModel
        {
            City = "TestCity",
            Latitude = 1,
            Longitude = 1,
            Name = "TestName",
            PostalCode = "TestPostal",
            Street = "TestStreet"
        };

        #region Detail Tests

        [Fact]
        public void InitDetailPage_WithData_ReturnsSelectedModelNotNull()
        {
            // Arrange
            var locationsService = new Mock<ILocationsService>();
            var detailPage = new LocationDetailPageModel(locationsService.Object, null);

            // Act
            detailPage.Init(new LocationModel());

            //Assert
            Assert.NotNull(detailPage.SelectedModel);
        }

        [Fact]
        public void DetailPageOnSaveCommand_WithValidInput_ExecutesCallToService()
        {
            // Arrange

            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new LocationDetailPageModel(locationsService.Object, vibrationsService.Object)
            {
                SelectedModel = ValidLocation,
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnSave.Execute(null);

            //Assert
            locationsService.Verify(locationService => locationService.UpdateLocationAsync(It.IsAny<LocationModel>()));
        }

        [Fact]
        public void DetailPageOnSaveCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new LocationDetailPageModel(locationsService.Object, vibrationsService.Object)
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
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new LocationDetailPageModel(locationsService.Object, vibrationsService.Object)
            {
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
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new LocationDetailPageModel(locationsService.Object, vibrationsService.Object)
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
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new LocationDetailPageModel(locationsService.Object, vibrationsService.Object)
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

            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var addPage = new AddLocationPageModel(locationsService.Object, vibrationsService.Object)
            {
                NewLocation = ValidLocation,
                CoreMethods = coreMethods.Object
            };

            // Act
            addPage.OnSave.Execute(null);

            //Assert
            locationsService.Verify(locationService => locationService.AddLocationAsync(It.IsAny<LocationModel>()));
        }

        [Fact]
        public void AddPageSaveCommand_Called_ExecutesVibrateCall()
        {
            // Arrange

            // Services
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();

            // Tested model
            var addPage = new AddLocationPageModel(locationsService.Object, vibrationsService.Object)
            {
                NewLocation = ValidLocation,
                CoreMethods = coreMethods.Object
            };

            // Act
            addPage.OnSave.Execute(null);

            //Assert
            vibrationsService.Verify(v => v.Vibrate());
        }

        [Fact]
        public void AddPageOnUploadImageCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var addPage = new AddLocationPageModel(locationsService.Object, vibrationsService.Object)
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
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var addPage = new AddLocationPageModel(locationsService.Object, vibrationsService.Object)
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
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new LocationsPageModel(locationsService.Object, vibrationsService.Object)
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
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new LocationsPageModel(locationsService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnViewDetails.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        [Fact]
        public void OverviewPageOnAddLocationCommand_Called_ExecutesVibrateCall()
        {
            // Arrange
            var locationsService = new Mock<ILocationsService>();
            var vibrationsService = new Mock<IVibrationService>();
            var coreMethods = new Mock<IPageModelCoreMethods>();
            var detailPage = new LocationsPageModel(locationsService.Object, vibrationsService.Object)
            {
                CoreMethods = coreMethods.Object
            };

            // Act
            detailPage.OnAddLocation.Execute(null);

            //Assert
            vibrationsService.Verify(vibrationService => vibrationService.Vibrate());
        }

        #endregion
    }
}