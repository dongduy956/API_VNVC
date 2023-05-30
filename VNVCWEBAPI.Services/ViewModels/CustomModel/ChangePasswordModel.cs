using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNVCWEBAPI.Common.Library;

namespace VNVCWEBAPI.Services.ViewModels.CustomModel
{
    public class ChangePasswordModel
    {
        private string _password;
        private string _newPassword;
        public string Password
        {
            get
            {
                return _password.PasswordHash();
            }
            set
            {
                _password = value;
            }
        }

        public string NewPassword
        {
            get
            {
                return _newPassword.PasswordHash();
            }
            set
            {
                _newPassword = value;
            }
        }
    }
}
