using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class HotelCancellationPolicy
    {
        [Key]
        public int ID { get; set; }
        public int HotellD { get; set; }
        public string PolicyDescription_tr { get; set; }
        public string PolicyDescription_en { get; set; }
        public string PolicyDescription_de { get; set; }
        public string PolicyDescription_es { get; set; }
        public string PolicyDescription_fr { get; set; }
        public string PolicyDescription_ru { get; set; }
        public string PolicyDescription_it { get; set; }
        public string PolicyDescription_ar { get; set; }
        public string PolicyDescription_ja { get; set; }
        public string PolicyDescription_pt { get; set; }
        public string PolicyDescription_zh { get; set; }
        public string PaymentDescription_en { get; set; }
        public string PaymentDescription_tr { get; set; }
        public string PaymentDescription_ar { get; set; }
        public string PaymentDescription_ja { get; set; }
        public string PaymentDescription_zh { get; set; }
        public string PaymentDescription_pt { get; set; }
        public string PaymentDescription_it { get; set; }
        public string PaymentDescription_ru { get; set; }
        public string PaymentDescription_fr { get; set; }
        public string PaymentDescription_de { get; set; }
        public string PaymentDescription_es { get; set; }
        public Nullable<bool> IsPublicDisplay { get; set; }
        public Nullable<bool> IsPrivateDisplay { get; set; }
        public Nullable<bool> IsPromotionDisplay { get; set; }
        public Nullable<bool> IsPeriodExists { get; set; }
        public Nullable<int> ArrivalTypeID { get; set; }
        public Nullable<int> ArrivalRateID { get; set; }
        public Nullable<int> CancelTypeID { get; set; }
        public Nullable<int> PrepaymentTypeID { get; set; }
        public Nullable<int> CancelRateID { get; set; }
        public Nullable<bool> IsPrepayment { get; set; }
        public Nullable<System.DateTime> OpDateTime { get; set; }
        public Nullable<long> OpUserID { get; set; }
    }
}
