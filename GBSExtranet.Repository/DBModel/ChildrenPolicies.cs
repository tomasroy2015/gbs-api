using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class ChildrenPolicies
    {
        [Key]
        public int ID { get; set; }
        public bool IsAccommodate { get; set; }
        public bool IsExtraBedProvided { get; set; }
        public int NoOfExtraBed { get; set; }
        public bool IsTwoYearsOld { get; set; }
        public decimal TwoYearsChildCharge { get; set; }
        public bool IsExtraBedChild { get; set; }
        public int ExtraBedChildUnit { get; set; }
        public bool IsAdult { get; set; }
        public decimal AdultCharge { get; set; }
        public bool IsExistingBedChild { get; set; }
        public int ExistingBedChildUnit { get; set; }
        public int ExistingBedChildCount { get; set; }
        public decimal ExistingBedChildCharge { get; set; }
        public int HotelID { get; set; }
        public int CurrencyID { get; set; }
        public DateTime OpDateTime { get; set; }
        public Int64 OpUserID { get; set; }
    }
}
