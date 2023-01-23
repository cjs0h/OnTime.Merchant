using System.ComponentModel;

namespace Merchant.Common.Const;

public static class Enums
{
    public enum UserRole
    {
        [Description("Admin")]
        Admin = 0,
        [Description("User")]
        User = 1,
        [Description("HelpDesk")]
        HelpDesk = 2,
        [Description("NotActive")]
        NotActive = 3,
    }
    public enum Gender
    {
        [Description("Male")]
        Male=1,
        [Description("Female")]
        Female=2,
        [Description("Other")]
        Other=3
    }
    public enum UniqueIdentifier
    {
        UserName,
        PhoneNumber,
    }
}