using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.Models.Enumerations
{
    public enum Permissions
    {
        /// <summary>
        /// AllUsers All type of users may have access
        /// </summary>
        None = -1,
        AllUsers = 0,
        Administrator = 1,
    }

    public enum LoginTypes
    {
        FormLogin = 0,
        WindowsLogin = 1
    }
}