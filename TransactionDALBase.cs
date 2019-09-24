using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pecunia.Entities;

namespace Pecunia.Contracts
{
    public abstract class TransactionDALAbstract
    {
        public abstract bool StoreTransactionRecord(Guid accountID, double Amount, TypeOfTranscation type, ModeOfTransaction mode, string chequeNo);
        public abstract bool DebitTransactionByWithdrawalSlipDAL(Guid AccountID, double Amount);
        public abstract bool CreditTransactionByDepositSlipDAL(Guid AccountID, double Amount);
        public abstract bool DebitTransactionByChequeDAL(Guid AccountID, double Amount, string ChequeNumber);
        public abstract bool CreditTransactionByChequeDAL(Guid AccountID, double Amount, string ChequeNumber);
        public abstract List<Transaction> DisplayTransactionByAccountIDDAL(Guid AccountID);
        public abstract Transaction DisplayTransactionByTransactionIDDAL(Guid TransactionID);
        public abstract bool SerializeIntoJSON(List<Transaction> transactions, string filename);
        public abstract List<Transaction> DeserializeFromJSON(string filename);
        public abstract bool TransactionIDExistsDAL(Guid TransactionID);
        public abstract List<Transaction> GetAllTransactionsDAL();

    }
}
