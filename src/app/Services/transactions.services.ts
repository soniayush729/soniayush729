import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaction } from '../Models/transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionsService {
  constructor(private httpClient: HttpClient)
  {
  }

  CreditTransactionByCheque(transaction: Transaction): Observable<boolean>
  {
    transaction.dateOfTransaction = new Date().toLocaleDateString();
    transaction.transactionID = this.uuidv4();
    return this.httpClient.post<boolean>(`/api/transactions`, transaction);
  }

  DebitTransactionByCheque(transaction: Transaction): Observable<boolean> {
    transaction.dateOfTransaction = new Date().toLocaleDateString();
    transaction.transactionID = this.uuidv4();
    return this.httpClient.post<boolean>(`/api/transactions`, transaction);
  }

  CreditTransactionByDepositSlip(transaction: Transaction): Observable<boolean>
  {
    transaction.dateOfTransaction = new Date().toLocaleDateString();
    transaction.transactionID = this.uuidv4();
    return this.httpClient.post<boolean>(`/api/transactions`, transaction);
  }

  DebitTransactionByWithdrawalSlip(transaction: Transaction): Observable<boolean>
  {
    transaction.dateOfTransaction = new Date().toLocaleDateString();
    transaction.transactionID = this.uuidv4();
    return this.httpClient.post<boolean>(`/api/transactions`, transaction);
  }

  GetAllTransactions(): Observable<Transaction[]>
  {
    return this.httpClient.get<Transaction[]>(`/api/transactions`);
  }

  GetTransactionByTrasactionID(TrasactionID: string): Observable<Transaction>
  {
    return this.httpClient.get<Transaction>(`/api/transactions?transactionID=${TrasactionID}`);
  }

  GetTransactionByType(TypeOfTransaction: string): Observable<Transaction>
  {
    return this.httpClient.get<Transaction>(`/api/transactions?typeOfTransaction=${TypeOfTransaction}`);
  }

  GetTransactionByAccountNumber(AccountNumber: number): Observable<Transaction>
  {
    return this.httpClient.get<Transaction>(`/api/transactions?accountNumber=${AccountNumber}`);
  }

  uuidv4()
  {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });

  }

}
