//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProvisioAPI.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transactions
    {
        public int EntryID { get; set; }
        public string ReferenceNumber { get; set; }
        public string TransactionType { get; set; }
        public decimal TransactionAmount { get; set; }
        public Nullable<System.DateTime> TansactionDate { get; set; }
        public string Comments { get; set; }
        public string TransactionCode { get; set; }
        public string B_AcountNumber { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
    }
}
