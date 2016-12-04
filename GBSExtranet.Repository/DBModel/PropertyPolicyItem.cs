using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class PropertyPolicyItem
    {
        [Key]
        public int ID { get; set; }
        public int PropertyPolicyID { get; set; }
        public string Name { get; set; }
        public string Name_en { get; set; }
        public string Name_tr { get; set; }
        public string Name_de { get; set; }
        public string Name_es { get; set; }
        public string Name_fr { get; set; }
        public string Name_ru { get; set; }
        public string Name_it { get; set; }
        public string Name_ar { get; set; }
        public string Name_ja { get; set; }
        public string Name_pt { get; set; }
        public string Name_zh { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsParentItem { get; set; }
        public Nullable<bool> IsChargedItem { get; set; }
        public string PriceLabel { get; set; }
        public Nullable<int> ParentPolicyItemID { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string RelatedIcon { get; set; }
    }
}
