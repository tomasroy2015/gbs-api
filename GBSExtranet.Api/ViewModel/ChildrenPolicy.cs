using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSExtranet.Api.ViewModel
{
    public class ChildrenPolicy
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }      
        public string Description{ get; set; }      
        public Nullable<short> Sort { get; set; }
        public bool Active { get; set; }
        public Nullable<int> PolicyType { get; set; }
        public Nullable<System.DateTime> OpDateTime { get; set; }
        public Nullable<long> OpUserID { get; set; }
        public List<ChildrenPolicyItem> ChildrenPolicyItems { get; set; }
    }
}