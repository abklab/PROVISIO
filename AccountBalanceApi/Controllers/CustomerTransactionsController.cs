using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AccountBalanceApi.Models;

namespace AccountBalanceApi.Controllers
{
    [RoutePrefix("provisio/v1000/api")]
    public class CustomerTransactionsController : ApiController
    {
        private readonly DataService services = new DataService();

        [Route("get_customer_accounts")]
        [ResponseType(typeof(CustomerAccount))]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var accounts = services.GetCustomerAccounts();
            return Ok(accounts);
        }

        [HttpGet]
        [Route("get_customer_account/{accountnumber}/_account")]
        [ResponseType(typeof(CustomerAccount))]
        public IHttpActionResult Get(string accountnumber)
        {
            var response = services.GetCustomerAccountsByAccountMomoNumber(accountnumber);
            return Ok(response);
        }

        [HttpPost]
        [Route("_customer_account/_post_transaction")]
        public IHttpActionResult Post([FromBody]CustomerAccount account)
        {
            var accountToUpdate = new CustomerAccount();
            if (account != null)
            {
                if (string.IsNullOrWhiteSpace(account.B_AccountNumber) && string.IsNullOrWhiteSpace(account.MomoNumber))
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }
               
                var response = services.CreateTransaction(account);
               
                return Ok(response);

            }
            return StatusCode(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [Route("_customer_account/_update_transaction")]
        public IHttpActionResult UpdateTransaction([FromBody]CustomerAccount account)
        {
            if (string.IsNullOrWhiteSpace(account.B_AccountNumber) && string.IsNullOrWhiteSpace(account.MomoNumber))
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            var accountToUpdate = new CustomerAccount();
            if (!string.IsNullOrWhiteSpace(account.B_AccountNumber))
            {
                accountToUpdate = services.GetCustomerAccountsByAccountMomoNumber(account.B_AccountNumber);
            }
            else
            {
                accountToUpdate = services.GetCustomerAccountsByAccountMomoNumber(account.B_AccountNumber);
            }

            if (accountToUpdate != null)
            {
                account.ReferenceNumber = accountToUpdate.ReferenceNumber;
                
                var response = services.UpdateAccount(account);

                return Ok(response);
            }
            return StatusCode(HttpStatusCode.NotFound);
        }

        // DELETE: api/CustomerTransactions/5
        public void Delete(int id)
        {
        }
    }
}
