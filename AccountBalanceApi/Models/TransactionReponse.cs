using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountBalanceApi.Models
{
    public class TransactionReponse
    {
        public int ResponseCode { get; set; }
        public string StatusCode { get; set; }
        public string ResponseDateTime { get; set; }
    }
}