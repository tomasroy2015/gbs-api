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
    public class TB_Country
    {
        [Key]
        public int ID { get; set; }
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
        public string CultureCode { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<int> VAT { get; set; }
        public Nullable<bool> HasCityTax { get; set; }
        public Nullable<long> HitCount { get; set; }
        public Nullable<short> Sort { get; set; }
        public bool Active { get; set; }
        public System.DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
        public Nullable<int> TempCode { get; set; }
        public string ContinentsID { get; set; }
    }
}
