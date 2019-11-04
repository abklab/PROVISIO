using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AccountBalanceApi.Models
{
    public class CustomerAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Customer Reference Number is required.")]
        public string ReferenceNumber { get; set; }
              
        public decimal CustomerBalance { get; set; }

        [Required(ErrorMessage = "Customer Account Number is required.")]
        public string B_AccountNumber { get; set; }

        [Required(ErrorMessage = "Customer Account Name is required.")]
        public string B_AccountName { get; set; }

        [Required(ErrorMessage = "Provide Banker Name.")]
        public string B_Name { get; set; }

        [Required(ErrorMessage = "Mono Phone Number is required.")]
        public string MomoNumber { get; set; }

        [Required(ErrorMessage = "Provide the Name of Mono Provider.")]
        public string MomoProvider { get; set; }

        [Required(ErrorMessage = "Customer Contact Number is required.")]
        public string TelephoneNumber { get; set; }

        public DateTime? LastAccessed { get; set; }
        public string Notes { get; set; }
    }
}