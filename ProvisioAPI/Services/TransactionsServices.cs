using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ProvisioAPI.DAL;
using ProvisioAPI.Models;

namespace ProvisioAPI.Services
{
    public class TransactionsServices
    {
        private PROVISIODBContext db;

        /// <summary>
        /// Service Error Message.
        /// </summary>
        public string TransactionErrorMessage { get; set; }

        /// <summary>
        /// Get all customers accounts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CustomerAccountDTO> GetAll_CustomersAccounts()
        {
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var customersDTO = new List<CustomerAccountDTO>()
                        ;
                    var customers = db.CustomerAccount.ToList();

                    if (customers != null && customers.Count > 0)
                    {

                        foreach (var customer in customers)
                        {
                            var _customer = new CustomerAccountDTO
                            {
                                Id = customer.Id,
                                ReferenceNumber = customer.ReferenceNumber,
                                B_AccountNumber = customer.B_AccountNumber,
                                B_AccountName = customer.B_AccountName,
                                B_Name = customer.B_Name,
                                MomoNumber = customer.MomoNumber,
                                MomoProvider = customer.MomoProvider,
                                CustomerBalance = customer.CustomerBalance,
                                TelephoneNumber = customer.TelephoneNumber,
                                LastAccessed = customer.LastAccessed,
                                Notes = customer.Notes
                            };
                            customersDTO.Add(_customer);
                        }
                    }
                    return customersDTO;
                }
            }
            catch (Exception ex)
            {
                TransactionErrorMessage = ex.Message;
                return null;
            }
        }

        public async Task<CustomerAccountDTO> GetCustomerByReferenceOrAccountNumber(string reference_number)
        {
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var customer = await db.CustomerAccount.FirstOrDefaultAsync(c => c.ReferenceNumber == reference_number || c.B_AccountNumber == reference_number);
                    return new CustomerAccountDTO
                    {
                        Id = customer.Id,
                        ReferenceNumber = customer.ReferenceNumber,
                        B_AccountNumber = customer.B_AccountNumber,
                        B_AccountName = customer.B_AccountName,
                        B_Name = customer.B_Name,
                        MomoNumber = customer.MomoNumber,
                        MomoProvider = customer.MomoProvider,
                        CustomerBalance = customer.CustomerBalance,
                        TelephoneNumber = customer.TelephoneNumber,
                        LastAccessed = customer.LastAccessed,
                        Notes = customer.Notes
                    };
                }
            }
            catch (Exception ex)
            {
                TransactionErrorMessage = ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Get customer by Reference Number 
        /// </summary>
        /// <param name="reference_number"></param>
        /// <returns></returns>
        public CustomerAccountDTO GetCustomerByReference(string reference_number)
        {
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var customer = db.CustomerAccount.FirstOrDefault(c => c.ReferenceNumber == reference_number);
                    return new CustomerAccountDTO
                    {
                        Id = customer.Id,
                        ReferenceNumber = customer.ReferenceNumber,
                        B_AccountNumber = customer.B_AccountNumber,
                        B_AccountName = customer.B_AccountName,
                        B_Name = customer.B_Name,
                        MomoNumber = customer.MomoNumber,
                        MomoProvider = customer.MomoProvider,
                        CustomerBalance = customer.CustomerBalance,
                        TelephoneNumber = customer.TelephoneNumber,
                        LastAccessed = customer.LastAccessed,
                        Notes = customer.Notes
                    };
                }
            }
            catch (Exception ex)
            {
                TransactionErrorMessage = ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Get customer by Banck Account Number 
        /// </summary>
        /// <param name="Account_Number"></param>
        /// <returns></returns>
        public CustomerAccountDTO GetCustomerByAccountNumber(string account_number)
        {
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var customer = db.CustomerAccount.FirstOrDefault(c => c.B_AccountNumber == account_number);
                    return new CustomerAccountDTO
                    {
                        Id = customer.Id,
                        ReferenceNumber = customer.ReferenceNumber,
                        B_AccountNumber = customer.B_AccountNumber,
                        B_AccountName = customer.B_AccountName,
                        B_Name = customer.B_Name,
                        MomoNumber = customer.MomoNumber,
                        MomoProvider = customer.MomoProvider,
                        CustomerBalance = customer.CustomerBalance,
                        TelephoneNumber = customer.TelephoneNumber,
                        LastAccessed = customer.LastAccessed,
                        Notes = customer.Notes
                    };
                }
            }
            catch (Exception ex)
            {
                TransactionErrorMessage = ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Get customer by Reference Number 
        /// </summary>
        /// <param name="momo_number"></param>
        /// <returns></returns>
        public CustomerAccountDTO GetCustomerByMomoNumber(string momo_number)
        {
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var customer = db.CustomerAccount.FirstOrDefault(c => c.MomoNumber == momo_number);
                    return new CustomerAccountDTO
                    {
                        Id = customer.Id,
                        ReferenceNumber = customer.ReferenceNumber,
                        B_AccountNumber = customer.B_AccountNumber,
                        B_AccountName = customer.B_AccountName,
                        B_Name = customer.B_Name,
                        MomoNumber = customer.MomoNumber,
                        MomoProvider = customer.MomoProvider,
                        CustomerBalance = customer.CustomerBalance,
                        TelephoneNumber = customer.TelephoneNumber,
                        LastAccessed = customer.LastAccessed,
                        Notes = customer.Notes
                    };
                }
            }
            catch (Exception ex)
            {
                TransactionErrorMessage = ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Create a Customer account
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<string> AddCustomerAccount(CustomerAccountDTO customer)
        {
            if(customer==null)
            {
                return "Please provide a valid customer information.";
            }
            try
            {

                using (db = new PROVISIODBContext())
                {
                    var IsExistingCustomer = db.CustomerAccount.Any(c => c.ReferenceNumber == customer.ReferenceNumber || c.B_AccountNumber == customer.B_AccountNumber || c.MomoNumber == customer.MomoNumber);
                    if (IsExistingCustomer)
                    {
                        TransactionErrorMessage = "There's an existing customer with the same Reference/Account number or Momo number";
                        return null;
                    }

                    var new_customer = new CustomerAccount
                    {
                        Id = customer.Id,
                        ReferenceNumber = customer.ReferenceNumber,
                        B_AccountNumber = customer.B_AccountNumber,
                        B_AccountName = customer.B_AccountName,
                        B_Name = customer.B_Name,
                        MomoNumber = customer.MomoNumber,
                        MomoProvider = customer.MomoProvider,
                        CustomerBalance = customer.CustomerBalance,
                        TelephoneNumber = customer.TelephoneNumber,
                        LastAccessed = customer.LastAccessed,
                        Notes = customer.Notes
                    };

                    db.CustomerAccount.Add(new_customer);

                    var i = await db.SaveChangesAsync();

                    return i > 0 ? "Account Created Successfully" : "Unale to Create Account, please contact API provider.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        /// <summary>
        /// Get all transactions accounts
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TransactionDTO>> GetAll_TransactionsAsync()
        {
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var transactionsDTO = new List<TransactionDTO>();

                    var _transactionsList = await db.Transactions.ToListAsync();

                    if (_transactionsList != null && _transactionsList.Count > 0)
                    {

                        foreach (var transaction in _transactionsList)
                        {
                            var _transaction = new TransactionDTO
                            {
                                EntryID = transaction.EntryID,
                                ReferenceNumber = transaction.ReferenceNumber,
                                B_AcountNumber = transaction.B_AcountNumber,
                                TransactionAmount = transaction.TransactionAmount,
                                TransactionType = transaction.TransactionType,
                                Comments = transaction.Comments,
                                TansactionDate = transaction.TansactionDate,
                                TransactionCode = transaction.TransactionCode
                            };
                            transactionsDTO.Add(_transaction);
                        }
                    }

                    return transactionsDTO;
                }
            }
            catch (Exception ex)
            {
                TransactionErrorMessage = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Get all transactions By Reference or Account Number
        /// </summary>
        /// <param name="reference_account_number">Reference or Account number</param>
        /// <returns>TransactionDTO</returns>
        public async Task<TransactionDTO> Get_TransactionsByReferenceOrAccountNumber(string reference_account_number)
        {
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var transaction = await db.Transactions.FirstOrDefaultAsync(t => t.ReferenceNumber == reference_account_number || t.B_AcountNumber == reference_account_number);

                    if (transaction != null)
                    {

                        var transactionsDTO = new TransactionDTO
                        {
                            EntryID = transaction.EntryID,
                            ReferenceNumber = transaction.ReferenceNumber,
                            B_AcountNumber = transaction.B_AcountNumber,
                            TransactionAmount = transaction.TransactionAmount,
                            TransactionType = transaction.TransactionType,
                            Comments = transaction.Comments,
                            TansactionDate = transaction.TansactionDate,
                            TransactionCode = transaction.TransactionCode
                        };
                        return transactionsDTO;
                    }
                }
            }
            catch (Exception ex)
            {
                TransactionErrorMessage = ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Get all transactions By EntryID
        /// </summary>
        /// <param name="entryID">EntryIDr</param>
        /// <returns>TransactionDTO</returns>
        public async Task<TransactionDTO> Get_TransactionsByEntryID(int entryID)
        {
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var transaction = await db.Transactions.FindAsync(entryID);

                    if (transaction != null)
                    {

                        var transactionsDTO = new TransactionDTO
                        {
                            EntryID = transaction.EntryID,
                            ReferenceNumber = transaction.ReferenceNumber,
                            B_AcountNumber = transaction.B_AcountNumber,
                            TransactionAmount = transaction.TransactionAmount,
                            TransactionType = transaction.TransactionType,
                            Comments = transaction.Comments,
                            TansactionDate = transaction.TansactionDate,
                            TransactionCode = transaction.TransactionCode
                        };
                        return transactionsDTO;
                    }
                }
            }
            catch (Exception ex)
            {
                TransactionErrorMessage = ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Create or Post a transaction
        /// </summary>
        /// <param name="trans">Transaction details</param>
        /// <returns></returns>
        public async Task<string> Create_Transactions(TransactionDTO trans)
        {
            string msg = "";

            try
            {
                using (db = new PROVISIODBContext())
                {
                    //get associated customer accont and update the balance
                    var customer = await db.CustomerAccount.FirstOrDefaultAsync(c => c.ReferenceNumber == trans.ReferenceNumber);

                    if (customer == null)
                    {
                        msg = $"No customer found with Reference Number: {trans.ReferenceNumber}.";
                    }
                    else
                    {
                        var t = await db.Transactions.FindAsync(trans.EntryID);
                        if (t == null)//transaction found
                        {
                            var transaction = new Transactions
                            {
                                EntryID = trans.EntryID,
                                ReferenceNumber = trans.ReferenceNumber,
                                B_AcountNumber = trans.ReferenceNumber,
                                TransactionType = trans.TransactionType,
                                Comments = trans.Comments,
                                TransactionAmount = trans.TransactionAmount,
                                TansactionDate = trans.TansactionDate,
                                TransactionCode = trans.TransactionCode
                            };

                            db.Transactions.Add(transaction);

                            customer.CustomerBalance = GetNewBalance(customer.CustomerBalance, trans.TransactionAmount, trans.TransactionType);

                            var i = await db.SaveChangesAsync();

                            msg = i > 0 ? "Transaction Saved successfully" : "Something went wrong. Try again later";
                        }
                        else //transaction with found with the EntryID
                        {
                            //update the customer 
                            msg = await Update_TransactionsAsync(trans);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        /// <summary>
        /// Update an existing transaction, by EntryID,
        /// </summary>
        /// <param name="transaction">Transaxtion object</param>
        /// <returns>Response indicating success or failure</returns>
        public async Task<string> Update_TransactionsAsync(TransactionDTO transaction)
        {
            string msg = "";
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var customer = await db.CustomerAccount.FirstOrDefaultAsync(c => c.ReferenceNumber == transaction.ReferenceNumber);

                    if (customer == null)
                    {
                        msg = $"No customer found with Reference Number: {transaction.ReferenceNumber}.";
                    }
                    else
                    {
                        var t = await db.Transactions.FindAsync(transaction.EntryID);

                        if (t == null)
                        {
                            msg = "No transaction found to Update";
                        }
                        else
                        {
                            t.EntryID = transaction.EntryID;
                            t.TransactionAmount = transaction.TransactionAmount;
                            t.TansactionDate = transaction.TansactionDate;
                            t.TransactionCode = transaction.TransactionCode;
                            t.Comments = transaction.Comments;
                            t.LastUpdated = DateTime.Now;

                            customer.CustomerBalance = GetNewBalance(customer.CustomerBalance, transaction.TransactionAmount, transaction.TransactionType);
                        }
                        var i = await db.SaveChangesAsync();
                        msg = i > 0 ? "Transaction Saved successfully" : "Something went wrong. Try again later";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

      

        /// <summary>
        /// This gets the new balance after transaction is posted
        /// </summary>
        /// <param name="currentBalance">current Balance</param>
        /// <param name="transactionAmount">transaction Amount</param>
        /// <param name="transType">Credit or Debit</param>
        /// <returns></returns>
        private decimal GetNewBalance(decimal currentBalance, decimal transactionAmount, string transType = "Debit")
        {
            return transType == "Debit" ? (currentBalance - transactionAmount) : (currentBalance + transactionAmount);
        }

        public async Task<string> DeleteTansaction(int entryID)
        {
            try
            {
                using (db = new PROVISIODBContext())
                {
                    var transaction = await db.Transactions.FindAsync(entryID);
                    if (transaction == null)
                    {
                        return $"This transaction could not be found to delete.";
                    }

                    db.Transactions.Remove(transaction);
                    var i = await db.SaveChangesAsync();
                    return i > 0 ? "Transaction deleted successfully" : "Something went wrong, try again later.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    } 


    /// <summary>
        /// Update customer record upon every transaction
        /// </summary>
        /// <param name="currentBalance"></param>
        /// <param name="transactionAmount"></param>
        /// <param name="transType"></param>
        /// <returns></returns>
        //public string UpdateCustomerBalance(TransactionDTO transaction)
        //{
        //    try
        //    {

        //        using (db = new PROVISIODBContext())
        //        {
        //            var _customer = db.CustomerAccount.FirstOrDefault(c => c.ReferenceNumber == transaction.ReferenceNumber);

        //            if (_customer == null)
        //            {
        //                TransactionErrorMessage = $"There's no existing customer with the  Reference number: {transaction.ReferenceNumber}";
        //                return null;
        //            }

        //            _customer.CustomerBalance = GetNewBalance(_customer.CustomerBalance, transaction.TransactionAmount, transaction.TransactionType);

        //            _customer.LastAccessed = DateTime.Now;
        //            _customer.Notes = string.IsNullOrWhiteSpace(_customer.Notes) ? transaction.Comments : $"{_customer.Notes}; {transaction.Comments}";

        //            var i = db.SaveChanges();
        //            return i > 0 ? "Account updated successfully." : "Sorry something went wrong. Try again later";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }

        //}
}