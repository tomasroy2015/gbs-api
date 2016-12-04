using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class ChildrenPolicySummary
    {
        [Key]
        public int ID { get; set; }
        public bool HasChildrenPolicy { get; set; }
        public bool HasExtraBedPolicy { get; set; }
        public Nullable<int> MaxNoExtraBed { get; set; }
        public Nullable<int> MaxNoOfChild { get; set; }
        public Nullable<decimal> ChildrenPrice { get; set; }
        public Nullable<decimal> AdultPrice { get; set; }       
        public Nullable<int> HotelID { get; set; } 
    }
}
