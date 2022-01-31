using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Imi.Project.Mobile.Core.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(Imi.Project.Mobile.Droid.Services.VibrationServiceAndroid))]

namespace Imi.Project.Mobile.Droid.Services
{
    public class VibrationServiceAndroid : IVibrationService
    {
        public async Task Vibrate()
        {
            var vibrator = await Task.FromResult(Android.App.Application.Context.GetSystemService(Context.VibratorService) as Vibrator);
            var hasVibrator = vibrator?.HasVibrator;
            if (hasVibrator.HasValue && !hasVibrator.Value) return;
            vibrator?.Vibrate(VibrationEffect.CreateOneShot(50, VibrationEffect.DefaultAmplitude));
        }
    }
}