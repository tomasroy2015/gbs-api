using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public  class TB_HotelAttribute
    {
        [Key]
        public int ID { get; set; }
        public int HotelID { get; set; }
        public int AttributeID { get; set; }
        public Nullable<bool> Charged { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string UnitValue { get; set; }
        public Nullable<decimal> Charge { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}
