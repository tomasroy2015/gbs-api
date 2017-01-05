using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//BalsTechnology-SK   
namespace GBSExtranet.Api.ViewModel
{
    public class InvoiceDetails
    {
        public string Period { get; set; }

        public string Amount { get; set; }

        public string InvoiceDate { get; set; }

        public string DueDate { get; set; }

        public string InvoiceID { get; set; }
    }
}