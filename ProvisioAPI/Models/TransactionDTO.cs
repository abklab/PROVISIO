using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProvisioAPI.Models
{
    public class TransactionDTO
    {
        [Key]
        public int EntryID { get; set; }

        [Required(ErrorMessage ="Please provide Account Number")]
        public string B_AcountNumber { get; set; }

        [Required(ErrorMessage = "Please provide Reference Number")]
        public string ReferenceNumber { get; set; }

        [Required(ErrorMessage = "Transaction Type is required (Credit or Debit)")]
        public string TransactionType { get; set; }

        [Required(ErrorMessage = "Please provide Transaction Amount")]
        public decimal TransactionAmount { get; set; }
        public DateTime? TansactionDate { get; set; }
        public string Comments { get; set; }
        public string TransactionCode { get; set; }//100 - Approved , 300 - Rejected.
    }
}