using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProvisioAPI.Models;
using ProvisioAPI.Services;

namespace ProvisioAPI.Controllers
{
    [RoutePrefix("_provisio/api/v001")]
    public class CustomerAccountsController : ApiController
    {
        //private PROVISIODBContext db = new PROVISIODBContext();
        private readonly TransactionsServices services = new TransactionsServices();

        #region Customer Accounts

        [HttpGet]
        [Route("_customer_accounts")]
        public IEnumerable<CustomerAccountDTO> GetCustomerAccount()
        {
            return services.GetAll_CustomersAccounts();
        }

        [HttpGet]
        [Route("CustomerAccounts/{referenceOrAccountNumber}/_getaccount")]
        [ResponseType(typeof(CustomerAccountDTO))]
        public async Task<IHttpActionResult> GetCustomerAccount(string referenceOrAccountNumber)
        {
            var customerAccount = await services.GetCustomerByReferenceOrAccountNumber(referenceOrAccountNumber);

            return Ok(customerAccount);
        }


        [Route("CustomerAccounts/createaccount/_admin")]
        [ResponseType(typeof(string))]
        [HttpPost]
        public async Task<IHttpActionResult> PostCustomerAccount(CustomerAccountDTO customerAccount)
        {
            //if (customerAccount==null)
            //{
            //    return BadRequest(ModelState);
            //}

            var response = await services.AddCustomerAccount(customerAccount);

            return Ok(response);
        }

        #endregion


        #region Transactions

        /// <summary>
        /// get all Transactions
        /// </summary>      
        /// <returns></returns>
        [HttpGet]
        [Route("transactions/_getalltransactions")]
        [ResponseType(typeof(IEnumerable<TransactionDTO>))]
        public async Task<IHttpActionResult> GetAllTransactions()
        {
            var response =await services.GetAll_TransactionsAsync();

            return Ok(response);
        }

        /// <summary>
        /// get Transactions by Reference or Account number
        /// </summary>      
        /// <returns></returns>
        [HttpGet]
        [Route("transactions/{reference_account}/_getaccountinfo")]
        [ResponseType(typeof(IEnumerable<TransactionDTO>))]
        public async Task<IHttpActionResult> GetTransactionsByReferenceOrAccountNumber(string reference_account)
        {
            var response = await services.Get_TransactionsByReferenceOrAccountNumber(reference_account);

            return Ok(response);
        }


        /// <summary>
        /// Create Transactions
        /// </summary>
        /// <param name="transactionDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("transactions/add_transactions")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> CreateTransactions([FromBody]TransactionDTO transactionDTO)
        {
            if (transactionDTO==null)
            {
                return BadRequest(ModelState);
            }
            var response = await services.Create_Transactions(transactionDTO);

            return Ok(response);
        }

        [HttpPost]
        [Route("transactions/update_transactions")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> UpdateTransactions([FromBody]TransactionDTO customerAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await services.Update_TransactionsAsync(customerAccount);

            return Ok(response);
        }

        [HttpDelete]
        [Route("tranasctions/deleteaccount/{id}/_admin")]
        [ResponseType(typeof(string))]
        public async Task<IHttpActionResult> DeleteTransaction(int id)
        {
            var response = await services.DeleteTansaction(id);

            return Ok(response);
        }


        #endregion



    }
}