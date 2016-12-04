using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class HotelPropertyPolicy
    {
        [Key]
        public int ID { get; set; }
        public int PropertyPolicyID { get; set; }
        public int PropertyPolicyItemID { get; set; }
        public int HotelID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string UnitValue { get; set; }
        public Nullable<int> PriceUnitID { get; set; }
        public Nullable<decimal> Price { get; set; }
        public int CurrencyID { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}
