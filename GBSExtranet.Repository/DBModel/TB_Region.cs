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
    public class TB_Region
    {
        [Key]
        public long ID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<long> ParentID { get; set; }
        public Nullable<long> SecondParentID { get; set; }
        public string RegionType { get; set; }
        public string SubRegionType { get; set; }
        public string Name { get; set; }
        public string NameASCII { get; set; }
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
        public string Code { get; set; }
        public Nullable<long> Population { get; set; }
        public Nullable<bool> IsIncludedInDestinationSearch { get; set; }
        public Nullable<bool> IsCity { get; set; }
        public bool IsPopular { get; set; }
        public bool IsFilter { get; set; }
        public bool IsMainPageDisplay { get; set; }
        public Nullable<int> MainPageDisplaySort { get; set; }
        public Nullable<bool> HasCityTax { get; set; }
        public Nullable<long> HitCount { get; set; }
        public Nullable<short> Sort { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<int> MapZoomIndex { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public string Msgtopdest { get; set; }
        public string Msgtopdest_en { get; set; }
        public string Msgtopdest_ar { get; set; }
        public string Image { get; set; }
    }
}
