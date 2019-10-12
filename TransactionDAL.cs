using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.Pecunia.DataAccessLayer;
using System.IO;
using Newtonsoft.Json;
using Capgemini.Pecunia.Exceptions;
using Capgemini.Pecunia.Entities;
using Capgemini.Pecunia.Contracts.DALContracts;

namespace Capgemini.Pecunia.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting Transaction from Transactions collection.
    /// </summary>
    [Serializable]
    public class TransactionDAL: TransactionDALBase, IDisposable
    {
        
        public static List<Transaction> Transactions = new List<Transaction>() { };
        public List<Transaction> TransactionsToSerialize = new List<Transaction>() { };
        private string filepath = "Transactions.txt";

        /// <summary>
        /// Stores all transaction records to Transactions collection.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <param name="amount">Amount to be transacted.</param>
        /// <param name="type">Type of transaction such as credit, debit.</param>
        /// <param name="mode">Mode of transaction such as cheque or withdrawal slip.</param>
        /// <param name="chequeNumber">Cheque number if mode of transaction is cheque and null in case of withdrawal slip.</param>
        /// <returns>Determinates whether the transactions are stored.</returns>
        public override bool StoreTransactionRecord(Guid accountID, double amount, TypeOfTranscation type, 
            ModeOfTransaction mode, string chequeNumber)
        {

            try
            {
                //// retrieving customerID based on account No
                AccountDAL accountDAL = new AccountDAL();
                List<Account> accounts = accountDAL.DeserializeFromJSON("AccountData.txt");
                string customerID = "00000000000000";// dummy initialization to avoid warnings
                foreach (Account account in accounts)
                {
                    if (account.AccountID == accountID)
                        customerID = account.CustomerID.ToString();
                }

                DateTime time = DateTime.Now;
                Guid TransactionID = Guid.NewGuid();

                Transaction transaction = new Transaction();
                transaction.AccountID = accountID;
                transaction.Type = type;
                transaction.Amount = amount;
                transaction.TransactionID = TransactionID;
                transaction.DateOfTransaction = time;
                transaction.Mode = mode;
                transaction.ChequeNumber = chequeNumber;

                List<Transaction> TransactionsRecords = DeserializeFromJSON(filepath);
                TransactionsRecords.Add(transaction);
                return SerializeIntoJSON(TransactionsRecords, filepath);
            }

            catch (PecuniaException)
            {
                throw new StoreTransactionException("Transaction not stored.");
            }
        }

        /// <summary>
        /// Checks if a particular transaction ID exists in Transactions collection.
        /// </summary>
        /// <param name="transactionID">Uniquely generated transaction ID.</param>
        /// <returns>Determinates whether the transaction ID exists.</returns>
        public override bool TransactionIDExistsDAL(Guid transactionID)
        {
            bool transactionIDExists = false;
            try
            {
                List<Transaction> transactionsList = DeserializeFromJSON("Transactions.txt");
                
                foreach (Transaction transaction in transactionsList)
                {
                    if (transaction.TransactionID == transactionID)
                    {
                        transactionIDExists = true;
                    }

                }
                return transactionIDExists;
            }
            catch (PecuniaException)
            {
                throw new TransactionIDExistsException("Transaction ID does not exist.");
            }          
        }


        /// <summary>
        /// Debit type of transaction with mode of transaction as withdrawal slip.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <param name="amount">Amount to be debited.</param>       
        /// <returns>Determinates whether the amount is debited by withdrawal slip.</returns>
        public override bool DebitTransactionByWithdrawalSlipDAL(Guid accountID, double amount)
        {
            bool transactionWithdrawal = false;
            try
            {
                AccountDAL accountDAL = new AccountDAL();
                List<Account> accounts = accountDAL.DeserializeFromJSON("AccountData.txt");
                foreach (Account account in accounts)
                {
                    if (account.AccountID == accountID)
                    {
                        if (account.Balance >= amount)
                        {
                            account.Balance = account.Balance - amount;
                            TypeOfTranscation typeOfTransaction;
                            Enum.TryParse("Debit", out typeOfTransaction);
                            ModeOfTransaction modeOfTransaction;
                            Enum.TryParse("WithdrawalSlip", out modeOfTransaction);
                            StoreTransactionRecord(accountID, amount, typeOfTransaction, modeOfTransaction, null);
                            transactionWithdrawal = true;
                            accountDAL.SerialiazeIntoJSON(accounts, "AccountData.txt");
                            break;
                        }
                    }                             
                }
                return transactionWithdrawal;
            }
            catch (PecuniaException)
            {
                throw new InsufficientBalanceException("Insufficient Balance");
            }
        }

        /// <summary>
        /// Credit type of transaction with mode of transaction as withdrawal slip.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <param name="amount">Amount to be credited.</param>       
        /// <returns>Determinates whether the amount is credited by withdrawal slip.</returns>
        public override bool CreditTransactionByDepositSlipDAL(Guid accountID, double amount)
        {
            bool transactionDeposit = false;
            try
            {
                AccountDAL accountDAL = new AccountDAL();
                List<Account> accounts = accountDAL.DeserializeFromJSON("AccountData.txt");
                foreach (Account account in accounts)
                {
                    if (account.AccountID == accountID)
                    {
                        account.Balance = account.Balance + amount;
                        TypeOfTranscation typeOfTranscation;
                        Enum.TryParse("Credit", out typeOfTranscation);
                        ModeOfTransaction modeOfTransaction;
                        Enum.TryParse("WithdrawalSlip", out modeOfTransaction);
                        StoreTransactionRecord(accountID, amount, typeOfTranscation, modeOfTransaction, null);
                        accountDAL.SerialiazeIntoJSON(accounts, "AccountData.txt");
                        transactionDeposit = true;                        
                    }
                }
                return transactionDeposit;
            }
            catch (PecuniaException)
            {

                throw new CreditSlipException("Invalid Account No or Amount");
            } 
        }


        /// <summary>
        /// Debit type of transaction with mode of transaction as cheque.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <param name="amount">Amount to be debited.</param>       
        /// <param name="chequeNumber">Cheque Number.</param>       
        /// <returns>Determinates whether the amount is debited by withdrawal slip.</returns>
        public override bool DebitTransactionByChequeDAL(Guid accountID, double amount, string chequeNumber)
        {
            bool transactionDebited = false;
            try
            {
                AccountDAL accountDAL = new AccountDAL();
                List<Account> accounts = accountDAL.DeserializeFromJSON("AccountData.txt");

                foreach (Account account in accounts)
                {
                    if (account.AccountID == accountID)
                    {
                        if (account.Balance >= amount)
                        {
                            account.Balance = account.Balance - amount;
                            TypeOfTranscation typeOfTranscation;
                            Enum.TryParse("Debit", out typeOfTranscation);
                            ModeOfTransaction modeOfTransaction;
                            Enum.TryParse("Cheque", out modeOfTransaction);
                            StoreTransactionRecord(accountID, amount, typeOfTranscation, modeOfTransaction, chequeNumber);
                            accountDAL.SerialiazeIntoJSON(accounts, "AccountData.txt");
                            transactionDebited = true;                         
                        }
                    }
                }
                return transactionDebited;
            }
            catch (PecuniaException)
            {

                throw new DebitChequeException("Invalid Account Credentials or Amount");
            }                   
        }


        /// <summary>
        /// Credit type of transaction with mode of transaction as cheque.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <param name="amount">Amount to be credited.</param>      
        /// <param name="chequeNumber">Cheque Number.</param>                
        /// <returns>Determinates whether the amount is credited by cheque.</returns>
        public override bool CreditTransactionByChequeDAL(Guid accountID, double amount, string chequeNumber)
        {
            
            bool transactionCredited = false;
            try
            {
                AccountDAL accountDAL = new AccountDAL();
                List<Account> accounts = accountDAL.DeserializeFromJSON("AccountData.txt");

                foreach (Account account in accounts)
                {
                    if (account.AccountID == accountID)
                    {
                        account.Balance = account.Balance + amount;
                        TypeOfTranscation typeOfTranscation;
                        Enum.TryParse("Credit", out typeOfTranscation);
                        ModeOfTransaction modeOfTransaction;
                        Enum.TryParse("Cheque", out modeOfTransaction);
                        StoreTransactionRecord(accountID, amount, typeOfTranscation, modeOfTransaction, chequeNumber);
                        accountDAL.SerialiazeIntoJSON(accounts, "AccountData.txt");
                        transactionCredited = true;

                    }
                }
                return transactionCredited;
            }
            catch (PecuniaException)
            {

                throw new CreditChequeException("Invalid Account Credentials or Amount");
            }
        }

        /// <summary>
        /// Displays all transactions for a particular account ID.
        /// </summary>
        /// <param name="accountID">Uniquely generated account ID.</param>
        /// <returns>Provides a list of transactions for a particular account ID.</returns>
        public override List<Transaction> DisplayTransactionByAccountIDDAL(Guid accountID)
        {
            try
            {
                List<Transaction> transactions = DeserializeFromJSON("Transactions.txt");
                List<Transaction> transactionsOfAccountID = new List<Transaction>();
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.AccountID == accountID)
                    {
                        transactionsOfAccountID.Add(transaction);
                    }

                }
                return transactionsOfAccountID;
            }
            catch (PecuniaException)
            {

                throw new TransactionDisplayAccountException("Invalid Account ID");
            }            
        }

        /// <summary>
        /// Displays transaction for a particular transaction ID.
        /// </summary>
        /// <param name="transactionID">Uniquely generated transaction ID.</param>
        /// <returns>Provides transaction details for a particular transaction ID.</returns>
        public override Transaction DisplayTransactionByTransactionIDDAL(Guid transactionID)
        {
            try
            {
                List<Transaction> transactions = DeserializeFromJSON("Transactions.txt");
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.TransactionID == transactionID)
                    {
                        return transaction;
                    }
                }
                return null;
            }
            catch (PecuniaException)
            {

                throw new TransactionDisplayAccountException("Invalid Transaction ID");
            }            
        }

        /// <summary>
        /// Gets all Transactions from the collection.
        /// </summary>
        /// <returns>Returns list of all Transactions.</returns>
        public override List<Transaction> GetAllTransactionsDAL()
        {
            try
            {
                List<Transaction> transactions = DeserializeFromJSON("Transactions.txt");
                return transactions;
            }
            catch (PecuniaException)
            {

                throw new GetAllTransactionException("Transactions not found.");
            }           
        }

        /// <summary>
        /// Reads all Transactions from the file.
        /// </summary>
        /// <param name="fileName">Name of file where data is to be read.</param>
        /// <returns>Returns list of all Transactions.</returns>
        public override List<Transaction> DeserializeFromJSON(string fileName)
        {
            try
            {
                List<Transaction> transactionsList = JsonConvert.DeserializeObject<List<Transaction>>(File.ReadAllText(fileName));// Done to read data from file
                return transactionsList;
            }
            catch 
            {
                throw;
            }           
        }

        /// <summary>
        /// Writes all Transactions to the file.
        /// </summary>
        /// <param name="transactions">List of transactions.</param>
        /// <param name="fileName">Name of file where data is to be written.</param>
        /// <returns>Determinates if the data is written in the file.</returns>
        public override bool SerializeIntoJSON(List<Transaction> transactions, string fileName)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter streamWriter = new StreamWriter(fileStream))   //filename is used so that we can have access over our own file
                using (JsonWriter writer = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(writer, transactions);
                    streamWriter.Close();
                    fileStream.Close();
                    return true;
                }
            }
            catch
            {
               throw;
            }
        }

        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently
        }

    }
}
