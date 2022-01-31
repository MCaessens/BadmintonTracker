namespace Imi.Project.Mobile.Infrastructure.Helpers
{
    public static class ImageHelper
    {
        public static string CreateImageHeader(string fileExt)
        {
            if (fileExt.Equals("jpg") || fileExt.Equals("png") || fileExt.Equals("jpeg"))
            {
                return $"image/{fileExt}";
            }
            else
            {
                return "";
            }
        }
    }
}
