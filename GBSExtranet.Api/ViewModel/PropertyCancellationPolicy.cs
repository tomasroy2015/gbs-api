using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyCancellationPolicy
    {
        public int ID { get; set; }
        public int HotellD { get; set; }
        public Nullable<bool> IsPublicDisplay { get; set; }
        public Nullable<bool> IsPrivateDisplay { get; set; }
        public Nullable<bool> IsPromotionDisplay { get; set; }
        public Nullable<bool> IsPeriodExists { get; set; }
        public Nullable<int> ArrivalTypeID { get; set; }
        public Nullable<int> ArrivalRateID { get; set; }
        public Nullable<int> PrepaymentTypeID { get; set; }
        public Nullable<int> CancelRateID { get; set; }
        public Nullable<bool> IsPrepayment { get; set; }
        public Nullable<int> CancelTypeID { get; set; }
        public string PolicyDescription { get; set; }       
        public string PaymentDescription { get; set; }        
        public Nullable<System.DateTime> OpDateTime { get; set; }
        public Nullable<long> OpUserID { get; set; }
    }
}