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

namespace Pecunia.DataAccessLayer
{

    public abstract class TransactionDALAbstract
    {
        public abstract bool StoreTransaction(long accountNo, double Amount, TypeOfTranscation type, string mode, string chequeNo);
        public abstract bool DebitTransactionByWithdrawalSlipDAL(long AccountNo, double Amount);
        public abstract bool CreditTransactionByWithdrawalSlipDAL(long AccountNo, double Amount);
        public abstract bool DebitTransactionByChequeDAL(long AccountNo, double Amount, string ChequeNo);
        public abstract bool CreditTransactionByChequeDAL(long AccountNo, double Amount, string ChequeNo);
        public abstract TransactionEntities DisplayTransactionByCustomerID_DAL(string CustomerID);
        public abstract TransactionEntities DisplayTransactionByAccountNo_DAL(long AccountNo);
        public abstract TransactionEntities DisplayTransactionDetailsByTransactionID_DAL(string TransactionID);
        public abstract bool  SerializeIntoJSON(List<TransactionEntities> transaction, string FileName);
        public abstract List<TransactionEntities> DeSerializeFromJSON(string FileName);

    }
    [Serializable]
    public class TransactionDAL: TransactionDALAbstract
    {
        
        public static List<TransactionEntities> Transactions = new List<TransactionEntities>() { };
        
        
        public override bool StoreTransaction(long accountNo, double Amount, TypeOfTranscation type, string mode, string chequeNo)
        {
            //// retrieving customerID based on account No
            string customerID = "00000000000000";// dummy initialization to avoid warnings
            foreach(Account acc in AccountDAL.ListOfAccounts)
            {
                    if (acc.AccountNo == accountNo)
                        customerID = acc.CustomerID;
            }

            DateTime time = DateTime.Now;
            string transactionID = "TRANS" + time.ToString("yyyyMMddhhmmss");//sample transactionID : TRANS20190921154525

            ////Creating object for Transaction Entites
            TransactionEntities trans = new TransactionEntities();
            trans.AccountNo = accountNo;
            trans.CustomerID = customerID;
            trans.Type = type;
            trans.Amount = Amount;
            trans.TransactionID = transactionID;
            trans.DateOfTransaction = time;
            trans.Mode = mode;
            trans.ChequeNo = chequeNo;

            Transactions.Add(trans);
            return SerializeIntoJSON(Transactions, "Transactions.txt");
        }

        public override bool DebitTransactionByWithdrawalSlipDAL(long AccountNo, double Amount)
        {
            bool res = false;
            foreach (Account acc in AccountDAL.ListOfAccounts)
            {
                if (acc.AccountNo == AccountNo)
                {
                    acc.Balance = acc.Balance - Amount;
                    TypeOfTranscation transEnum;
                    Enum.TryParse("Debit", out transEnum);
                    StoreTransaction(AccountNo, Amount, transEnum ,"WithdrawalSlip", null);
                    res = true;
                    break;
                
                }

            }
            if (res == true)
            {
                return true;
            }
            else
            {
               return false;
            }
        }

        public override bool CreditTransactionByWithdrawalSlipDAL(long AccountNo, double Amount)
        {
            bool res = false;
            foreach (Account acc in AccountDAL.ListOfAccounts)
            {
                if (acc.AccountNo == AccountNo)
                {
                    acc.Balance = acc.Balance + Amount;
                    TypeOfTranscation transEnum;
                    Enum.TryParse("Credit", out transEnum);
                    StoreTransaction(AccountNo, Amount, transEnum, "WithdrawalSlip", null);
                    res=true;
                    break;
                }

            }
            if (res == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool DebitTransactionByChequeDAL(long AccountNo, double Amount, string ChequeNo)
        {
            bool res = false;
            foreach (Account acc in AccountDAL.ListOfAccounts)
            {
                if (acc.AccountNo == AccountNo)
                {
                    acc.Balance = acc.Balance - Amount;
                    TypeOfTranscation transEnum;
                    Enum.TryParse("Debit", out transEnum);
                    StoreTransaction(AccountNo, Amount, transEnum, "Cheque", ChequeNo);
                    res = true;
                    break;
                }

            }
            if (res == true)
            {
                
                return true;
            }
            else
            {
                
                return false;
            }
        }

        public override bool CreditTransactionByChequeDAL(long AccountNo, double Amount, string ChequeNo)
        {
            
            bool res = false;
            foreach (Account acc in AccountDAL.ListOfAccounts)
            {
                if (acc.AccountNo == AccountNo)
                {
                    acc.Balance = acc.Balance + Amount;
                    TypeOfTranscation transEnum;
                    Enum.TryParse("Credit", out transEnum);
                    StoreTransaction(AccountNo, Amount, transEnum, "Cheque", ChequeNo);
                    res = true;
                    break;
                }

            }
            if (res == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override TransactionEntities DisplayTransactionByCustomerID_DAL(string CustomerID)
        {
            List<TransactionEntities> transactionList = DeSerializeFromJSON("Transactions.txt");

            foreach (List<TransactionEntities> trans in transactionList)
            {
                if (trans.CustomerID == CustomerID)
                {
                    return trans;
                }
            }
            return null;
        }

        public override TransactionEntities DisplayTransactionByAccountNo_DAL(long AccountNo)
        {
            List<TransactionEntities> transactionList = DeSerializeFromJSON("Transactions.txt");

            foreach (List<TransactionEntities> trans in transactionList)
            {
                if (trans.AccountNo == AccountNo)
                {
                    return trans;
                }
            }
            return null;
        }

        public override TransactionEntities DisplayTransactionDetailsByTransactionID_DAL(string TransactionID)
        {
            List<TransactionEntities> transactionList = DeSerializeFromJSON("Transactions.txt");

            foreach (List<TransactionEntities> trans in transactionList)
            {
                if (trans.TransactionID == TransactionID)
                {
                    return trans;
                }
            }
            return null;
        }

        public List<TransactionEntities> GetAllTransactionsDAL()
        {
            List<TransactionEntities> transactionList = DeSerializeFromJSON("Transactions.txt");
            return transactionList;
        }

        public override bool SerializeIntoJSON(List<TransactionEntities> transaction, string FileName)
        {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(FileName))   //filename is used so that we can have access over our own file
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, transaction); // Serialize transactions in transaction.json
                    return true;
                }
        }

        public override List<TransactionEntities> DeSerializeFromJSON(string FileName)
        {
            List<TransactionEntities> transaction = JsonConvert.DeserializeObject<List<TransactionEntities>>(File.ReadAllText(FileName));// Done to read data from file
            using (StreamReader file = File.OpenText(FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<TransactionEntities> deserializedTransaction = (List<TransactionEntities>)serializer.Deserialize(file, typeof(List<TransactionEntities>));
                return deserializedTransaction;
            }
        }


    }
}
