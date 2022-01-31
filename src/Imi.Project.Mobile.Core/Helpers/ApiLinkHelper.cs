using Xamarin.Forms;

namespace Imi.Project.Mobile.Core.Helpers
{
    public static class ApiLinkHelper
    {
        public static string GetApiLink()
        {
            string ipAddress = Device.RuntimePlatform == Device.Android ? "mc.thawollow.me" : "localhost";
            string apiLink = $"http://{ipAddress}:5000/api/";
            return apiLink;
        }
    }
}
