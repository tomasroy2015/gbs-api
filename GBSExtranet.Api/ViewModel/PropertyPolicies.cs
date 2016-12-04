using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyPolicies
    {
        public int ID { get; set; }
        public string Code { get; set; }
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
        public Nullable<short> Sort { get; set; }
        public string Icons { get; set; }
        public bool Active { get; set; }
        public List<PropertyPolicyItems> PropertyPolicyItems { get; set; }
    }
}