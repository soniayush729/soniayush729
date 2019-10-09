export class Transaction {
  id: number;
  accountNumber: number;
  accountID: string;
  typeOfTransaction: string;
  amount: number;
  transactionID: string;
  dateOfTransaction: string;
  modeOfTransaction: string;
  chequeNumber: string;

  constructor(
    ID: number,
    AccountNumber: number,
    AccountID: string,
    TypeOfTransaction: string,
    Amount: number,
    TransactionID: string,
    DateOfTransaction: string,
    ModeOfTransaction: string,
    ChequeNumber: string)
  {
    this.id = ID;
    this.accountNumber = AccountNumber;
    this.accountID = AccountID;
    this.typeOfTransaction = TypeOfTransaction;
    this.amount = Amount;
    this.transactionID = TransactionID;
    this.dateOfTransaction = DateOfTransaction;
    this.modeOfTransaction = ModeOfTransaction;
    this.chequeNumber = ChequeNumber;
  }
}

