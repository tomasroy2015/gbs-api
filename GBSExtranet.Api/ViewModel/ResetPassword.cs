using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class ResetPassword
    {
        public string Email { get; set; }
        public string CultureCode { get; set; }
        public int UserID { get; set; }
        public string Password { get; set; }
    }
}