using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class Region
    {
        public long ID { get; set; }
        public string CountryID { get; set; }

        public string Country { get; set; }
        public string ParentID { get; set; }
        public string secondParentID { get; set; }
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
        public string Population { get; set; }
        public bool IsIncludedInSearch { get; set; }
        public bool IsCity { get; set; }
        public bool IsPopular { get; set; }
        public bool IsFilter { get; set; }
        public bool IsMainPageDisplay { get; set; }
        public string MainPageDisplaySort { get; set; }
        public string HitCount { get; set; }
        public string Sort { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MapZoomIndex { get; set; }
        public bool CityTax { get; set; }
        public long OpUserID { get; set; }
        public bool Active { get; set; }
        public string Image { get; set; }
    }
}