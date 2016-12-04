using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyCancelPolicy
    {
        public int HotelID { get; set; }
        public int CancelTypeID { get; set; }
        public string CancelTypeName { get; set; }
        public string Refundable { get; set; }
        public int RefundableDayCount { get; set; }
        public int PenaltyRateTypeID { get; set; }
        public string PenaltyRateTypeName { get; set; }
    }
}