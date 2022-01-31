using Imi.Project.Mobile.Core.Constants;
using System;
using System.IO;
using System.Text;

namespace Imi.Project.Mobile.Core.Services
{
    public static class TokenService
    {
        public static string GetToken()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KeyValues.storageName);
            try
            {
                var encodedData = Convert.FromBase64String(File.ReadAllText(path));
                var token = Encoding.UTF8.GetString(encodedData);
                return token;
            }
            catch
            {
                return "";
            }
        }
        public static bool SaveToken(string token)
        {
            try
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KeyValues.storageName);
                var dataBytes = Encoding.UTF8.GetBytes(token);
                var base64string = Convert.ToBase64String(dataBytes);
                File.WriteAllText(path, base64string);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool ResetToken()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KeyValues.storageName);
            try
            {
                File.Delete(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
