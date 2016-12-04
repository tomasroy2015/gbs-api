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
    public class BizTbl_Parameter
    {
        [Key]
        public int ID { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public string Description_tr { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_es { get; set; }
        public string Description_fr { get; set; }
        public string Description_ru { get; set; }
        public string Description_it { get; set; }
        public string Description_ar { get; set; }
        public string Description_jp { get; set; }
        public bool IsCommon { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}
