using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Haptics;
using Imi.Project.Mobile.Core.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Imi.Project.Mobile.UWP.Services.VibrationServiceUwp))]

namespace Imi.Project.Mobile.UWP.Services
{
    public class VibrationServiceUwp : IVibrationService
    {
        public async Task Vibrate()
        {
            var access = await VibrationDevice.RequestAccessAsync();
            if (access != VibrationAccessStatus.Allowed) return;
            var vibrationDevice = await VibrationDevice.GetDefaultAsync();
            var supportedFeedbacks = vibrationDevice?.SimpleHapticsController.SupportedFeedback;
            if (supportedFeedbacks == null) return;
            vibrationDevice?.SimpleHapticsController.SendHapticFeedbackForDuration(supportedFeedbacks.First(), 1, TimeSpan.FromMilliseconds(50));
        }
    }
}