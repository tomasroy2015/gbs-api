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
    
    public partial class B_GetBank_TB_Bank_SP_Result
    {
        public Nullable<int> ID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public string BankName { get; set; }
        public string BankBranchName { get; set; }
        public string BankAccountNumber { get; set; }
        public string IBAN { get; set; }
        public string SWIFT { get; set; }
        public string OtherInfo { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
    }
}
