using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merchant.Common.Const
{
    public static partial class Constants
    {
        public static class Error
        {
            public static readonly Tuple<int, string> UserNotFound = new(1, "User not found.");
            public static readonly Tuple<int, string> UpdatePasswordInvalidOldPassword = new(2, "Invalid old password.");
            public static readonly Tuple<int, string> LoginError = new(3, "Please Check you username and password");
            public static readonly Tuple<int, string> UserNotActive = new(4, "Please activate you account first");
            public static readonly Tuple<int, string> UserAlreadyActivated = new(5, "User Already activated");
        }
    }
}
