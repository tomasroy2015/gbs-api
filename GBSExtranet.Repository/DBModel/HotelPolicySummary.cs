using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class HotelPolicySummary
    {
        [Key]
        public int ID { get; set; }
        public string Description { get; set; }
        public int HotelID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public string Icons { get; set; }
        public long OpUserID { get; set; }
    }
}
