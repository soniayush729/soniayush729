using System;
using Pecunia.Exceptions;
using Pecunia.DataAccessLayer;
using System.Text.RegularExpressions;
using Pecunia.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pecunia.Contracts;

namespace Pecunia.BusinessLayer
{
    public class TransactionsBL
    {
        public async Task<bool> DebitTransactionByWithdrawalSlipBL(Guid AccountID, double Amount)
        {
            bool transactionWithdrawal = false;
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if (Amount <= 50000 && accountnoexist.AccountIDExists(AccountID) == true)
                {
                    await Task.Run(() =>
                    {
                        TransactionDAL debit = new TransactionDAL();
                        debit.DebitTransactionByWithdrawalSlipDAL(AccountID, Amount);
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
        public async Task<bool> CreditTransactionByDepositSlipBL(Guid AccountID, Double Amount)
        {
            bool transactionWithdrawal = false;
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if (accountnoexist.AccountIDExists(AccountID) == true && Amount <= 50000)
                {
                    await Task.Run(() => 
                    {
                        TransactionDAL credit = new TransactionDAL();
                        credit.CreditTransactionByDepositSlipDAL(AccountID, Amount);
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
        public async Task<bool> DebitTransactionByChequeBL(Guid AccountID, double Amount, string ChequeNumber)
        {
            bool transactionCheque = false;
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if (accountnoexist.AccountIDExists(AccountID) == true && Amount <= 50000 && ChequeNumber.Length == 10 && (Regex.IsMatch(ChequeNumber, "[A-Z0-9]$") == true))
                {
                    await Task.Run(() =>
                    {
                        TransactionDAL Cheque = new TransactionDAL();
                        Cheque.DebitTransactionByChequeDAL(AccountID, Amount, ChequeNumber);
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
        public async Task<bool> CreditTransactionByChequeBL(Guid AccountID, double Amount, string ChequeNumber)
        {
            bool transactionCheque = false;
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if (accountnoexist.AccountIDExists(AccountID) == true && await ValidateChequeNumber(ChequeNumber) == true && Amount <= 50000)
                {
                    await Task.Run(() =>
                    {
                        TransactionDAL Cheque = new TransactionDAL();
                        Cheque.CreditTransactionByChequeDAL(AccountID, Amount, ChequeNumber);
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
       
        public async Task DisplayTransactionByAccountIDBL(Guid AccountID)
        {
            try
            {
                AccountDAL accountnoexist = new AccountDAL();
                if (accountnoexist.AccountIDExists(AccountID) == true)
                {
                    await Task.Run(() =>
                    {
                        TransactionDAL transaction = new TransactionDAL();
                        transaction.DisplayTransactionByAccountIDDAL(AccountID);
                    });
                   
                }
            }
            catch (PecuniaException ex)
            {
                throw new TransactionDisplayAccountException(ex.Message);
            }
           
            
        }

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

    }
}
