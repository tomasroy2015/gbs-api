using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBSExtranet.Repository 
{
    public class TB_Attribute
    {
        [Key]
        public int ID { get; set; }
        public int PartID { get; set; }
        public int AttributeTypeID { get; set; }
        public Nullable<int> AttributeHeaderID { get; set; }
        public string Name_tr { get; set; }
        public string Name_en { get; set; }
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
        public Nullable<int> DataTypeID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public Nullable<bool> Chargeable { get; set; }
        public Nullable<bool> IsFilter { get; set; }
        public Nullable<short> Sort { get; set; }
        public bool Active { get; set; }
        public Nullable<int> RelatedAttributeID { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string Icons { get; set; }
    }
}
