using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class BizTbl_SecurityGroupRight
    {
        [Key]
        public int ID { get; set; }
        public int SecurityGroupID { get; set; }
        public int SecurityID { get; set; }
        public bool HasRight { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}
