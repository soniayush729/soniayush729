using System;
using Capgemini.Pecunia.DataAccessLayer;
using System.Text.RegularExpressions;
using Capgemini.Pecunia.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Capgemini.Pecunia.Entities;
using Capgemini.Pecunia.Contracts.BLContracts;
using Capgemini.Pecunia.Contracts.DALContracts;


namespace Capgemini.Pecunia.BusinessLayer
{
    /// <summary>
    /// Contains business layer methods for validating, inserting, updating, deleting Employees from Employees collection.
    /// </summary>
    public class TransactionBL : BLbase<Transaction>, ITransactionBL, IDisposable
    {

        //fields
        TransactionDALBase transactionDAL;

        /// <summary>
        /// Constructor.
        /// </summary>
        public TransactionBL()
        {
            this.transactionDAL = new TransactionDAL();
        }

        /// <summary>
        /// Debit type of transaction with mode of transaction as withdrawal slip.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <param name="amount">Amount to be debited.</param>       
        /// <returns>Determinates whether the amount is debited by withdrawal slip.</returns>
        public async Task<bool> DebitTransactionByWithdrawalSlipBL(Guid accountID, double amount)
        {
            bool transactionWithdrawal = false;
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if ((amount <= 50000) && (amount > 0) && accountnoexist.AccountIDExists(accountID) == true)
                {
                    await Task.Run(() =>
                    {
                        TransactionDAL transactionDAL = new TransactionDAL();
                        transactionDAL.DebitTransactionByWithdrawalSlipDAL(accountID, amount);
                        transactionWithdrawal = true;
                    });                       
                                      
                }
                return transactionWithdrawal;
            }
            catch (PecuniaException ex)
            {
                throw new InsufficientBalanceException(ex.Message);
            }
           
        }

        /// <summary>
        /// Credit type of transaction with mode of transaction as withdrawal slip.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <param name="amount">Amount to be credited.</param>       
        /// <returns>Determinates whether the amount is credited by withdrawal slip.</returns>
        public async Task<bool> CreditTransactionByDepositSlipBL(Guid accountID, double amount)
        {
            bool transactionWithdrawal = false;
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if (accountnoexist.AccountIDExists(accountID) == true && (amount <= 50000) && (amount> 0))
                {
                    await Task.Run(() => 
                    {
                        TransactionDAL transactionDAL = new TransactionDAL();
                        transactionDAL.CreditTransactionByDepositSlipDAL(accountID, amount);
                        transactionWithdrawal = true;
                    });
                    
                }
                return transactionWithdrawal;
            }
            
            catch(PecuniaException ex)
            {
                throw new CreditSlipException(ex.Message);
            }
        }

        /// <summary>
        /// Debit type of transaction with mode of transaction as cheque.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <param name="amount">Amount to be debited.</param>       
        /// <param name="chequeNumber">Cheque Number.</param>       
        /// <returns>Determinates whether the amount is debited by withdrawal slip.</returns>
        public async Task<bool> DebitTransactionByChequeBL(Guid accountID, double amount, string chequeNumber)
        {
            bool transactionCheque = false;
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if (accountnoexist.AccountIDExists(accountID) == true && (amount <= 50000) && (amount>0) && chequeNumber.Length == 6 && (Regex.IsMatch(chequeNumber, "[A-Z0-9]$") == true))
                {
                    await Task.Run(() =>
                    {
                        TransactionDAL transactionDAL = new TransactionDAL();
                        transactionDAL.DebitTransactionByChequeDAL(accountID, amount, chequeNumber);
                        transactionCheque = true;
                    });
                }
                return transactionCheque;
            }
            
           
            catch(PecuniaException ex)
            {
                throw new DebitChequeException(ex.Message);
            }

        }

        /// <summary>
        /// Credit type of transaction with mode of transaction as cheque.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <param name="amount">Amount to be credited.</param>      
        /// <param name="chequeNumber">Cheque Number.</param>                
        /// <returns>Determinates whether the amount is credited by cheque.</returns>
        public async Task<bool> CreditTransactionByChequeBL(Guid accountID, double amount, string chequeNumber)
        {
            bool transactionCheque = false;
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if (accountnoexist.AccountIDExists(accountID) == true && await ValidateChequeNumber(chequeNumber) == true && (amount <= 50000) && (amount>0)
                   )
                {
                    await Task.Run(() =>
                    {
                        TransactionDAL transactionDAL = new TransactionDAL();
                        transactionDAL.CreditTransactionByChequeDAL(accountID, amount, chequeNumber);
                        transactionCheque = true;
                    });
                    
                }
                return transactionCheque;
            }
            
            catch(PecuniaException ex)
            {
                throw new CreditChequeException(ex.Message);
            }
           
        }

        /// <summary>
        /// Displays all transactions for a particular account ID.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <returns>Provides a list of transactions for a particular account ID.</returns>
        public async Task DisplayTransactionByAccountIDBL(Guid accountID)
        {
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if (accountnoexist.AccountIDExists(accountID) == true)
                {
                    await Task.Run(() =>
                    {
                        TransactionDAL transactionDAL = new TransactionDAL();
                        transactionDAL.DisplayTransactionByAccountIDDAL(accountID);
                    });
                   
                }
            }
            catch (PecuniaException ex)
            {
                throw new TransactionDisplayAccountException(ex.Message);
            }
           
            
        }

        /// <summary>
        /// Checks if a particular transaction ID exists in Transactions collection.
        /// </summary>
        /// <param name="transactionID">Uniquely generated transaction ID.</param>
        /// <returns>Determinates whether the transaction ID exists.</returns>
        public async Task<bool> TransactionIDExistsBL(Guid transactionID)
        {
            bool transactionIDExists = false;
            try
            {
                TransactionDAL transactionDAL = new TransactionDAL();
                if(transactionDAL.TransactionIDExistsDAL(transactionID))
                {
                    await Task.Run(() =>
                    {
                        transactionIDExists = true;
                    });
                }
                return transactionIDExists;
            }
            catch (PecuniaException ex)
            {

                throw new TransactionIDExistsException(ex.Message);
            }
        }

        /// <summary>
        /// Displays transaction for a particular transaction ID.
        /// </summary>
        /// <param name="transactionID">Uniquely generated transaction ID.</param>
        /// <returns>Provides transaction details for a particular transaction ID.</returns>
        public async Task<Transaction> DisplayTransactionByTransactionIDBL(Guid transactionID)
        {
            try
            {
                Transaction transaction = new Transaction();
                if (await TransactionIDExistsBL(transactionID))
                {
                    await Task.Run(() => {
                        TransactionDAL transactionDAL = new TransactionDAL();
                        transaction = transactionDAL.DisplayTransactionByTransactionIDDAL(transactionID);
                    });
                   
                }
                return transaction;
            }
            catch (PecuniaException ex)
            {

                throw new TransactionDisplayAccountException(ex.Message);
            }
        }

        /// <summary>
        /// Validates Cheque Number.
        /// </summary>
        /// <param name="chequeNumber">Cheque Number.</param>
        /// <returns>Determinates whether the cheque number is valid or not.</returns>
        public async Task<bool> ValidateChequeNumber(string chequeNumber)
        {
            bool validChequeNumber = false;
            try
            {
                if (Regex.IsMatch(chequeNumber, "[0-9]{6}$") == true)
                {
                    await Task.Run(() => {
                        validChequeNumber = true;
                    });

                }
                return validChequeNumber;
            }
            catch (PecuniaException)
            {

                throw new ValidateChequeNumberException("Invalid Cheque Number.");
            }
            
        }

        /// <summary>
        /// Gets all Transactions from the collection.
        /// </summary>
        /// <returns>Returns list of all Transactions.</returns>
        public async Task<List<Transaction>> GetAllTransactionBL()
        {
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                await Task.Run(() => 
                {
                    TransactionDAL transactionDAL = new TransactionDAL();
                    transactions = transactionDAL.GetAllTransactionsDAL();
                });
                return transactions;
            }
            catch (PecuniaException ex)
            {

                throw new GetAllTransactionException(ex.Message);
            }
            
        }

        /// <summary>
        /// Disposes DAL object(s).
        /// </summary>
        public void Dispose()
        {
            ((TransactionDAL)transactionDAL).Dispose();
        }

    }
}
