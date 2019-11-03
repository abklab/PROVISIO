using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProvisioAPI.Models;
using ProvisioAPI.Services;

namespace ProvisioAPI.Controllers
{
    [RoutePrefix("_provisio/api/v001")]
    public class TransactionsController : ApiController
    {
        private readonly TransactionsServices services = new TransactionsServices();

        //// GET: _provisio_transactionsapi/Transactionslit
        //[Route("transactionslist")]
        //public GetCustomerAccounts() => services.GetAll_CustomersAccounts();

        //// GET: api/Transactions/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Transactions
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Transactions/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Transactions/5
        //public void Delete(int id)
        //{
        //}
    }
}
