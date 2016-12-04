using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository
{
    public class ChildrenPolicyItems  
    {
        [Key]
        public int ID { get; set; }
        public int ChildrenPolicyHeaderID { get; set; }
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
        public string Description_tr { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_es { get; set; }
        public string Description_fr { get; set; }
        public string Description_ru { get; set; }
        public string Description_it { get; set; }
        public string Description_ar { get; set; }
        public string Description_ja { get; set; }
        public string Description_pt { get; set; }
        public string Description_zh { get; set; }
        public Nullable<bool> IsChargeable { get; set; }
        public string PriceLabel { get; set; }
        public int ChildUnitID { get; set; }
        public Nullable<bool> IsProviderNeeded { get; set; }
        public Nullable<bool> HasChildUnit { get; set; }
        public Nullable<bool> IsExtrabedItem { get; set; }
        public Nullable<bool> IsCheckedItem { get; set; }
        public Nullable<bool> IsExistingBedItem { get; set; }
    }
}
