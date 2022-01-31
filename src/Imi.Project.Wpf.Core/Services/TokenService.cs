using System;
using System.IO;
using System.Text;
using Imi.Project.Wpf.Core.Entities;

namespace Imi.Project.Wpf.Core.Services
{
    public static class TokenService
    {
        public static string GetToken()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.TokenStorageName);
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
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.TokenStorageName);
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
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.TokenStorageName);
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