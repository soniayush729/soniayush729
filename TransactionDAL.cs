using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pecunia.Entities;
using Pecunia.DataAccessLayer;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Newtonsoft.Json;
using Pecunia.Exceptions;
using Pecunia.Contracts;


namespace Pecunia.DataAccessLayer
{
   
    [Serializable]
    public class TransactionDAL: TransactionDALAbstract
    {
        
        public static List<Transaction> Transactions = new List<Transaction>() { };
        public List<Transaction> TransactionsToSerialize = new List<Transaction>() { };
        private string filepath = "Transactions.txt";

        public override bool StoreTransactionRecord(Guid accountID, double Amount, TypeOfTranscation type, 
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
                        customerID = account.CustomerID;
                }

                DateTime time = DateTime.Now;
                Guid TransactionID = Guid.NewGuid();



                Transaction transaction = new Transaction();
                transaction.AccountID = accountID;
                transaction.Type = type;
                transaction.Amount = Amount;
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

        public override bool TransactionIDExistsDAL(Guid TransactionID)
        {
            bool transactionIDExists = false;
            try
            {
                List<Transaction> transactionsList = DeserializeFromJSON("Transactions.txt");
                
                foreach (Transaction transaction in transactionsList)
                {
                    if (transaction.TransactionID == TransactionID)
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
        public override bool DebitTransactionByWithdrawalSlipDAL(Guid AccountID, double Amount)
        {
            bool transactionWithdrawal = false;
            try
            {
                AccountDAL accountDAL = new AccountDAL();
                List<Account> accounts = accountDAL.DeserializeFromJSON("AccountData.txt");
                foreach (Account account in accounts)
                {
                    if (account.AccountID == AccountID)
                    {
                        if (account.Balance >= Amount)
                        {
                            account.Balance = account.Balance - Amount;
                            TypeOfTranscation transEnum;
                            Enum.TryParse("Debit", out transEnum);
                            ModeOfTransaction SlipEnum;
                            Enum.TryParse("WithdrawalSlip", out SlipEnum);
                            StoreTransactionRecord(AccountID, Amount, transEnum, SlipEnum, null);
                            transactionWithdrawal = true;
                            accountDAL.SerializeIntoJSON(accounts, "AccountData.txt");
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

        public override bool CreditTransactionByDepositSlipDAL(Guid AccountID, double Amount)
        {
            bool transactionDeposit = false;
            try
            {
                AccountDAL accountDAL = new AccountDAL();
                List<Account> accounts = accountDAL.DeserializeFromJSON("AccountData.txt");
                foreach (Account account in accounts)
                {
                    if (account.AccountID == AccountID)
                    {
                        account.Balance = account.Balance + Amount;
                        TypeOfTranscation transEnum;
                        Enum.TryParse("Credit", out transEnum);
                        ModeOfTransaction SlipEnum;
                        Enum.TryParse("WithdrawalSlip", out SlipEnum);
                        StoreTransactionRecord(AccountID, Amount, transEnum, SlipEnum, null);
                        accountDAL.SerializeIntoJSON(accounts, "AccountData.txt");
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

        public override bool DebitTransactionByChequeDAL(Guid AccountID, double Amount, string ChequeNumber)
        {
            bool transactionDebited = false;
            try
            {
                AccountDAL accountDAL = new AccountDAL();
                List<Account> accounts = accountDAL.DeserializeFromJSON("AccountData.txt");

                foreach (Account account in accounts)
                {
                    if (account.AccountID == AccountID)
                    {
                        if (account.Balance >= Amount)
                        {
                            account.Balance = account.Balance - Amount;
                            TypeOfTranscation transEnum;
                            Enum.TryParse("Debit", out transEnum);
                            ModeOfTransaction cheEnum;
                            Enum.TryParse("Cheque", out cheEnum);
                            StoreTransactionRecord(AccountID, Amount, transEnum, cheEnum, ChequeNumber);
                            accountDAL.SerializeIntoJSON(accounts, "AccountData.txt");
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

        public override bool CreditTransactionByChequeDAL(Guid AccountID, double Amount, string ChequeNumber)
        {
            
            bool transactionCredited = false;
            try
            {
                AccountDAL accountDAL = new AccountDAL();
                List<Account> accounts = accountDAL.DeserializeFromJSON("AccountData.txt");

                foreach (Account account in accounts)
                {
                    if (account.AccountID == AccountID)
                    {
                        account.Balance = account.Balance + Amount;
                        TypeOfTranscation transEnum;
                        Enum.TryParse("Credit", out transEnum);
                        ModeOfTransaction cheEnum;
                        Enum.TryParse("Cheque", out cheEnum);
                        StoreTransactionRecord(AccountID, Amount, transEnum, cheEnum, ChequeNumber);
                        accountDAL.SerializeIntoJSON(accounts, "AccountData.txt");
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

        public override List<Transaction> DisplayTransactionByAccountIDDAL(Guid AccountID)
        {
            try
            {
                List<Transaction> transactions = DeserializeFromJSON("Transactions.txt");
                List<Transaction> transactionsOfAccountID = new List<Transaction>();
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.AccountID == AccountID)
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

        public override Transaction DisplayTransactionByTransactionIDDAL(Guid TransactionID)
        {
            try
            {
                List<Transaction> transactions = DeserializeFromJSON("Transactions.txt");
                foreach (Transaction transaction in transactions)
                {
                    if (transaction.TransactionID == TransactionID)
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
        public override List<Transaction> DeserializeFromJSON(string FileName)
        {
            try
            {
                List<Transaction> transactionsList = JsonConvert.DeserializeObject<List<Transaction>>(File.ReadAllText(FileName));// Done to read data from file
                return transactionsList;
            }
            catch 
            {
                throw;
            }
           
        }

        public override bool SerializeIntoJSON(List<Transaction> transactions, string fileName)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))   //filename is used so that we can have access over our own file
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, transactions);
                    sw.Close();
                    fs.Close();
                    return true;
                }
            }
            catch
            {
               throw;
            }
        }

    }
}
