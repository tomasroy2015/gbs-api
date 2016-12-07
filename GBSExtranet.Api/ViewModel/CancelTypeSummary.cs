using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class CancelTypeSummary
    {
        public int CancelTypeID { get; set; }
        public string CancelTypeName { get; set; }
        public string CancelSummaryText { get; set; }
        public string PrepaymentSummaryText { get; set; }
        public bool IsRefundable { get; set; }
    }
}