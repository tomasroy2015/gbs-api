using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class PropertyPolicyItems
    {
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
        public Nullable<int> PriceUnitID { get; set; }
        public bool Active { get; set; }
        public string RelatedIcon { get; set; }
        public int HotelID { get; set; }
        public Nullable<int> UnitID { get; set; }
        public string UnitValue { get; set; }
        public Nullable<decimal> Price { get; set; }
        public int CurrencyID { get; set; }
        public List<PropertyPolicyUnits> PolicyItemUnits { get; set; }
    }
}