using System;
using System.Collections.Generic;
using System.Text;

namespace Imi.Project.Wpf.Core.Entities
{
    public class LoginModel
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
