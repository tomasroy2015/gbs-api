//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GBSExtranet.Api.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_HitCount
    {
        public long ID { get; set; }
        public int PartID { get; set; }
        public long RecordID { get; set; }
        public System.DateTime Date { get; set; }
        public long HitCount { get; set; }
        public string Description { get; set; }
    
        public virtual TB_Part TB_Part { get; set; }
    }
}
